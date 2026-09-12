using System;
using Skills;
using UnityEngine;

internal class Skill_deb_Attack : MonoBehaviour, ISkill, IDestroible
{
    private int _powerDebuff;
    private Mob _mob;

    private void Start()
    {
        _mob = GetComponent<Mob>();

        if (_mob != null || _mob._DB != null)
        {
            _powerDebuff = _mob._DB.MobSkill.Power;
            UseSkill();
        }
    }

    public void UseSkill()
    {
        if (_mob != null || _mob._DB != null)
        {
            PlayerHelper.Instance.Atack -= _powerDebuff;
        }
    }

    public void ClearSkill()
    {
        PlayerHelper.Instance.Atack += _powerDebuff;
    }

    public void OnDestroy()
    {
        ClearSkill();
    }
}