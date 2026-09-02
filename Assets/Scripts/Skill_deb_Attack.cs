using Skills;
using UnityEngine;

internal class Skill_deb_Attack:MonoBehaviour,ISkill,IDestroible
{
    public void UseSkill()
    {
        PlayerHelper.Instance.Atack-=5;
    }

    public void ClearSkill()
    {
        PlayerHelper.Instance.Atack+=5;
    }

    public void OnDestroy()
    {
       ClearSkill();
    }
}