using Services;
using Skills;
using UnityEngine;
using YG;


internal class Skill_Stunn : MonoBehaviour, ISkill, IDestroible
{
    [SerializeField] private int stunnCnt = 0;
    
    public void UseSkill()
    {
        if (stunnCnt < 3)
        {
            Debug.LogWarning("i Can Stunn");
            var shans = GetComponent<Mob>()._DB.MobSkill.Shans;
            if (SkillServices.GetSkilСhance(shans) && stunnCnt < 3)
            {
                Debug.LogWarning("Stunn");
                stunnCnt++;
                var msgTxt = YG2.envir.language == "en" ? "Stunn" : "Оглушен";
                CombatTextManager.Instance.CreateText(new Vector2(Screen.width / 2, Screen.height / 1.5f), msgTxt, Color.cyan);
                FindFirstObjectByType<SpeedHelper>().IsStunned = true;
            }
        }
        else
        {
            Debug.LogWarning("i Have max Stunn");
            
        }
    }

    public void ClearSkill()
    {
    }

    public void OnDestroy()
    {
        ClearSkill();
    }
}