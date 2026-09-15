using Skills;
using UnityEngine;

internal class Skill_IQ_kX2:MonoBehaviour, ISkill, IDestroible
{
    private Mob _mob;
    private EnemyHP _enemyHp;
    private bool _iSUsed=false;
    private int _initHP;

    private void Start()
    {
        _mob = GetComponent<Mob>();
        _enemyHp = GetComponent<EnemyHP>();
        _initHP = _enemyHp._hp;
    }

    private void Update()
    {
        if(_mob==null || _mob._DB==null || _enemyHp==null) return;
        if (_enemyHp._hp<= _initHP/2 && _iSUsed==false)
        {
            UseSkill();
        }
        else if(_enemyHp._hp > _initHP/2 && _iSUsed)
        {
            ClearSkill();
        }
    }


    public void UseSkill()
    {
        _mob.IQ *= _mob._DB.MobSkill.Power;
        _iSUsed = true;
    }

    public void ClearSkill()
    {
        _mob.IQ /= _mob._DB.MobSkill.Power;
        _iSUsed = false;
    }

    public void OnDestroy()
    {
        ClearSkill();
    }
}