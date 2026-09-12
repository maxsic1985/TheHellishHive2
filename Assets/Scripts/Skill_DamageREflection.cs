using Services;
using UnityEngine;
using YG;

namespace Skills
{
    internal class Skill_DamageREflection : MonoBehaviour, ISkill
    {
        public bool IsReflect;
        private Mob _mob;

        private void Start()
        {
            _mob = GetComponent<Mob>();
        }

        public void UseSkill()
        {
            if (_mob == null || _mob._DB == null) return;
            
            var shans = _mob._DB.MobSkill.Shans;
            if (SkillServices.GetSkilСhance(shans))
            {
                IsReflect = true;
                var msgTxt = YG2.envir.language == "en" ? "Reflection" : "Отражение";
                CombatTextManager.Instance.CreateText(new Vector2(Screen.width / 2, Screen.height / 1.5f), msgTxt,
                    Color.cyan);
                
                int damage = PlayerHelper.Instance.Atack - _mob.MobDefens;
                Debug.LogWarning($"Reflect+ {damage}");
                PlayerHelper.Instance.HpCur -= damage;
                
               // IsReflect = false;
            }
        }

        public void ClearSkill()
        {
            IsReflect = false;
        }
    }
}