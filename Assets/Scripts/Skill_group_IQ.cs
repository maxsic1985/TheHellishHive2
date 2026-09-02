using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Skills
{
    public class Skill_group_IQ : MonoBehaviour, ISkill, IDestroible
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
                
                Debug.Log($"Group_IQ_for {_mobs.Count} was {_mobs[i].IQ} then {_mobs[i].IQ = _mobs[i].IQ + 2}");
            }
        }

        public void ClearSkill()
        {
            for (int i = 0; i < _mobs.Count; i++)
            {
                Debug.Log($"Group_IQ_for {_mobs.Count} was {_mobs[i].IQ} then {_mobs[i].IQ = _mobs[i].IQ - 2}");
            }
        }

        public void OnDestroy()
        {
            ClearSkill();
            _mobs.Clear();
        }
    }
}