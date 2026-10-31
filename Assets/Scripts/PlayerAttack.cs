using _SO;
using UnityEngine;
using YG;

namespace Skills
{
    public class PlayerAttack : MonoBehaviour
    {
        private static PlayerAttack instance;
        public DB_MobSkills SimpleSwordAttack;
        public DB_MobSkills DefUP;
        public DB_MobSkills Heal;
        public DB_MobSkills HpToAttack;
        public DB_MobSkills HpToMana;
        public DB_MobSkills IqUp;
        public DB_MobSkills SimpleMagic;
        public DB_MobSkills SimpleMassAttack;
        public DB_MobSkills SetMinimalDamage;
        public DB_MobSkills Rage10Percent;
        public DB_MobSkills ResistPoison;
        public DB_MobSkills ResistStan;
        private int _oldAttack;
        private bool rageActiveNow;
        public bool EnbBattle;

        public bool IsGo => GetComponent<damage>().IsGo;

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

        public bool NewRound { get; set; }

        public bool GetKritShans(int iq)
        {
            int shans = Random.Range(0, 100);
            if (shans > (100 - iq))
            {
                var msgTxt = YG2.envir.language == "en"
                    ? $"Krit"
                    : $"Крит";
                CombatTextManager.Instance.CreateText(new Vector2(Screen.width / 2, Screen.height / 2),
                    msgTxt, Color.red);
            }
            else
            {
                return false;
            }

            return true;
        }

        public int SimpleAttack()
        {
            if (GetKritShans(PlayerHelper.Instance.baseIQ))
            {
                return (PlayerHelper.Instance.AtackCur + SimpleSwordAttack.Power) * 2;
            }

            return PlayerHelper.Instance.AtackCur + SimpleSwordAttack.Power;
        }

        public int SimpleMagicAttack()
        {
            PlayerHelper.Instance.ManaCur -= SimpleMagic.Mana;
            if (GetKritShans(PlayerHelper.Instance.baseIQ))
            {
                return (PlayerHelper.Instance.Intelect + SimpleMagic.Power) * 2;
            }

            return (PlayerHelper.Instance.Intelect + SimpleMagic.Power);
        }

        public int MinDamageForTargetMob(Mob selected)
        {
            PlayerHelper.Instance.ManaCur -= SetMinimalDamage.Mana;
            if (GetKritShans(SetMinimalDamage.Power))
            {
                selected.SetMinDamageDeBuf = true;
                selected.SetMinDamage();

                var msgTxt = YG2.envir.language == "en"
                    ? $"Set minimal damage for round"
                    : $"Минимальный урон на ход";
                CombatTextManager.Instance.CreateText(new Vector2(Screen.width / 2, Screen.height / 2),
                    msgTxt, Color.green);

                return (PlayerHelper.Instance.AtackCur + SetMinimalDamage.Power / 5);
            }
            else
            {
                var msgTxt = YG2.envir.language == "en"
                    ? $"Miss"
                    : $"Не повезло";
                CombatTextManager.Instance.CreateText(new Vector2(Screen.width / 2, Screen.height / 2),
                    msgTxt, Color.red);
                return PlayerHelper.Instance.AtackCur;
            }
        }

        public int MassAttack()
        {
            if (GetKritShans(PlayerHelper.Instance.baseIQ))
            {
                return (PlayerHelper.Instance.Intelect + SimpleMassAttack.Power) * 2;
            }

            return (PlayerHelper.Instance.Intelect + SimpleMassAttack.Power);
        }

        public void DefenceUp() //Баф на пдеф
        {
            float w = Screen.width;
            float h = Screen.height;
            if (!DefUP.IsUse)
            {
                PlayerHelper.Instance.ManaCur -= DefUP.Mana;
                PlayerHelper.Instance.Stamina += DefUP.Power;

                var damage = GetComponent<damage>();
                damage.attackClipAudioSource.GetComponent<AudioSource>().PlayOneShot(damage.AtPlay);

                var msgTxt = YG2.envir.language == "en"
                    ? $"Defence UP + {DefUP.Power}"
                    : $"Защита Увеличена + {DefUP.Power}";
                CombatTextManager.Instance.CreateText(new Vector2(w / 2, h / 2), msgTxt, Color.green);
                DefUP.IsUse = true;
            }
        }

        public void DefenceDown() //Снятие бафа на пдеф
        {
            PlayerHelper.Instance.Stamina -= DefUP.Power;
            DefUP.IsUse = false;
        }

