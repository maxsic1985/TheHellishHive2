using System;
using System.Collections.Generic;
using Skills;
using UnityEngine;

internal class Skill_group_Speed:MonoBehaviour,ISkill,IDestroible
{
    private List<Mob> _mobs;

    private void Start()
    {
        _mobs = new List<Mob>();
        var mobsGO = GameObject.FindGameObjectsWithTag("Mob");
        for (int i = 0; i < mobsGO.Length; i++)
        {
            _mobs.Add(mobsGO[i].GetComponent<Mob>());
        }

        UseSkill();
    }

    public void UseSkill()
    {
        for (int i = 0; i < _mobs.Count; i++)
        {
            Debug.Log($"Group_Speed_for {_mobs.Count} was {_mobs[i].Speed} then {_mobs[i].Speed = _mobs[i].Speed + 1}");
        }
    }

    public void ClearSkill()
    {
        for (int i = 0; i < _mobs.Count; i++)
        {
            Debug.Log($"Group_Speed_for {_mobs.Count} was {_mobs[i].Speed} then {_mobs[i].Speed = _mobs[i].Speed - 1}");
        }
    }

    public void OnDestroy()
    {
      ClearSkill();
    }
}