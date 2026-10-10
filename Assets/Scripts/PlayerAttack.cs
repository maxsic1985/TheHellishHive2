using System.Collections;
using _SO;
using UnityEngine;
using YG;

namespace Skills
{
    public class PlayerAttack : MonoBehaviour
    {
        private static PlayerAttack instance;
        [SerializeField] private DB_MobSkills DefUP;
        [SerializeField] private DB_MobSkills Heal;
        [SerializeField] private DB_MobSkills HpToAttack;
        [SerializeField] private DB_MobSkills HpToMana;
        [SerializeField] private DB_MobSkills IqUp;
        [SerializeField] private DB_MobSkills SimpleMagic;
        [SerializeField] private DB_MobSkills SimpleMassAttack;
        [SerializeField] private DB_MobSkills MinDamageTarget;
        [SerializeField] private DB_MobSkills Rage10Percent;
        [SerializeField] private DB_MobSkills ResistPoison;
        [SerializeField] private DB_MobSkills ResistStan;
        public bool checkDefenceUp;

        public static PlayerAttack Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PlayerAttack>();
                }

                return instance;
            }
        }

        public int SimpleAttack()
        {
            print("simple");
          //  GameObject.FindObjectOfType<PVP>().selectTypeAtack = 1;
            //GameObject.FindObjectOfType<PVP>().SelectTypeAttack();
            return PlayerHelper.Instance.Atack;
        }

        public int SimpleMagicAttack()
        {
            print("simpleMagic");
            //GameObject.FindObjectOfType<PVP>().selectTypeAtack = 1;
          //  GameObject.FindObjectOfType<PVP>().SelectTypeAttack();
            
            PlayerHelper.Instance.ManaCur -= SimpleMagic.Power;
            return (PlayerHelper.Instance.Atack + PlayerHelper.Instance.Intelect / 2);
        }

        public int MassAttack()
        {
            print("simpleMagic");
            //GameObject.FindObjectOfType<PVP>().selectTypeAtack = 3;
            PlayerHelper.Instance.ManaCur -= SimpleMassAttack.Power;
            return (PlayerHelper.Instance.Atack + PlayerHelper.Instance.Intelect);
        }
        
        
        public void DefenceUp()//Баф на пдеф
        {
            float w = Screen.width;
            float h = Screen.height;
            if (!checkDefenceUp)
            {
                GetComponent<PlayerHelper>().ManaCur -= DefUP.Mana;
                GetComponent<PlayerHelper>().Stamina += DefUP.Power;
                print("Атака магией 1");
                var damage = GetComponent<damage>();
               damage.attackClipAudioSource.GetComponent<AudioSource>().PlayOneShot(damage.AtPlay);

                var msgTxt= YG2.envir.language == "en" ? "Defence UP":"Защита Увеличена";
                CombatTextManager.Instance.CreateText(new Vector2(w / 2, h / 2), msgTxt, Color.green);
                checkDefenceUp = true;
            }


        }
        public void DefenceDown()//Снятие бафа на пдеф
        {
            GetComponent<PlayerHelper>().Stamina -= DefUP.Power;
        }
    }
}