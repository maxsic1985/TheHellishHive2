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
        public int CurrentSkillLVL = 1;
        public int StartUpLvlCost = 5;
        public int UpLvlCost;


        [SerializeField] private string SkillNameRu = "";
        [SerializeField] private string SkillNameEn = "";
        [SerializeField] private string SkillDescriptionRu = "";
        [SerializeField] private string SkillDescriptionEn = "";
        [SerializeField] private string coinsKey = "coins";


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

        public bool CanUpSkillLvl()
        {
            if (CurrentSkillLVL < MaxSkillLvl)
            {
                if (YG2.GetState(coinsKey) >= GetPriceToUpdate())
                {
                    return true;
                }
            }

            return false;
        }

        public int GetPriceToUpdate()
        {
            return UpLvlCost = StartUpLvlCost * CurrentSkillLVL;
        }

        public void UpLvl()
        {
            if (!CanUpSkillLvl()) return;
            int coins = YG2.GetState(coinsKey);
            YG2.SetState(coinsKey, coins - UpLvlCost);
            CurrentSkillLVL++;
            Power += UpStepPower;
            Shans += UpStepShans;
        }
    }
}