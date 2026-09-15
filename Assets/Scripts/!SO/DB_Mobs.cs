using YG;

namespace _SO
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "MonsterData", menuName = "Monsters/Monster Data", order = 1)]
    public class DB_Mobs : ScriptableObject
    {
        [Header("=== BASIC INFORMATION ===")] [SerializeField]
        private string monsterNameRU;
        [SerializeField] private string monsterNameEn;
        [SerializeField] private string monsterDescriptionRU;
        [SerializeField] private string monsterDescriptionEN;
      //  [SerializeField] private string monsterSkillsRU;
      //  [SerializeField] private string monsterSkillsEN;

        [Header("=== SKILLS===")] [SerializeField]
        private DB_MobSkills _mobSkill;

       
        
        public string MonsterName { get; private set; }
        public string MonsterDescription { get; private set; }
        public string MonsterSkills { get; private set; }

        public DB_MobSkills MobSkill => _mobSkill;


        private void OnEnable()
        {
            switch (YG2.envir.language)
            {
                case "en":
                    MonsterName = monsterNameEn;
                    MonsterDescription = monsterDescriptionEN;
                  //  MonsterSkills = monsterSkillsEN;
                    break;
                case "ru":
                    MonsterName = monsterNameRU;
                    MonsterDescription = monsterDescriptionRU;
                  //  MonsterSkills = monsterSkillsRU;
                    break;
                default:
                    MonsterName = monsterNameRU;
                    MonsterDescription = monsterDescriptionRU;
                  //  MonsterSkills = monsterSkillsRU;
                    break;
            }
        }
    }
}