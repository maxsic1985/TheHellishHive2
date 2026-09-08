using System;
using Skills;
using UnityEngine;

internal class Skill_NoESC:MonoBehaviour,ISkill,IDestroible
{
    private void Start()
    {
       // UseSkill();
    }

    public void UseSkill()
    {
        PlayerHelper.Instance._blockOut = true;
    }

    public void ClearSkill()
    {
        PlayerHelper.Instance._blockOut = false;

    }

    public void OnDestroy()
    {
      ClearSkill();
    }
}