using System;
using Skills;
using UnityEngine;

internal class Skill_deb_Defence:MonoBehaviour,ISkill,IDestroible
{
    private Mob _mob;
    private void Start()
    {
        _mob = GetComponent<Mob>();
        if(_mob==null||_mob._DB==null) return;
        UseSkill();
    }

    public void UseSkill()
    {
        PlayerHelper.Instance.Stamina-=_mob._DB.MobSkill.Power;
    }

    public void ClearSkill()
    {
        PlayerHelper.Instance.Stamina+=_mob._DB.MobSkill.Power;
    }

    public void OnDestroy()
    {
      ClearSkill();
    }
}

