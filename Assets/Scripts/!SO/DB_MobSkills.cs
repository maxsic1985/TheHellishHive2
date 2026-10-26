using UnityEngine;
using YG;

namespace _SO
{
    [CreateAssetMenu(fileName = "MonsterSkills", menuName = "Monsters/MonsterSkills", order = 1)]
    public class DB_MobSkills : ScriptableObject
    {
        public SkillEnum Skill;
        public Sprite SkillSprite;
        public string SkillName = "";
        public string SkillDescription = "";
        public int Power;
        public int UpStepPower;
        public int Shans;
        public int UpStepShans;
        public int Mana;
        public int AccessLvl;
        public bool IsUse = false;
        public int MaxSkillLvl;
        public int CurrentSkillLVL = 0;
        public int UpLvlCost = 0;

        [SerializeField] private string SkillNameRu = "";
        [SerializeField] private string SkillNameEn = "";
        [SerializeField] private string SkillDescriptionRu = "";
        [SerializeField] private string SkillDescriptionEn = "";


        private void OnEnable()
        {
            IsUse = false;
            switch (YG2.envir.language)
            {
                case "en":
                    SkillName = SkillNameEn;
                    SkillDescription = SkillDescriptionEn;
                    break;
                case "ru":
                    SkillName = SkillNameRu;
                    SkillDescription = SkillDescriptionRu;
                    break;
                default:
                    SkillName = SkillNameRu;
                    SkillDescription = SkillDescriptionRu;
                    break;
            }
        }

        public bool CanUse()
        {
            if (PlayerHelper.Instance.LvlPlayer >= AccessLvl && PlayerHelper.Instance.manaCur >= Mana &&
                !IsUse) return true;
            return false;
        }

        public void UpLvl()
        {
            if (true)
                if (CurrentSkillLVL < MaxSkillLvl)
                {
                    CurrentSkillLVL++;
                    Power += UpStepPower;
                    Shans += UpStepShans;
                }
        }
    }
}