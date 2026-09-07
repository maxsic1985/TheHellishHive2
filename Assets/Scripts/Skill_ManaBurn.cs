using Skills;
using UnityEngine;

internal class Skill_ManaBurn : MonoBehaviour, ISkill, IDestroible
{
    private Mob _mob;

    private void Start()
    {
        _mob = GetComponent<Mob>();
    }

    public void UseSkill()
    {
        if (_mob == null || _mob._DB == null) return;
        if (PlayerHelper.Instance.ManaCur >= _mob._DB.MobSkill.Power)
            PlayerHelper.Instance.ManaCur -= _mob._DB.MobSkill.Power;
    }

    public void ClearSkill()
    {
    }

    public void OnDestroy()
    {
        ClearSkill();
    }
}