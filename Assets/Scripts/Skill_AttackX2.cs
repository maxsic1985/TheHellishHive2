using System;
using Skills;
using UnityEngine;

internal class Skill_AttackX2 : MonoBehaviour, ISkill, IDestroible
{
    private Mob _mob;
    private bool _iSUsed=false;

    private void Start()
    {
        _mob = GetComponent<Mob>();
    }

    private void Update()
    {
        if(PlayerHelper.Instance==null||_mob==null) return;
        if (PlayerHelper.Instance.HpCur <= PlayerHelper.Instance.HpMax && _iSUsed==false)
        {
            UseSkill();
        }
        else if(PlayerHelper.Instance.HpCur > PlayerHelper.Instance.HpMax && _iSUsed)
        {
            ClearSkill();
        }
    }


    public void UseSkill()
    {
        if(_mob==null||_mob._DB==null) return;
        _mob.Atack *= _mob._DB.MobSkill.Power;
        _iSUsed = true;
    }

    public void ClearSkill()
    {
        if(_mob==null||_mob._DB==null) return;
        _mob.Atack /= _mob._DB.MobSkill.Power;
        _iSUsed = false;
    }

    public void OnDestroy()
    {
        ClearSkill();
    }
}