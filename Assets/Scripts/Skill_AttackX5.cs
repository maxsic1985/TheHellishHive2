using Skills;
using UnityEngine;

internal class Skill_AttackX5 : MonoBehaviour, ISkill, IDestroible
{
    private Mob _mob;
    private bool _isUsed = false;
    private void Start()
    {
        _mob = GetComponent<Mob>();
    }

    public void UseSkill()
    {
        if ((_mob == null || _mob._DB == null)&& _isUsed) return;
        _mob.Atack = _mob.Atack + _mob._DB.MobSkill.Power;
        _isUsed = true;
    }

    public void ClearSkill()
    {
        
    }

    public void OnDestroy()
    {
   
    }
}