        public void IQUp() //Баф на пдеф
        {
            float w = Screen.width;
            float h = Screen.height;
            if (!IqUp.IsUse)
            {
                GetComponent<PlayerHelper>().ManaCur -= IqUp.Mana;
                GetComponent<PlayerHelper>().baseIQ += IqUp.Power;

                var damage = GetComponent<damage>();
                damage.attackClipAudioSource.GetComponent<AudioSource>().PlayOneShot(damage.AtPlay);

                var msgTxt = YG2.envir.language == "en"
                    ? $"Shans kritical damage up + {IqUp.Power}"
                    : $"Шанс критической атаки увеличен + {IqUp.Power}";
                CombatTextManager.Instance.CreateText(new Vector2(w / 2, h / 2), msgTxt, Color.green);
                IqUp.IsUse = true;
            }
        }

        public void IQDown() //Снятие бафа на пдеф
        {
            GetComponent<PlayerHelper>().baseIQ -= IqUp.Power;
            IqUp.IsUse = false;
        }

        public void HealSelf()
        {
            float w = Screen.width;
            float h = Screen.height;
            if (!Heal.IsUse)
            {
                PlayerHelper.Instance.ManaCur -= Heal.Mana;
                PlayerHelper.Instance.HpCur += Heal.Power;

                var damage = GetComponent<damage>();
                damage.attackClipAudioSource.GetComponent<AudioSource>().PlayOneShot(damage.AtPlay);

                var msgTxt = YG2.envir.language == "en" ? $"Heal+ {Heal.Power}" : $"Лечение+ {Heal.Power}";
                CombatTextManager.Instance.CreateText(new Vector2(w / 2, h / 2), msgTxt, Color.green);
                Heal.IsUse = true;
            }
        }

        public void HPToAttackSelf()
        {
            float w = Screen.width;
            float h = Screen.height;
            if (!HpToAttack.IsUse)
            {
                if (PlayerHelper.Instance.HpCur > HpToAttack.Power)
                {
                    PlayerHelper.Instance.ManaCur -= HpToAttack.Mana;
                    PlayerHelper.Instance.HpCur -= HpToAttack.Power;
                    PlayerHelper.Instance.AtackCur += HpToAttack.Power;

                    var damage = GetComponent<damage>();
                    damage.attackClipAudioSource.GetComponent<AudioSource>().PlayOneShot(damage.AtPlay);

                    var msgTxt = YG2.envir.language == "en"
                        ? $"Change HP to Attack + Power {HpToAttack.Power}/ - HP {HpToAttack.Power}"
                        : $"Обмен здоровья на силу + Атака {HpToAttack.Power}/ - Здоровье {HpToAttack.Power}";
                    CombatTextManager.Instance.CreateText(new Vector2(w / 2, h / 2), msgTxt, Color.green);
                    HpToAttack.IsUse = true;
                }
            }
        }

        public void HPToManaSelf()
        {
            float w = Screen.width;
            float h = Screen.height;
            if (!HpToMana.IsUse)
            {
                if (PlayerHelper.Instance.HpCur > HpToMana.Power)
                {
                    //PlayerHelper.Instance.ManaCur -= HpToAttack.Mana;
                    PlayerHelper.Instance.HpCur -= HpToMana.Power;
                    PlayerHelper.Instance.ManaCur += HpToMana.Power;

                    var damage = GetComponent<damage>();
                    damage.attackClipAudioSource.GetComponent<AudioSource>().PlayOneShot(damage.AtPlay);

                    var msgTxt = YG2.envir.language == "en"
                        ? $"Change Mana to HP + MP {HpToMana.Power}/ - HP {HpToMana.Power}"
                        : $"Обмен здоровья на ману + Мана {HpToMana.Power} - Здоровье {HpToMana.Power}";
                    CombatTextManager.Instance.CreateText(new Vector2(w / 2, h / 2), msgTxt, Color.green);
                    HpToMana.IsUse = true;
                }
            }
        }

        public void RageBtnActivate()
        {
            float w = Screen.width;
            float h = Screen.height;
            if (!Rage10Percent.IsUse)
            {
                PlayerHelper.Instance.ManaCur -= Rage10Percent.Mana;
                // PlayerHelper.Instance.Atack = PlayerHelper.Instance.Atack * Rage10Percent.Power;

                var damage = GetComponent<damage>();
                damage.attackClipAudioSource.GetComponent<AudioSource>().PlayOneShot(damage.AtPlay);

                var msgTxt = YG2.envir.language == "en"
                    ? $"Rage"
                    : $"Ярость";
                CombatTextManager.Instance.CreateText(new Vector2(w / 2, h / 2), msgTxt, Color.green);

                Rage10Percent.IsUse = true;
            }
        }

        public void RageActivate()
        {
            if (rageActiveNow) return;
            _oldAttack = PlayerHelper.Instance.AtackCur;
            PlayerHelper.Instance.AtackCur = Rage10Percent.Power * _oldAttack;
            rageActiveNow = true;
        }

