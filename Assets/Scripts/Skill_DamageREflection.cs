using UnityEngine;

namespace Skills
{
    internal class Skill_DamageREflection:MonoBehaviour,ISkill
    {
        public bool IsReflect;
        public void UseSkill()
        {
            IsReflect = true;
        }

        public void ClearSkill()
        {
          
        }
    }
}