using System;
using UnityEngine;

namespace Skills
{
    public class Skill_group_IQ : MonoBehaviour, ISkill,IDestroible
    {
        private Mob[] _mobs = new Mob[3];

        private void Start()
        {
            var mobsGO = GameObject.FindGameObjectsWithTag("Mob");
            for (int i = 0; i < mobsGO.Length; i++)
            {
                _mobs[i] = mobsGO[i].GetComponent<Mob>();
            }

            UseSkill();
        }

        public void UseSkill()
        {
            for (int i = 0; i < _mobs.Length; i++)
            {
                Debug.Log($"Group_IQ_for {_mobs.Length} was {_mobs[i].IQ} then {_mobs[i].IQ = _mobs[i].IQ + 2}");
            }
        }

        public void ClearSkill()
        {
            for (int i = 0; i < _mobs.Length; i++)
            {
                Debug.Log($"Group_IQ_for {_mobs.Length} was {_mobs[i].IQ} then {_mobs[i].IQ = _mobs[i].IQ - 2}");
            }
        }
      public  void OnDestroy()
        {
           ClearSkill();
        }
    }
}