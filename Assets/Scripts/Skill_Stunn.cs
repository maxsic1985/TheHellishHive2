using System;
using Services;
using Skills;
using UnityEngine;


internal class Skill_Stunn : MonoBehaviour, ISkill, IDestroible
{
    private bool _iSUsed = false;
    private _randomMob _rm;

    private void Start()
    {
        _rm = GetComponentInParent<_randomMob>();
    }

    private void Update()
    {
        Debug.LogWarning(this.gameObject.GetComponent<damage>().IsGo);
        if (this.gameObject.GetComponent<damage>().IsGo == true && _iSUsed == false)
        {
            UseSkill();
        }

        // if (GetComponent<damage>().IsGo == false && _iSUsed == true)
        //     _iSUsed = false;
    }


    public void UseSkill()
    {
        if (SkillServices.GetSkilСhance())
        {
            Debug.LogWarning("Stunn");
            //   CombatTextManager.Instance.CreateText("Stunn",Color.magenta);
            GetComponent<EnemyHP>().TextDamageToPlayer(gameObject, "Stunn", Color.cyan);
            _iSUsed = true;
            _rm.EndRound();
        }
    }

    public void ClearSkill()
    {
        _iSUsed = false;
    }

    public void OnDestroy()
    {
        ClearSkill();
    }
}