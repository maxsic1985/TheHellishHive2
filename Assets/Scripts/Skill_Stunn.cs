using System;
using Services;
using Skills;
using UnityEngine;


internal class Skill_Stunn : MonoBehaviour, ISkill, IDestroible
{
    private bool _iSUsed = false;

    private void Start()
    {
    }

    private void Update()
    {
        if (GetComponent<damage>().IsGo == true && _iSUsed==false)
            UseSkill();
    }


    public void UseSkill()
    {
        if (SkillServices.GetSkilСhance())
        {
            Debug.Log("Stunn");
            _iSUsed = true;
        }
    }

    public void ClearSkill()
    {
    }

    public void OnDestroy()
    {
        ClearSkill();
    }
}