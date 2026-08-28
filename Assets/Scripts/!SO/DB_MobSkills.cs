using UnityEngine;
using YG;

namespace _SO
{
    [CreateAssetMenu(fileName = "MonsterSkills", menuName = "Monsters/MonsterSkills", order = 1)]
    public class DB_MobSkills : ScriptableObject
    {
        public MobSkillEnum Skill;
        public string SkillName="";
        public string SkillDescription="";

        [SerializeField] private string SkillNameRu="";
        [SerializeField] private string SkillNameEn="";
        [SerializeField] private string SkillDescriptionRu="";
        [SerializeField] private string SkillDescriptionEn="";


        private void OnEnable()
        {
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
    }
}