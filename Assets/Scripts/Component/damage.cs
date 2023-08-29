using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Класс реализует интерфейс Idamage,
/// вешается на все то что можно дамажить 
/// </summary>
public class damage : MonoBehaviour, Idamage

{
    #region Variables
    public AudioClip AtPlay;

    public AudioSource Attack;

    SpeedHelper sh;


    /// <summary>
    /// Урон по игроку
    /// </summary>
    private int damToPlayer;
    /// <summary>
    /// мана
    /// </summary>
    private int mana;
    /// <summary>
    /// Урон по мобу
    /// </summary>
    private int damToMob;
    /// <summary>
    /// тип атаки
    /// </summary>
    public int typeAttack;
    /// <summary>
    /// Урон по умолчанию
    /// </summary>
    public int damageonLoad;
    /// <summary>
    /// 
    /// </summary>
    public int goPlay;
    /// <summary>
    /// 
    /// </summary>
    public bool isGo;
    #endregion
    #region Properties
    public int TypeAttack
    {
        get
        {
            return typeAttack;
        }

        set
        {
            typeAttack = value;
        }
    }
    public int GoPlay
    {
        get
        {
            return goPlay;
        }

        set
        {
            goPlay = value;
        }
    }
    public bool IsGo
    {
        get
        {
            return isGo;
        }

        set
        {
            isGo = value;
        }
    }



    public bool checkDefenceUp { get; private set; }
    public bool checkMassAttack { get; set; }

    randomMob rm;
    #endregion
    #region Voids
    /// <summary>
    /// Расчет урона по мобу, вызывается в ImpactDamage
    /// </summary>
    public void GetDamageToMob()
    {
        switch (TypeAttack)
        {
            case 1:

                damToMob = GetComponent<PlayerHelper>().Atack;
                Attack.GetComponent<AudioSource>().PlayOneShot(AtPlay);
                print("удар мечем");

                break;
            case 2:
                damToMob = GetComponent<PlayerHelper>().Atack + (GetComponent<PlayerHelper>().Atack/2);//GetComponent<PlayerHelper>().Intelect
                mana = GetComponent<PlayerHelper>().ManaCur -= 10;
                Attack.GetComponent<AudioSource>().PlayOneShot(AtPlay);
                print("Атака магией 1");
                break;
            default:
                break;
        }

    }
    public void DefenceUp()//Баф на пдеф
    {
        float w = Screen.width;
        float h = Screen.height;
        if (!checkDefenceUp)
        {
            mana = GetComponent<PlayerHelper>().ManaCur -= 50;
            GetComponent<PlayerHelper>().Stamina += 4;
            Attack.GetComponent<AudioSource>().PlayOneShot(AtPlay);
            print("Атака магией 1");
            CombatTextManager.Instance.CreateText(new Vector2(w / 2, h / 2), "Защита Увеличена", Color.green);
            checkDefenceUp = true;
        }


    }
    public void DefenceDown()//Снятие бафа на пдеф
    {
        GetComponent<PlayerHelper>().Stamina -= 10;
    }

    public IEnumerator MassAttack()
    {
        GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
        print(checkMassAttack);
        if (!checkMassAttack)
        {
            rm.hideMenuBattle();
            IsGo = true;
            for (int i = 0; i < mobs.Length; i++)
            {
                checkMassAttack = true;
                damToMob = GetComponent<PlayerHelper>().Intelect+GetComponent<PlayerHelper>().Atack;
                mobs[i].GetComponent<hp>().ImpactDamageOnMob(rm.effekt3);
                print(ImpactDamageToMob());
                yield return new WaitForSeconds(0.3f);
            }
            mana = GetComponent<PlayerHelper>().ManaCur -= 30;


         
            sh.playerStep = false;

          //  sh.endRound = false;
            sh.WhoIsDamag();

            rm.hideMenuBattle();
            checkMassAttack = false;
        }
    }

    /// <summary>
    ///  Расчет урона по игроку, вызывается в ImpactDamage
    /// </summary>
    /// <param name="dmg"></param>
    public void GetDamageToPlayer(int dmg)
    {
        //    damToPlayer = Random.Range(0, dmg);
        damToPlayer = dmg;

    }
    /// <summary>
    /// Переменная урона по мобу, для передачи в hp
    /// </summary>
    /// <returns>Урон по мобу</returns>
    public int ImpactDamageToMob()
    {if (!checkMassAttack)
        {
            GetDamageToMob();
        }
        var result = damToMob;
        return result;
    }
    /// <summary>
    /// Переменная урона по игроку, для передачи в hp
    /// </summary>
    /// <returns>Урон по игроку</returns>
    public int ImpactDamageToPlayer()
    {

        // dmg = Random.Range(0, dmg);
        GetDamageToPlayer(GetComponent<Mob>().MobAtack);
        var result = damToPlayer;

        return result;


    }

    void Start()
    {

        rm = FindObjectOfType<randomMob>();
        sh = FindObjectOfType<SpeedHelper>();
    }
    void Update()
    {
        if (rm.EnbBattle && checkDefenceUp)
        {
            DefenceDown();
            checkDefenceUp = false;

        }
        else if (rm.EnbBattle)
        {
            checkMassAttack = false;
        }
    }
    #endregion
}




