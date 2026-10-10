using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Skills;
using UnityEngine.Serialization;
using YG;

/// <summary>
/// Класс реализует интерфейс Idamage,
/// вешается на все то что можно дамажить 
/// </summary>

public class damage : MonoBehaviour, Idamage

{
    #region Variables
    public AudioClip AtPlay;

    [FormerlySerializedAs("Attack")] public AudioSource attackClipAudioSource;

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

    _randomMob rm;
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

                damToMob = PlayerAttack.Instance.SimpleAttack();
                attackClipAudioSource.GetComponent<AudioSource>().PlayOneShot(AtPlay);
                break;
            case 2:
                damToMob = PlayerAttack.Instance.SimpleMagicAttack();
                attackClipAudioSource.GetComponent<AudioSource>().PlayOneShot(AtPlay);
                break;
            case 3:
                damToMob = 0;//GetComponent<PlayerHelper>().Intelect
                print("Отражение");
                break;
            default:
                break;
        }

    }
   

    public IEnumerator MassAttack()
    {
        GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
        if (!PlayerAttack.Instance.SimpleMassAttack.IsUse)
        {
            rm.hideMenuBattle();
            IsGo = true;
            for (int i = 0; i < mobs.Length; i++)
            {
                PlayerAttack.Instance.SimpleMassAttack.IsUse = true;
                damToMob = PlayerAttack.Instance.MassAttack();
                mobs[i].GetComponent<EnemyHP>().ImpactDamageOnMob(rm.effekt3);
                print(ImpactDamageToMob());
                yield return new WaitForSeconds(0.3f);
            }
         //   mana = GetComponent<PlayerHelper>().ManaCur -= 30;


         
            sh.playerStep = false;

          //  sh.endRound = false;
            sh.WhoIsDamag();

            rm.hideMenuBattle();
            PlayerAttack.Instance.SimpleMassAttack.IsUse = false;
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
    {if (!PlayerAttack.Instance.SimpleMassAttack.IsUse)
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
        GetDamageToPlayer(GetComponent<Mob>().Atack);
        var result = damToPlayer;

        return result;


    }

    void Start()
    {

        rm = FindObjectOfType<_randomMob>();
        sh = FindObjectOfType<SpeedHelper>();
    }
    void Update()
    {
        if (rm.EnbBattle && PlayerAttack.Instance.DefUP.IsUse)
        {
            PlayerAttack.Instance.DefenceDown();
            PlayerAttack.Instance.DefUP.IsUse = false;

        }
        else if (rm.EnbBattle)
        {
            PlayerAttack.Instance.SimpleMassAttack.IsUse = false;
        }
    }
    #endregion
}




