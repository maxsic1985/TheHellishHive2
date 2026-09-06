using Services;
using Skills;
using UnityEngine;
using YG;


internal class Skill_Stunn : MonoBehaviour, ISkill, IDestroible
{
    [SerializeField] private int stunnCnt = 0;

    private void Start()
    {
    }

    public void UseSkill()
    {
        if (stunnCnt < 3)
        {
            Debug.LogWarning("i Can Stunn");

            if (SkillServices.GetSkilСhance() && stunnCnt < 3)
            {
                Debug.LogWarning("Stunn");
                stunnCnt++;
                var msgTxt = YG2.envir.language == "en" ? "Stunn" : "Оглушен";
               // GetComponent<EnemyHP>().TextDamageToPlayer(gameObject, msgTxt, Color.cyan);
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