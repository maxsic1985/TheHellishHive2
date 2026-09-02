using System;
using Skills;
using UnityEngine;

internal class Skill_deb_Defence:MonoBehaviour,ISkill,IDestroible
{
    private void Start()
    {
       
        UseSkill();
    }

    public void UseSkill()
    {
        PlayerHelper.Instance.Stamina-=2;
    }

    public void ClearSkill()
    {
        PlayerHelper.Instance.Stamina+=2;
    }

    public void OnDestroy()
    {
      ClearSkill();
    }
}

