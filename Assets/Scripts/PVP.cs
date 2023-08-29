//1. Выбираем игрока на экране SelectPlayer
//2. Выбираем  тип действия selectTypeAtack
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PVP : MonoBehaviour
{
    public int splayer;
    public int tattack;
    public int selectPlayer
    {
        get
        {
            return splayer;
        }

        set
        {
            splayer = 1;
        }
    }
    public Inventory battleInv;

    private int _manaPlayer;
    private int _expPayer;

    public GameObject forther;
    public GameObject mana1;
    public GameObject mana2;
    public GameObject mana3;

    public GameObject BtnRun;

    public void SelectPlayer()
    {
        if (selectTypeAtack == 1)
        {
        }
    }
    public int selectTypeAtack
    {
        get
        {
            return tattack;
        }

        set
        {
            tattack = value;
        }
    }
    public void SelectTypeAttack()
    {
        //0-ничего не выбрано,1-простая атка,2-магия,3-инвентарь, 4 уйти
        //if (selectPlayer != 0)
        //  {if
        if (GameObject.FindGameObjectWithTag("Mob"))
        {
            switch (selectTypeAtack)
            {
                case 1: GameObject.FindObjectOfType<randomMob>().GetComponent<randomMob>().ShowButtons(); BtnRun.SetActive(false); break;//выбрать моба для нанесения урона
                case 2: print("Защита увеличина"); GetComponent<damage>().DefenceUp(); break;//бафнуть во время битвы без выбора моба
                case 3: GetComponent<damage>().checkMassAttack = false; StartCoroutine(GetComponent<damage>().MassAttack()); BtnRun.SetActive(false); break;
                default: GameObject.FindObjectOfType<randomMob>().GetComponent<randomMob>().HideButtoms(); BtnRun.SetActive(true); break;
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
        if (forther.activeSelf)
        {
            _manaPlayer = GetComponent<PlayerHelper>().ManaCur;
            _expPayer = GetComponent<PlayerHelper>().LvlPlayer;
            //простая абилка магическая
            if (_expPayer >= 2 && _manaPlayer >= 10)
            {
                mana1.GetComponent<Button>().enabled = true;
                mana1.GetComponent<Image>().color = Color.white;
            }
            //Увеличение защиты
            else
            {
                mana1.GetComponent<Button>().enabled = false;
                mana1.GetComponent<Image>().color = Color.gray;
            }
            if (_expPayer >= 5 && _manaPlayer >= 50 && !GetComponent<damage>().checkDefenceUp)
            {
                mana2.GetComponent<Button>().enabled = true;
                mana2.GetComponent<Image>().color = Color.white;
            }
            //Массуха
            else
            {
                mana2.GetComponent<Button>().enabled = false;
                mana2.GetComponent<Image>().color = Color.gray;
            }
            if (_expPayer >= 7 && _manaPlayer >= 30 && !GetComponent<damage>().checkMassAttack)
            {
                mana3.GetComponent<Button>().enabled = true;
                mana3.GetComponent<Image>().color = Color.white;
            }
            else
            {
                mana3.GetComponent<Button>().enabled = false;
                mana3.GetComponent<Image>().color = Color.gray;
            }
            SelectTypeAttack();
        }
    }
}
