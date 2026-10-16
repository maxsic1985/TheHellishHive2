//1. Выбираем игрока на экране SelectPlayer
//2. Выбираем  тип действия selectTypeAtack

using UnityEngine;
using Skills;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PVP : MonoBehaviour
{
    public int splayer;
    public int tattack;

    public int selectPlayer
    {
        get { return splayer; }

        set { splayer = 1; }
    }

    public Inventory battleInv;

    private int _manaPlayer;
    private int _expPayer;

    public GameObject BtnSkills;
    public GameObject SImple;
    public GameObject DefUp;
    public GameObject MagicSimple;
    public GameObject MagicMass;
    public GameObject HealBtn;
    public GameObject HPToAttackBtn;
    public GameObject HPToManaBtn;
    public GameObject IQUpBtn;
    public GameObject RageBtn; 
    public GameObject MinDmgBtn;
    public GameObject ResistPoisonBtn;
    public GameObject ResisStanBtn;
    public GameObject HPToManaBtn4;

    public GameObject BtnRun;

    public void SelectPlayer()
    {
        if (selectTypeAtack == 1)
        {
        }
    }

    public int selectTypeAtack
    {
        get { return tattack; }

        set { tattack = value; }
    }

    public void SelectTypeAttack(int curselectTypeAtack)
    {
        //0-ничего не выбрано,1-простая атка,2-магия,3-инвентарь, 4 уйти
        //if (selectPlayer != 0)
        //  {if
        selectTypeAtack = curselectTypeAtack;
        if (GameObject.FindGameObjectWithTag("Mob"))
        {
            switch (selectTypeAtack)
            {
                case 1:
                    GameObject.FindObjectOfType<_randomMob>().GetComponent<_randomMob>().ShowButtons();
                    GetComponent<damage>().typeAttack = 1;
                    BtnRun.SetActive(false);
                    break; //выбрать моба для нанесения урона
                case 2:
                    GameObject.FindObjectOfType<_randomMob>().GetComponent<_randomMob>().ShowButtons();
                    GetComponent<damage>().typeAttack = 2;
                    BtnRun.SetActive(false);
                    break; //выбрать моба для нанесения урона
                case 3:
                    print("Защита увеличина");
                    PlayerAttack.Instance.DefenceUp();
                    break; //бафнуть во время битвы без выбора моба
                case 4:
                    PlayerAttack.Instance.SimpleMassAttack.IsUse = false;
                    StartCoroutine(GetComponent<damage>().MassAttack());
                    BtnRun.SetActive(false);
                    break;
                case 5:
                    print("Heal");
                    PlayerAttack.Instance.HealSelf();
                    break;
                case 6:
                    print("HPTOAT");
                    PlayerAttack.Instance.HPToAttackSelf();
                    break;
                case 7:
                    print("HPToManaSelf");
                    PlayerAttack.Instance.HPToManaSelf();
                    break;
                case 8:
                    print("IQUp");
                    PlayerAttack.Instance.IQUp();
                    break;
                case 9:
                    print("Rage10Percent");
                    PlayerAttack.Instance.RageBtnActivate();
                    break;
                case 10:
                    GameObject.FindObjectOfType<_randomMob>().GetComponent<_randomMob>().ShowButtons();
                  
                    // GetComponent<damage>().typeAttack = 1;
                    
                    GetComponent<damage>().typeAttack = 4;
                    BtnRun.SetActive(false);
                    break; //выбрать моба для нанесения урона
                case 11:
                    PlayerAttack.Instance.ResistToPoison();
                    break;
                case 12:
                    PlayerAttack.Instance.ResistToStan();
                    break;
                default:
                    GameObject.FindObjectOfType<_randomMob>().GetComponent<_randomMob>().HideButtoms();
                    BtnRun.SetActive(true);
                    break;
            }
        }
    }

    public void ShowBattleInv()
    {
        battleInv.ShowInventory();
    }

    void Start()
    {
        selectPlayer = 0;
        selectTypeAtack = 0;
        print(_expPayer);
    }

    void Update()
    {
        //Управление кнопками абилки
        if (BtnSkills.activeSelf)
        {
            _manaPlayer = PlayerHelper.Instance.ManaCur;
            _expPayer = PlayerHelper.Instance.LvlPlayer;
            //простая абилка магическая
            if (_expPayer >= 2 && _manaPlayer >= 10)
            {
                MagicSimple.GetComponent<Button>().enabled = true;
                MagicSimple.GetComponent<Image>().color = Color.white;
            }
            //Увеличение защиты
            else
            {
                MagicSimple.GetComponent<Button>().enabled = false;
                MagicSimple.GetComponent<Image>().color = Color.gray;
            }

            if (_expPayer >= 5 && _manaPlayer >= 50 && !PlayerAttack.Instance.DefUP.IsUse)
            {
                DefUp.GetComponent<Button>().enabled = true;
                DefUp.GetComponent<Image>().color = Color.white;
            }
            //Массуха
            else
            {
                DefUp.GetComponent<Button>().enabled = false;
                DefUp.GetComponent<Image>().color = Color.gray;
            }

            if (_expPayer >= 7 && _manaPlayer >= 30 && !PlayerAttack.Instance.SimpleMassAttack.IsUse)
            {
                MagicMass.GetComponent<Button>().enabled = true;
                MagicMass.GetComponent<Image>().color = Color.white;
            }
            else
            {
                MagicMass.GetComponent<Button>().enabled = false;
                MagicMass.GetComponent<Image>().color = Color.gray;
            }

            if (_expPayer >= 7 && _manaPlayer >= 30 && !PlayerAttack.Instance.Heal.IsUse)
            {
                HealBtn.GetComponent<Button>().enabled = true;
                HealBtn.GetComponent<Image>().color = Color.white;
            }
            else
            {
                HealBtn.GetComponent<Button>().enabled = false;
                HealBtn.GetComponent<Image>().color = Color.gray;
            }

            if (_expPayer >= 7 && _manaPlayer >= 30 && !PlayerAttack.Instance.HpToAttack.IsUse)
            {
                HPToAttackBtn.GetComponent<Button>().enabled = true;
                HPToAttackBtn.GetComponent<Image>().color = Color.white;
            }
            else
            {
                HPToAttackBtn.GetComponent<Button>().enabled = false;
                HPToAttackBtn.GetComponent<Image>().color = Color.gray;
            }

            if (_expPayer >= 7 && _manaPlayer >= 30 && !PlayerAttack.Instance.HpToMana.IsUse)
            {
                HPToManaBtn.GetComponent<Button>().enabled = true;
                HPToManaBtn.GetComponent<Image>().color = Color.white;
            }
            else
            {
                HPToManaBtn.GetComponent<Button>().enabled = false;
                HPToManaBtn.GetComponent<Image>().color = Color.gray;
            }

            if (_expPayer >= 7 && _manaPlayer >= 30 && !PlayerAttack.Instance.IqUp.IsUse)
            {
                IQUpBtn.GetComponent<Button>().enabled = true;
                IQUpBtn.GetComponent<Image>().color = Color.white;
            }
            else
            {
                IQUpBtn.GetComponent<Button>().enabled = false;
                IQUpBtn.GetComponent<Image>().color = Color.gray;
            }

            if (_expPayer >= 7 && _manaPlayer >= 30 && !PlayerAttack.Instance.Rage10Percent.IsUse)
            {
                RageBtn.GetComponent<Button>().enabled = true;
                RageBtn.GetComponent<Image>().color = Color.white;
            }
            else if (PlayerAttack.Instance.Rage10Percent.IsUse)
            {
                RageBtn.GetComponent<Button>().enabled = false;
                RageBtn.GetComponent<Image>().color = Color.gray;
                var calc10Percent = PlayerHelper.Instance.HpMax *   PlayerAttack.Instance.Rage10Percent.Shans/ 100;
                if (PlayerHelper.Instance.HpCur <= calc10Percent)
                {
                    PlayerAttack.Instance.RageActivate();
                    RageBtn.GetComponent<Image>().color = Color.red;
                }
                else
                {
                    PlayerAttack.Instance.RageDeActivate();
                    RageBtn.GetComponent<Image>().color = Color.gray;
                }
            }
            else
            {
                RageBtn.GetComponent<Button>().enabled = false;
                RageBtn.GetComponent<Image>().color = Color.gray;
            }

            if (_expPayer >= 7 && _manaPlayer >= 30 && !PlayerAttack.Instance.SetMinimalDamage.IsUse)
            {
                MinDmgBtn.GetComponent<Button>().enabled = true;
                MinDmgBtn.GetComponent<Image>().color = Color.white;
            }
            else
            {
                MinDmgBtn.GetComponent<Button>().enabled = false;
                MinDmgBtn.GetComponent<Image>().color = Color.gray;
            }
            
            if (_expPayer >= 1 && _manaPlayer >= 30 && !PlayerAttack.Instance.ResistPoison.IsUse)
            {
                ResistPoisonBtn.GetComponent<Button>().enabled = true;
                ResistPoisonBtn.GetComponent<Image>().color = Color.white;
            }
            else
            {
                ResistPoisonBtn.GetComponent<Button>().enabled = false;
                ResistPoisonBtn.GetComponent<Image>().color = Color.gray;
            }
            
            if (_expPayer >= 1 && _manaPlayer >= 30 && !PlayerAttack.Instance.ResistStan.IsUse)
            {
                ResisStanBtn.GetComponent<Button>().enabled = true;
                ResisStanBtn.GetComponent<Image>().color = Color.white;
            }
            else
            {
                ResisStanBtn.GetComponent<Button>().enabled = false;
                ResisStanBtn.GetComponent<Image>().color = Color.gray;
            }
            
            SelectTypeAttack(selectTypeAtack);
        }
    }
}