using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedHelper : MonoBehaviour
{
    private float pauseTime = 0.6f;//время на проигрывание анимаций урона
    private float oneMobTime = 1.5f;
    private float twoMobTime = 2.3f;
    private float threeMobTime = 3f;


    public AudioClip Atmob1;
    public AudioClip Atmob2;
    public AudioClip GADAttack;
    public AudioClip GADPain;
    public AudioClip GADDeath;
    public AudioClip AttackBee;
    public AudioClip Painmob2;
    private PlayAnimatorHPBar _playAnimBar;
    #region Var
    private int tmpExpForBattle;
    private int tmpGoldForBattle;
    Mob _mob;
    PlayerHelper _ph;
    private int m1, m2, m3;
    GameObject mobfirst;
    GameObject mobsecond;
    GameObject mobthirst;
    public bool playerStep;
    public bool playerIsFirst;
    private int speedPlayer;
    public bool playerIsSecond;
    public bool playerIsThr;
    public bool endRound;
    private bool AnimsSetDamageIsGone;
    private bool AnimsGetDamageIsGone;
    private bool endCorutine;
    private int i=0;



    #endregion
    #region Prop
    public int TmpGoldForBattle
    {
        get { return tmpGoldForBattle; }
        set { tmpGoldForBattle = value; }
    }
    public bool EndRound1
    {
        get
        {
            return endRound;
        }

        set
        {
            endRound = value;
        }
    }
    public int TmpExpForBattle
    {
        get { return tmpExpForBattle; }
        set { tmpExpForBattle = value; }
    }

    #endregion
    #region Voids

    public void GetNumberGo(GameObject one)
    {
        m1 = one.GetComponent<Mob>().Speed;
        one.GetComponent<damage>().GoPlay = 1;
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Mob"))
        {
            if (item.GetComponent<damage>().GoPlay == 1)
            {
                mobfirst = item.gameObject;
            }
        }
        speedPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHelper>().Speed;
        if (speedPlayer > mobfirst.GetComponent<Mob>().Speed)
        {
            playerIsFirst = true;
        }

        else playerIsSecond = true;
    }
    public void GetNumberGo(GameObject one, GameObject two)
    {
        m1 = one.GetComponent<Mob>().Speed;
        m2 = two.GetComponent<Mob>().Speed;
        if (m1 > m2)
        {
            one.GetComponent<damage>().GoPlay = 1;
            two.GetComponent<damage>().GoPlay = 2;
        }
        else
        {
            one.GetComponent<damage>().GoPlay = 2;
            two.GetComponent<damage>().GoPlay = 1;
        }
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Mob"))
        {
            if (item.GetComponent<damage>().GoPlay == 1)
            {
                mobfirst = item.gameObject;
            }
            if (item.GetComponent<damage>().GoPlay == 2)
            {
                mobsecond = item.gameObject;
            }
        }
        speedPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHelper>().Speed;
        if (speedPlayer > mobfirst.GetComponent<Mob>().Speed)
        {
            playerIsFirst = true;
        }
        else if (speedPlayer > mobsecond.GetComponent<Mob>().Speed)
        {
            playerIsSecond = true;
        }
        else playerIsThr = true;
    }
    public void GetNumberGo(GameObject one, GameObject two, GameObject three)
    {
        m1 = one.GetComponent<Mob>().Speed;
        m2 = two.GetComponent<Mob>().Speed;
        m3 = three.GetComponent<Mob>().Speed;
        if (m1 > m2)
        {
            if (m1 >= m3)
            {
                if (m2 >= m3)
                {
                    one.GetComponent<damage>().GoPlay = 1;
                    two.GetComponent<damage>().GoPlay = 2;
                    three.GetComponent<damage>().GoPlay = 3;
                }
                else
                {
                    one.GetComponent<damage>().GoPlay = 1;
                    two.GetComponent<damage>().GoPlay = 3;
                    three.GetComponent<damage>().GoPlay = 2;
                }
            }
            else
            {
                one.GetComponent<damage>().GoPlay = 2;
                two.GetComponent<damage>().GoPlay = 3;
                three.GetComponent<damage>().GoPlay = 1;
            }
        }
        else
        {
            if (m2 >= m3)
            {
                if (m3 >= m1)
                {
                    one.GetComponent<damage>().GoPlay = 3;
                    two.GetComponent<damage>().GoPlay = 1;
                    three.GetComponent<damage>().GoPlay = 2;
                }
                else
                {
                    one.GetComponent<damage>().GoPlay = 2;
                    two.GetComponent<damage>().GoPlay = 1;
                    three.GetComponent<damage>().GoPlay = 3;
                }
            }
            else
            {
                one.GetComponent<damage>().GoPlay = 3;
                two.GetComponent<damage>().GoPlay = 2;
                three.GetComponent<damage>().GoPlay = 1;
            }
        }
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Mob"))
        {
            if (item.GetComponent<damage>().GoPlay == 1)
            {
                mobfirst = item.gameObject;
            }
            if (item.GetComponent<damage>().GoPlay == 2)
            {
                mobsecond = item.gameObject;
            }
            if (item.GetComponent<damage>().GoPlay == 3)
            {
                mobthirst = item.gameObject;
            }
        }
        speedPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHelper>().Speed;
        if (speedPlayer > mobfirst.GetComponent<Mob>().Speed)
        {
            playerIsFirst = true;
            playerStep = true;

        }
        else if (speedPlayer > mobsecond.GetComponent<Mob>().Speed&& speedPlayer< mobfirst.GetComponent<Mob>().Speed)
        {
            playerIsSecond = true;
        }
        else if (speedPlayer > mobthirst.GetComponent<Mob>().Speed && speedPlayer < mobsecond.GetComponent<Mob>().Speed)
        {
            playerIsThr = true;
        }

    }
    IEnumerator WhoIsGo()
    {
        First:
        if (playerIsFirst)
        {
            Playerfirst();
        }
        else
        {
            if (mobfirst)
            {
                if (mobfirst.GetComponent<damage>().IsGo == true)
                {
                    goto Second;
                }
                else if (mobfirst.GetComponent<damage>().IsGo == false)
                {
                    MobDamage(mobfirst);
                }
            }
            else
            {
                goto Second;
            }

            //
            Second:
            if (playerIsSecond)
            {
                print("7");
                PlayerSecond();
            }
            else
            {
                if (mobsecond)
                {
                    if (mobsecond.GetComponent<damage>().IsGo == true)
                    {
                        print("8");
                        goto Thr;
                    }
                    else if (mobsecond.GetComponent<damage>().IsGo == false)
                    {
                        print("6");
                        MobDamage(mobsecond);
                    }
                }
                else
                {
                    goto Thr;
                }
            }
            //
            Thr:
            if (playerIsThr)
            {
                PlayerTH();
            }
            else
            {
                if (mobthirst)
                {
                    if (mobthirst.GetComponent<damage>().IsGo == true)
                    {
                        if (GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo == true)
                        {

                        }
                    }
                    else if (mobthirst.GetComponent<damage>().IsGo == false)
                    {
                        print("9");
                       // 
                        if (GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo == false && !playerIsSecond)
                        {
                            MobDamage(mobthirst);
                          
                        }
                        else
                        {
                            PlayerDamage();
                        }
                    }
                }
                else
                {
                   
                }
            }

            foth:
            if (!playerIsFirst && !playerIsSecond && !playerIsThr)
            {
                PlayerDamage();
            }
        }
        CheckEndRound();
        if (EndRound1 == true)
        {
            print("end round!!!");

        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().hideButtonEndRound();//кнопка конец хода
        }
        if (EndRound1 == false && GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo == true)
        {

            yield return new WaitForSeconds(0.1f);
            goto First;

        }
        yield return new WaitForSeconds(3.2f);// время окончания раунда 
        endCorutine = true;
    }
    public void CheckEndRound()
    {

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Mob").Length; i++)
        {
            if (GameObject.FindGameObjectsWithTag("Mob").Length == 3)
            {
                if ((GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo == true) && mobfirst.GetComponent<damage>().IsGo == true && mobsecond.GetComponent<damage>().IsGo == true && mobthirst.GetComponent<damage>().IsGo == true)
                {
                    //GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().ShowMenuNoBattle();
                    GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo = false;
                    mobfirst.GetComponent<damage>().IsGo = false;
                    mobsecond.GetComponent<damage>().IsGo = false;
                    mobthirst.GetComponent<damage>().IsGo = false;
                    print("End");
                    EndRound1 = true;
                }
                else
                {

                }
            }
            else if (GameObject.FindGameObjectsWithTag("Mob").Length == 2)
            {
                if (mobfirst && mobsecond)
                {
                    if ((GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo == true) && mobfirst.GetComponent<damage>().IsGo == true && mobsecond.GetComponent<damage>().IsGo == true)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo = false;
                        mobfirst.GetComponent<damage>().IsGo = false;
                        mobsecond.GetComponent<damage>().IsGo = false;
                        print("End");
                        EndRound1 = true;
                    }
                }
                else if (mobfirst && mobthirst)
                {
                    if ((GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo == true) && mobfirst.GetComponent<damage>().IsGo == true && mobthirst.GetComponent<damage>().IsGo == true)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo = false;
                        mobfirst.GetComponent<damage>().IsGo = false;
                        mobthirst.GetComponent<damage>().IsGo = false;
                        print("End");
                        EndRound1 = true;
                    }
                }
                else if (mobsecond && mobthirst)
                {
                    if ((GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo == true) && mobsecond.GetComponent<damage>().IsGo == true && mobthirst.GetComponent<damage>().IsGo == true)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo = false;
                        mobthirst.GetComponent<damage>().IsGo = false;
                        mobsecond.GetComponent<damage>().IsGo = false;
                        print("End");
                        EndRound1 = true;
                    }
                }
            }
            else if (GameObject.FindGameObjectsWithTag("Mob").Length == 1)
            {
                if (mobfirst)
                {
                    if ((GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo == true) && mobfirst.GetComponent<damage>().IsGo == true)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo = false;
                        mobfirst.GetComponent<damage>().IsGo = false;
                        print("End");
                        EndRound1 = true;
                    }
                }
                if (mobsecond)
                {
                    if ((GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo == true) && mobsecond.GetComponent<damage>().IsGo == true)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo = false;
                        mobsecond.GetComponent<damage>().IsGo = false;
                        print("End");
                        EndRound1 = true;
                    }
                }
                if (mobthirst)
                {
                    if ((GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo == true) && mobthirst.GetComponent<damage>().IsGo == true)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo = false;
                        mobthirst.GetComponent<damage>().IsGo = false;
                        print("End");
                        EndRound1 = true;
                    }
                }

            }

        }
        if (EndRound1)
        {
            AnimsSetDamageIsGone = false;

        }

    }
    private void PlayerTH()
    {

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo == true)
        {
            GetComponent<randomMob>().hideMenuBattle();
            if (mobthirst)
            {
                GetComponent<randomMob>().hideMenuBattle();

                if (mobthirst.GetComponent<damage>().IsGo == true && mobthirst)
                {
                   
                }
                else
                {
                    GetComponent<randomMob>().hideMenuBattle();
                    MobDamage(mobthirst);
                }
            }
        }
        else
        {
            if (endCorutine)
            {
                //  GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().ShowMenuBattle();
            }
            PlayerDamage();
        }
    }
    private void PlayerSecond()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo == true)
        {
            GetComponent<randomMob>().hideMenuBattle();
            if (mobsecond)
            {
               
                GetComponent<randomMob>().hideMenuBattle();
                if (mobsecond.GetComponent<damage>().IsGo == true)
                {
                    print("2");
                    PlayerTH();
                }
                else
                {
                    print("3");
                    GetComponent<randomMob>().hideMenuBattle();
                    MobDamage(mobsecond);
                }
            }
            else
            {
                print("4");
                PlayerTH();
            }
        }
        else
        {
            if (endCorutine)
            {
               //  GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().ShowMenuBattle();
            }
            print("5");
            PlayerDamage();
        }
    }
    private void Playerfirst()
    {
      
        if (i==0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().ShowMenuBattle();

        }
        i += 1;
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo == true)
        {
            GetComponent<randomMob>().hideMenuBattle();
            if (mobfirst)
            {
                GetComponent<randomMob>().hideMenuBattle();
                if (mobfirst.GetComponent<damage>().IsGo == true)
                {
                    PlayerSecond();
                }
                else
                {
                    GetComponent<randomMob>().hideMenuBattle();
                    MobDamage(mobfirst);
                }
            }
            else
            {
                PlayerSecond();
            }
        }
        else
        {
            PlayerDamage();
        }
    }
    private void PlayerDamage()
    {//все происходит нажатием на кнопку

        playerStep = true;


    }
    /// <summary>
    /// Проигрывание анимаций атаки мобов 
    /// </summary>
    /// <param name="mob">моб</param>
    /// <returns></returns>
    IEnumerator AnimsAtackMob(GameObject mob)
    {
        if (mob)
        {
            switch (mob.GetComponentInChildren<SkinnedMeshRenderer>().name)
            {
            
                case "Slizen":
                    if (mob)
                    {
                        if (mob.GetComponentInParent<damage>().GoPlay == 1)
                        {
                          
                            AnimsSetDamageIsGone = false;
                            yield return new WaitForSeconds(oneMobTime);//ждем 1.5 секунды после начала боя или хода игрока
                            GetComponent<randomMob>().hideMenuBattle();
                            GetComponent<AudioSource>().PlayOneShot(Atmob1);//звук атаки
                            mob.GetComponent<Animation>().Play("DamageSliz", PlayMode.StopAll);//проигрывание анимации
                            _playAnimBar.PlayAnimHPBAr();//анмация урона Бар запуск анимации
                            yield return new WaitForSeconds(pauseTime);//ждем пол секунды 
                            mob.GetComponent<Animation>().Play("IdelSliz");
                            mob.GetComponent<hp>().TextDamageToPlayer(mob, mob.GetComponent<damage>().ImpactDamageToPlayer().ToString(), Color.red);
                            _playAnimBar.StopAnims();
                            AnimsSetDamageIsGone = true;
                        }
                        if (mob.GetComponentInParent<damage>().GoPlay == 2)
                        {
                            AnimsSetDamageIsGone = false;
                            yield return new WaitForSeconds(twoMobTime);
                            GetComponent<randomMob>().hideMenuBattle();

                            GetComponent<AudioSource>().PlayOneShot(Atmob1);
                            mob.GetComponent<Animation>().Play("DamageSliz", PlayMode.StopAll);
                            _playAnimBar.PlayAnimHPBAr();
                            yield return new WaitForSeconds(pauseTime);
                            mob.GetComponent<Animation>().Play("IdelSliz");
                            mob.GetComponent<hp>().TextDamageToPlayer(mob, mob.GetComponent<damage>().ImpactDamageToPlayer().ToString(), Color.red);
                            _playAnimBar.StopAnims();
                            AnimsSetDamageIsGone = true;

                        }
                        if (mob.GetComponentInParent<damage>().GoPlay == 3)
                        {
                            AnimsSetDamageIsGone = false;
                            yield return new WaitForSeconds(threeMobTime);
                            GetComponent<randomMob>().hideMenuBattle();

                            GetComponent<AudioSource>().PlayOneShot(Atmob1);
                            mob.GetComponent<Animation>().Play("DamageSliz", PlayMode.StopAll);
                            _playAnimBar.PlayAnimHPBAr();
                            yield return new WaitForSeconds(pauseTime);
                            mob.GetComponent<Animation>().Play("IdelSliz");
                            mob.GetComponent<hp>().TextDamageToPlayer(mob, mob.GetComponent<damage>().ImpactDamageToPlayer().ToString(), Color.red);
                            _playAnimBar.StopAnims();
                            AnimsSetDamageIsGone = true;
                        }
                    }
                    break;
                case "TeloGADA":
                    if (mob)
                    {

                        if (mob.GetComponentInParent<damage>().GoPlay == 1)
                        {
                            AnimsSetDamageIsGone = false;
                            yield return new WaitForSeconds(oneMobTime);
                            GetComponent<randomMob>().hideMenuBattle();

                            GetComponent<AudioSource>().PlayOneShot(GADAttack);
                            mob.GetComponent<Animation>().Play("DamagGAD", PlayMode.StopAll);
                            _playAnimBar.PlayAnimHPBAr();
                            yield return new WaitForSeconds(pauseTime+0.1f);
                            mob.GetComponent<Animation>().Play("IdelGAD");
                            mob.GetComponent<hp>().TextDamageToPlayer(mob, mob.GetComponent<damage>().ImpactDamageToPlayer().ToString(), Color.red);
                            _playAnimBar.StopAnims();
                            AnimsSetDamageIsGone = true;
                        }
                        if (mob.GetComponentInParent<damage>().GoPlay == 2)
                        {
                            AnimsSetDamageIsGone = false;
                            yield return new WaitForSeconds(twoMobTime);
                            GetComponent<randomMob>().hideMenuBattle();

                            GetComponent<AudioSource>().PlayOneShot(GADAttack);
                            mob.GetComponent<Animation>().Play("DamagGAD", PlayMode.StopAll);
                            _playAnimBar.PlayAnimHPBAr();
                            yield return new WaitForSeconds(pauseTime+0.1f);
                            mob.GetComponent<Animation>().Play("IdelGAD");
                            mob.GetComponent<hp>().TextDamageToPlayer(mob, mob.GetComponent<damage>().ImpactDamageToPlayer().ToString(), Color.red);
                            _playAnimBar.StopAnims();
                            AnimsSetDamageIsGone = true;
                        }
                        if (mob.GetComponentInParent<damage>().GoPlay == 3)
                        {
                            AnimsSetDamageIsGone = false;
                            yield return new WaitForSeconds(threeMobTime);
                            GetComponent<randomMob>().hideMenuBattle();

                            GetComponent<AudioSource>().PlayOneShot(GADAttack);
                            mob.GetComponent<Animation>().Play("DamagGAD", PlayMode.StopAll);
                            _playAnimBar.PlayAnimHPBAr();
                            yield return new WaitForSeconds(pauseTime+0.1f);
                            mob.GetComponent<Animation>().Play("IdelGAD");
                            mob.GetComponent<hp>().TextDamageToPlayer(mob, mob.GetComponent<damage>().ImpactDamageToPlayer().ToString(), Color.red);
                            _playAnimBar.StopAnims();
                            AnimsSetDamageIsGone = true;
                        }
                    }
                    break;
                case "TeloGamunkula":
                    if (mob)
                    {

                        if (mob.GetComponentInParent<damage>().GoPlay == 1)
                        {
                            AnimsSetDamageIsGone = false;
                            yield return new WaitForSeconds(oneMobTime);
                            GetComponent<randomMob>().hideMenuBattle();

                            GetComponent<AudioSource>().PlayOneShot(Atmob2);
                            mob.GetComponent<Animation>().Play("DamagGom", PlayMode.StopAll);
                            _playAnimBar.PlayAnimHPBAr();
                            yield return new WaitForSeconds(pauseTime);
                            mob.GetComponent<Animation>().Play("IdelGom");
                            mob.GetComponent<hp>().TextDamageToPlayer(mob, mob.GetComponent<damage>().ImpactDamageToPlayer().ToString(), Color.red);
                            _playAnimBar.StopAnims();
                            AnimsSetDamageIsGone = true;
                        }
                        if (mob.GetComponentInParent<damage>().GoPlay == 2)
                        {
                            AnimsSetDamageIsGone = false;
                            yield return new WaitForSeconds(twoMobTime);
                            GetComponent<randomMob>().hideMenuBattle();

                            GetComponent<AudioSource>().PlayOneShot(Atmob2);
                            mob.GetComponent<Animation>().Play("DamagGom", PlayMode.StopAll);
                            _playAnimBar.PlayAnimHPBAr();
                            yield return new WaitForSeconds(pauseTime);
                            mob.GetComponent<Animation>().Play("IdelGom");
                            mob.GetComponent<hp>().TextDamageToPlayer(mob, mob.GetComponent<damage>().ImpactDamageToPlayer().ToString(), Color.red);
                            _playAnimBar.StopAnims();
                            AnimsSetDamageIsGone = true;
                        }
                        if (mob.GetComponentInParent<damage>().GoPlay == 3)
                        {
                            AnimsSetDamageIsGone = false;
                            yield return new WaitForSeconds(threeMobTime);
                            GetComponent<randomMob>().hideMenuBattle();

                            GetComponent<AudioSource>().PlayOneShot(Atmob2);
                            mob.GetComponent<Animation>().Play("DamagGom", PlayMode.StopAll);
                            _playAnimBar.PlayAnimHPBAr();
                            yield return new WaitForSeconds(pauseTime);
                            mob.GetComponent<Animation>().Play("IdelGom");
                            mob.GetComponent<hp>().TextDamageToPlayer(mob, mob.GetComponent<damage>().ImpactDamageToPlayer().ToString(), Color.red);
                            _playAnimBar.StopAnims();
                            AnimsSetDamageIsGone = true;
                        }
                    }
                    break;
			case "EvolGomun":
				if (mob)
				{

					if (mob.GetComponentInParent<damage>().GoPlay == 1)
					{
						AnimsSetDamageIsGone = false;
						yield return new WaitForSeconds(oneMobTime);
						GetComponent<randomMob>().hideMenuBattle();

						GetComponent<AudioSource>().PlayOneShot(Atmob2);
						mob.GetComponent<Animation>().Play("DamagEvolG", PlayMode.StopAll);
						_playAnimBar.PlayAnimHPBAr();
						yield return new WaitForSeconds(pauseTime);
						mob.GetComponent<Animation>().Play("IdelEvolG");
						mob.GetComponent<hp>().TextDamageToPlayer(mob, mob.GetComponent<damage>().ImpactDamageToPlayer().ToString(), Color.red);
						_playAnimBar.StopAnims();
						AnimsSetDamageIsGone = true;
					}
					if (mob.GetComponentInParent<damage>().GoPlay == 2)
					{
						AnimsSetDamageIsGone = false;
						yield return new WaitForSeconds(twoMobTime);
						GetComponent<randomMob>().hideMenuBattle();

						GetComponent<AudioSource>().PlayOneShot(Atmob2);
						mob.GetComponent<Animation>().Play("DamagEvolG", PlayMode.StopAll);
						_playAnimBar.PlayAnimHPBAr();
						yield return new WaitForSeconds(pauseTime);
						mob.GetComponent<Animation>().Play("IdelEvolG");
						mob.GetComponent<hp>().TextDamageToPlayer(mob, mob.GetComponent<damage>().ImpactDamageToPlayer().ToString(), Color.red);
						_playAnimBar.StopAnims();
						AnimsSetDamageIsGone = true;
					}
					if (mob.GetComponentInParent<damage>().GoPlay == 3)
					{
						AnimsSetDamageIsGone = false;
						yield return new WaitForSeconds(threeMobTime);
						GetComponent<randomMob>().hideMenuBattle();

						GetComponent<AudioSource>().PlayOneShot(Atmob2);
						mob.GetComponent<Animation>().Play("DamagEvolG", PlayMode.StopAll);
						_playAnimBar.PlayAnimHPBAr();
						yield return new WaitForSeconds(pauseTime);
						mob.GetComponent<Animation>().Play("IdelEvolG");
						mob.GetComponent<hp>().TextDamageToPlayer(mob, mob.GetComponent<damage>().ImpactDamageToPlayer().ToString(), Color.red);
						_playAnimBar.StopAnims();
						AnimsSetDamageIsGone = true;
					}
				}
				break;
                case "myxa":
                    if (mob)
                    {

                        if (mob.GetComponentInParent<damage>().GoPlay == 1)
                        {

                            AnimsSetDamageIsGone = false;
                            yield return new WaitForSeconds(oneMobTime);
                            GetComponent<AudioSource>().PlayOneShot(AttackBee);
                            mob.GetComponent<Animation>().Play("DamageMyxa", PlayMode.StopAll);
                            _playAnimBar.PlayAnimHPBAr();
                            yield return new WaitForSeconds(0.6f);
                            mob.GetComponent<Animation>().Play("IdelMyxa");
                            mob.GetComponent<hp>().TextDamageToPlayer(mob, mob.GetComponent<damage>().ImpactDamageToPlayer().ToString(), Color.red);
                            _playAnimBar.StopAnims();
                            AnimsSetDamageIsGone = true;
                        }
                        if (mob.GetComponentInParent<damage>().GoPlay == 2)
                        {

                            AnimsSetDamageIsGone = false;
                            yield return new WaitForSeconds(twoMobTime);
                            GetComponent<AudioSource>().PlayOneShot(AttackBee);
                            mob.GetComponent<Animation>().Play("DamageMyxa", PlayMode.StopAll);
                            _playAnimBar.PlayAnimHPBAr();
                            yield return new WaitForSeconds(0.6f);
                            mob.GetComponent<Animation>().Play("IdelMyxa");
                            mob.GetComponent<hp>().TextDamageToPlayer(mob, mob.GetComponent<damage>().ImpactDamageToPlayer().ToString(), Color.red);
                            _playAnimBar.StopAnims();
                            AnimsSetDamageIsGone = true;
                        }
                        if (mob.GetComponentInParent<damage>().GoPlay == 3)
                        {

                            AnimsSetDamageIsGone = false;
                            yield return new WaitForSeconds(threeMobTime);
                            GetComponent<AudioSource>().PlayOneShot(AttackBee);
                            mob.GetComponent<Animation>().Play("DamageMyxa", PlayMode.StopAll);
                            _playAnimBar.PlayAnimHPBAr();
                            yield return new WaitForSeconds(0.6f);
                            mob.GetComponent<Animation>().Play("IdelMyxa");
                            mob.GetComponent<hp>().TextDamageToPlayer(mob, mob.GetComponent<damage>().ImpactDamageToPlayer().ToString(), Color.red);
                            _playAnimBar.StopAnims();
                            AnimsSetDamageIsGone = true;
                        }
                    }
                    break;
                case "Krab":
                    if (mob)
                    {

                        if (mob.GetComponentInParent<damage>().GoPlay == 1)
                        {
                            mob.GetComponent<Animation>().Play("IdelKRAB");
                            print("Босс вторй");
                            AnimsSetDamageIsGone = false;
                            yield return new WaitForSeconds(1.3f);
                            GetComponent<AudioSource>().PlayOneShot(AttackBee);
                            mob.GetComponent<Animation>().Play("DamagKRAB", PlayMode.StopAll);

                            yield return new WaitForSeconds(0.6f);
                            _playAnimBar.PlayAnimHPBAr();
                            mob.GetComponent<hp>().TextDamageToPlayer(mob, mob.GetComponent<damage>().ImpactDamageToPlayer().ToString(), Color.red);

                            yield return new WaitForSeconds(0.2f);
                            mob.GetComponent<Animation>().Play("IdelKRAB");
                            _playAnimBar.StopAnims();
                            AnimsSetDamageIsGone = true;
                        }
                        if (mob.GetComponentInParent<damage>().GoPlay == 2)
                        {

                            AnimsSetDamageIsGone = false;
                            yield return new WaitForSeconds(1.6f);
                            GetComponent<AudioSource>().PlayOneShot(AttackBee);
                            mob.GetComponent<Animation>().Play("DamagKRAB", PlayMode.StopAll);

                            yield return new WaitForSeconds(0.6f);
                            _playAnimBar.PlayAnimHPBAr();
                            mob.GetComponent<hp>().TextDamageToPlayer(mob, mob.GetComponent<damage>().ImpactDamageToPlayer().ToString(), Color.red);

                            yield return new WaitForSeconds(0.2f);
                            mob.GetComponent<Animation>().Play("IdelKRAB");
                            _playAnimBar.StopAnims();
                            AnimsSetDamageIsGone = true;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }//анимация атаки мобов
    private void SetTimeMobsAnim()
    {
        if (GameObject.FindGameObjectsWithTag("Mob").Length == 3)
        {
            pauseTime = 0.6f;//время на проигрывание анимаций урона
            oneMobTime = 1.5f;
            twoMobTime = 2.3f;
            threeMobTime = 3f;
        }
        else if (GameObject.FindGameObjectsWithTag("Mob").Length == 2)

        {
            if (!mobfirst)
            {
                print("!mobfirst");
                twoMobTime = 1.5f;
                threeMobTime = 2.3f;
            }
            else if (!mobsecond)
            {
                print("!mobsec");
                oneMobTime = 1.5f;
                threeMobTime = 2.3f;
            }
            else if (!mobthirst)
            {
                print("!mobthre");
                oneMobTime = 1.5f;
                twoMobTime = 2.3f;
            }


            //   oneMobTime = 0f;

        }
        else if (GameObject.FindGameObjectsWithTag("Mob").Length == 1)
        {
            pauseTime = 0.6f;//время на проигрывание анимаций урона
            oneMobTime = 1.5f;
            twoMobTime = 1.5f;
            threeMobTime = 1.5f;
        }
    }
    /// <summary>
    /// Анимация получения урона мобом
    /// </summary>
    /// <param name="mob"></param>
    /// <returns></returns>
    public IEnumerator AnimsGiveDamageMob(GameObject mob)
    {
        if (mob)
        {
            switch (mob.GetComponentInChildren<SkinnedMeshRenderer>().name)
            {
                case "Slizen":
                    GetComponent<AudioSource>().PlayOneShot(Painmob2);
                    mob.GetComponent<Animation>().Play("PainSliz", PlayMode.StopAll);
                    GetComponent<randomMob>().hideMenuBattle();
                    AnimsGetDamageIsGone = false;
                    //   yield return new WaitForSeconds(0.6f);

                    yield return new WaitForSeconds(1f);
                    mob.GetComponent<hp>().TextDamageToMob(mob, this.GetComponent<hp>().Damage.ToString(), Color.green);
                    mob.GetComponent<Animation>().Play("IdelSliz");
                    AnimsGetDamageIsGone = true;
                    GetComponent<randomMob>().hideMenuBattle();
                    break;
                case "TeloGADA":
                    GetComponent<AudioSource>().PlayOneShot(GADPain);
                    mob.GetComponent<Animation>().Play("PainGAD", PlayMode.StopAll);
                    GetComponent<randomMob>().hideMenuBattle();
                    AnimsGetDamageIsGone = false;
                    //     yield return new WaitForSeconds(0.6f);

                    yield return new WaitForSeconds(1f);
                    mob.GetComponent<hp>().TextDamageToMob(mob, this.GetComponent<hp>().Damage.ToString(), Color.green);
                    mob.GetComponent<Animation>().Play("IdelGAD");
                    AnimsGetDamageIsGone = true;
                    GetComponent<randomMob>().hideMenuBattle();
                    break;
                case "TeloGamunkula":
                    GetComponent<AudioSource>().PlayOneShot(Atmob2);
                    mob.GetComponent<Animation>().Play("PainGom", PlayMode.StopAll);
                    GetComponent<randomMob>().hideMenuBattle();
                    AnimsGetDamageIsGone = false;
                    //     yield return new WaitForSeconds(0.6f);

                    yield return new WaitForSeconds(1f);
                    mob.GetComponent<hp>().TextDamageToMob(mob, this.GetComponent<hp>().Damage.ToString(), Color.green);
                    mob.GetComponent<Animation>().Play("IdelGom");
                    AnimsGetDamageIsGone = true;
                    GetComponent<randomMob>().hideMenuBattle();
                    break;
			case "EvolGomun":
				GetComponent<AudioSource>().PlayOneShot(Atmob2);
				mob.GetComponent<Animation>().Play("PainEvolG", PlayMode.StopAll);
				GetComponent<randomMob>().hideMenuBattle();
				AnimsGetDamageIsGone = false;
				//     yield return new WaitForSeconds(0.6f);

				yield return new WaitForSeconds(1f);
				mob.GetComponent<hp>().TextDamageToMob(mob, this.GetComponent<hp>().Damage.ToString(), Color.green);
				mob.GetComponent<Animation>().Play("IdelEvolG");
				AnimsGetDamageIsGone = true;
				GetComponent<randomMob>().hideMenuBattle();
				break;
                case "myxa":
                    GetComponent<AudioSource>().PlayOneShot(GADDeath);
                    mob.GetComponent<Animation>().Play("PainMyxa", PlayMode.StopAll);
                    GetComponent<randomMob>().hideMenuBattle();
                    AnimsGetDamageIsGone = false;
                    //    yield return new WaitForSeconds(0.6f);

                    yield return new WaitForSeconds(1f);
                    mob.GetComponent<hp>().TextDamageToMob(mob, this.GetComponent<hp>().Damage.ToString(), Color.green);
                    mob.GetComponent<Animation>().Play("IdelMyxa");
                    AnimsGetDamageIsGone = true;
                    GetComponent<randomMob>().hideMenuBattle();
                    break;
                case "Krab":
                    GetComponent<AudioSource>().PlayOneShot(GADDeath);
                    mob.GetComponent<Animation>().Play("PainKRAB", PlayMode.StopAll);
                    GetComponent<randomMob>().hideMenuBattle();
                    AnimsGetDamageIsGone = false;
                    //   yield return new WaitForSeconds(0.6f);
                    print("giveDamage");
                    yield return new WaitForSeconds(1f);
                    mob.GetComponent<hp>().TextDamageToMob(mob, this.GetComponent<hp>().Damage.ToString(), Color.green);
                    mob.GetComponent<Animation>().Play("IdelKRAB");
                    AnimsGetDamageIsGone = true;
                    GetComponent<randomMob>().hideMenuBattle();
                    break;
                default:
                  
                    break;
            }
        }
    }//анимация урона по мобам
     /// <summary>
     /// Анимация смерти моба
     /// </summary>
     /// <param name="mob"></param>
     /// <returns></returns>
    public IEnumerator AnimDeath(GameObject mob)
    {
        switch (mob.GetComponentInChildren<SkinnedMeshRenderer>().name)
        {
            case "TeloGADA": mob.GetComponent<Animation>().Play("DeathGAD"); GetComponent<AudioSource>().PlayOneShot(GADDeath); break;
            case "Slizen": mob.GetComponent<Animation>().Play("DeathSliz"); GetComponent<AudioSource>().PlayOneShot(GADDeath); break;
            case "TeloGamunkula": mob.GetComponent<Animation>().Play("DeathGom"); GetComponent<AudioSource>().PlayOneShot(Atmob2); break;
		case "EvolGomun": mob.GetComponent<Animation>().Play("DeathEvolG"); GetComponent<AudioSource>().PlayOneShot(Atmob2); break;
            case "myxa": mob.GetComponent<Animation>().Play("DeathMyxa"); GetComponent<AudioSource>().PlayOneShot(GADDeath); break;
            case "Krab": mob.GetComponent<Animation>().Play("DeathKRAB"); GetComponent<AudioSource>().PlayOneShot(GADDeath); break;
            default:
                break;
        }
        yield return new WaitForSeconds(0.9f);

        tmpExpForBattle += mob.GetComponent<Mob>().ExpForMob; //повышение опыта за моба
        tmpGoldForBattle += mob.GetComponent<Mob>().mobForGold;
        Destroy(mob.gameObject);
    }//анимация смерти моба
    public void WhoIsDamag()//для привязке к кнопке
    {
        endCorutine = false;
        EndRound1 = false;
        StartCoroutine(WhoIsGo());
    }
    public void MobDamage(GameObject targetMob)
    {
        if (targetMob)
        {
            //TextDamage(targetMob);
            SetTimeMobsAnim();
            StartCoroutine(AnimsAtackMob(targetMob));
            print(targetMob.name + "   дамажит");
            targetMob.GetComponent<damage>().IsGo = true;
          //  playerStep = true;
        }
        else
        {
            print("моба нет так та");
        }
    }
    void Start()
    {
        _playAnimBar = FindObjectOfType<PlayAnimatorHPBar>();
        _mob = GameObject.FindObjectOfType<Mob>();
        _ph = GameObject.FindObjectOfType<PlayerHelper>();
        GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().IsGo = false;
        // AnimsIsGone = true;
    }
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Mob").Length == 0)
        {
            playerIsFirst = false;
            playerIsSecond = false;
            playerIsThr = false;
            EndRound1 = false;
        }
        else
        {
            if (AnimsSetDamageIsGone)

            {
                if (endCorutine && !GetComponent<damage>().checkMassAttack && !GetComponent<damage>().IsGo && playerStep &&!endRound)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().ShowMenuBattle();
                }


            }


        }
        if (EndRound1)
        {

            if (endCorutine)
            {
                WhoIsDamag();
            }
            // GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().showButtonEndRound();
            // WhoIsDamag();

        }
        else
        {

        }
        if (GetComponent<randomMob>().EnbBattle)
        {
            i = 0;
        }
        if (GetComponent<damage>().IsGo)
        {
            if (mobfirst)
            {
                if (!playerIsFirst && mobfirst.GetComponent<damage>().IsGo==false&& !endCorutine)
                {
                    GetComponent<randomMob>().hideMenuBattle();

                }
            }
        }

    }
    #endregion
}