        public void RageDeActivate()
        {
            PlayerHelper.Instance.AtackCur = _oldAttack;
            rageActiveNow = false;
        }

        public void ResistToPoison()
        {
            float w = Screen.width;
            float h = Screen.height;
            if (!ResistPoison.IsUse && !ResistPoison.Miss)
            {
                PlayerHelper.Instance.ManaCur -= ResistPoison.Mana;
                if (GetKritShans(ResistPoison.Power))
                {
                    var damage = GetComponent<damage>();
                    damage.attackClipAudioSource.GetComponent<AudioSource>().PlayOneShot(damage.AtPlay);

                    var msgTxt = YG2.envir.language == "en"
                        ? $"Set resist to poison"
                        : $"Сопротивление яду";
                    CombatTextManager.Instance.CreateText(new Vector2(w / 2, h / 2), msgTxt, Color.green);
                    PlayerHelper.Instance._hasPoisonResist = true;
                    ResistPoison.IsUse = true;
                    return;
                }
                else
                {
                    var msgTxt = YG2.envir.language == "en"
                        ? $"Miss"
                        : $"Не удалось";
                    CombatTextManager.Instance.CreateText(new Vector2(w / 2, h / 2), msgTxt, Color.red);
                    ResistPoison.Miss = true;
                    return;
                }
            }
        }

        public void ResistToStan()
        {
            float w = Screen.width;
            float h = Screen.height;

            if (!ResistStan.IsUse && !ResistStan.Miss)
            {
                PlayerHelper.Instance.ManaCur -= ResistStan.Mana;
                if (GetKritShans(ResistStan.Power))
                {
                    var damage = GetComponent<damage>();
                    damage.attackClipAudioSource.GetComponent<AudioSource>().PlayOneShot(damage.AtPlay);

                    var msgTxt = YG2.envir.language == "en"
                        ? $"Set resist to stan"
                        : $"Сопротивление обездвиживанию";
                    CombatTextManager.Instance.CreateText(new Vector2(w / 2, h / 2), msgTxt, Color.green);
                    PlayerHelper.Instance._hasStanResist = true;
                    ResistStan.IsUse = true;
                }
                else
                {
                    var msgTxt = YG2.envir.language == "en"
                        ? $"Miss"
                        : $"Не удалось";
                    CombatTextManager.Instance.CreateText(new Vector2(w / 2, h / 2), msgTxt, Color.red);
                    ResistStan.Miss = true;
                    return;
                }
            }
        }

        public void EndRoundActions()
        {
            Heal.IsUse = false;
            ResistStan.Miss = false;
            ResistPoison.Miss = false;

            if (HpToAttack.IsUse)
            {
                HpToAttack.IsUse = false;
                PlayerHelper.Instance.AtackCur -= HpToAttack.Power;
            }

            if (HpToMana.IsUse)
            {
                HpToMana.IsUse = false;
            }
        }

        public void DeathActions()
        {
            if (Rage10Percent.IsUse)
            {
                Rage10Percent.IsUse = false;
                RageDeActivate();
            }

            EndBattleActions();
        }

        public void EndBattleActions()
        {
            if (EnbBattle && DefUP.IsUse)
            {
                PlayerAttack.Instance.DefenceDown();
                PlayerAttack.Instance.DefUP.IsUse = false;
            }

            if (EnbBattle && PlayerAttack.Instance.IqUp.IsUse)
            {
                PlayerAttack.Instance.IQDown();
                PlayerAttack.Instance.IqUp.IsUse = false;
            }

            if (EnbBattle && PlayerAttack.Instance.Rage10Percent.IsUse)
            {
                PlayerAttack.Instance.RageDeActivate();
                PlayerAttack.Instance.Rage10Percent.IsUse = false;
            }

            if (EnbBattle && PlayerAttack.Instance.ResistPoison.Miss)
            {
                PlayerAttack.Instance.ResistPoison.Miss = false;
            }

            if (EnbBattle && PlayerAttack.Instance.ResistStan.Miss)
            {
                PlayerAttack.Instance.ResistStan.Miss = false;
            }
            
            if (EnbBattle && PlayerAttack.Instance.ResistPoison.IsUse )
            {
                PlayerHelper.Instance._hasPoisonResist = false;
                PlayerAttack.Instance.ResistPoison.IsUse = false;
            }

            if (EnbBattle && PlayerAttack.Instance.ResistStan.IsUse)
            {
                PlayerHelper.Instance._hasStanResist = false;
                PlayerAttack.Instance.ResistStan.IsUse = false;
   
            }
            

            if (EnbBattle)
            {
                PlayerAttack.Instance.SimpleMassAttack.IsUse = false;
            }
        }

        void Update()
        {
            EndBattleActions();
        }
    }
}