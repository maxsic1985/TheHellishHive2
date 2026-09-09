using System;
using Services;
using Skills;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using YG;

internal class Skill_Poison : MonoBehaviour, ISkill, IDestroible
{
    private Mob _mob;
    private bool _iUsePoison;

    private void Start()
    {
        _mob = GetComponent<Mob>();
    }

    public void UseSkill()
    {
        if (_mob == null || _mob._DB == null) return;

        if (PlayerHelper.Instance._hasPoison) return;
        var shans = _mob._DB.MobSkill.Shans;
        if (SkillServices.GetSkilСhance(shans))
        {
            _iUsePoison = true;
            PlayerHelper.Instance._hasPoison = true;
            if (PlayerHelper.Instance.gameObject.GetComponent<Poison>() == null)
                PlayerHelper.Instance.gameObject.AddComponent<Poison>().Init(_mob._DB.MobSkill.Power);

            var msgTxt = YG2.envir.language == "en" ? "Poison" : "Отравление";
            CombatTextManager.Instance.CreateText(new Vector2(Screen.width / 2f, Screen.height / 1.5f), msgTxt,
                Color.magenta);
        }
    }

    public void ClearSkill()
    {
        PlayerHelper.Instance._hasPoison = false;
        var poison = PlayerHelper.Instance.gameObject.GetComponent<Poison>();
      if(poison!=null)  Destroy(poison);
    }

    public void OnDestroy()
    {
        if (_iUsePoison)
            ClearSkill();
    }
}