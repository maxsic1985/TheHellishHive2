//using UnityEngine;
//using System.Collections;

//public class SpeedHelper : MonoBehaviour
//{
//    private ihp _hp;
//    private int m1, m2, m3;
//    GameObject mobfirst;
//    GameObject mobsecond;
//    GameObject mobthirst;
//    public bool playerStep;
//    public bool playerIsFirst;
//    private int speedPlayer;
//    public bool playerIsSecond;
//    public bool playerIsThr;
//    public void GetNumberGo(GameObject one, GameObject two)
//    {
//        m1 = one.GetComponent<damage>().Speed;
//        m2 = two.GetComponent<damage>().Speed;
//        if (m1 > m2)
//        {
//            one.GetComponent<damage>().GoPlay = 1;
//            two.GetComponent<damage>().GoPlay = 2;
//        }
//        else
//        {
//            one.GetComponent<damage>().GoPlay = 2;
//            two.GetComponent<damage>().GoPlay = 1;
//        }
//        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Mob"))
//        {
//            if (item.GetComponent<damage>().GoPlay == 1)
//            {
//                mobfirst = item.gameObject;
//            }
//            if (item.GetComponent<damage>().GoPlay == 2)
//            {
//                mobsecond = item.gameObject;
//            }
//        }
//        speedPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().Speed;
//        if (speedPlayer > mobfirst.GetComponent<damage>().Speed)
//        {
//            playerIsFirst = true;
//        }
//        else if (speedPlayer > mobsecond.GetComponent<damage>().Speed)
//        {
//            playerIsSecond = true;
//        }
//        else playerIsThr = true;
//    }
//    public void GetNumberGo(GameObject one, GameObject two, GameObject three)
//    {
//        m1 = one.GetComponent<damage>().Speed;
//        m2 = two.GetComponent<damage>().Speed;
//        m3 = three.GetComponent<damage>().Speed;
//        if (m1 > m2)
//        {
//            if (m1 >= m3)
//            {
//                if (m2 >= m3)
//                {
//                    one.GetComponent<damage>().GoPlay = 1;
//                    two.GetComponent<damage>().GoPlay = 2;
//                    three.GetComponent<damage>().GoPlay = 3;
//                }
//                else
//                {
//                    one.GetComponent<damage>().GoPlay = 1;
//                    two.GetComponent<damage>().GoPlay = 3;
//                    three.GetComponent<damage>().GoPlay = 2;
//                }
//            }
//            else
//            {
//                one.GetComponent<damage>().GoPlay = 2;
//                two.GetComponent<damage>().GoPlay = 3;
//                three.GetComponent<damage>().GoPlay = 1;
//            }
//        }
//        else
//        {
//            if (m2 >= m3)
//            {
//                if (m3 >= m1)
//                {
//                    one.GetComponent<damage>().GoPlay = 3;
//                    two.GetComponent<damage>().GoPlay = 1;
//                    three.GetComponent<damage>().GoPlay = 2;
//                }
//                else
//                {
//                    one.GetComponent<damage>().GoPlay = 2;
//                    two.GetComponent<damage>().GoPlay = 1;
//                    three.GetComponent<damage>().GoPlay = 3;
//                }
//            }
//            else
//            {
//                one.GetComponent<damage>().GoPlay = 3;
//                two.GetComponent<damage>().GoPlay = 2;
//                three.GetComponent<damage>().GoPlay = 1;
//            }
//        }
//        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Mob"))
//        {
//            if (item.GetComponent<damage>().GoPlay == 1)
//            {
//                mobfirst = item.gameObject;
//            }
//            if (item.GetComponent<damage>().GoPlay == 2)
//            {
//                mobsecond = item.gameObject;
//            }
//            if (item.GetComponent<damage>().GoPlay == 3)
//            {
//                mobthirst = item.gameObject;
//            }
//        }
//        speedPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<damage>().Speed;
//        if (speedPlayer > mobfirst.GetComponent<damage>().Speed)
//        {
//            playerIsFirst = true;
//            playerStep = true;

//        }
//        else if (speedPlayer > mobsecond.GetComponent<damage>().Speed)
//        {
//            playerIsSecond = true;
//        }
//        else if (speedPlayer > mobthirst.GetComponent<damage>().Speed)
//        {
//            playerIsThr = true;
//        }

//    }
//    public GameObject WhoIsGo()
//    {
//        if (playerIsFirst)
//        {
//            if (playerStep)
//            {
//                GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().ShowMenuBattle();
//                print("игрок ходит");
//            }
//            if ((!GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().MenuBattle.activeSelf) & (!playerStep))
//            {
//                GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().ShowMenuNoBattle();
//                if (mobfirst)
//                {
//                    MobDamage(mobfirst);
//                    //  playerStep = true;
//                }
//                if (mobsecond)
//                {
//                    MobDamage(mobsecond);
//                    //  playerStep = true;
//                }
//                if (mobthirst)
//                {
//                    MobDamage(mobthirst);
//                    // playerStep = true;
//                }
//                print("Завершение хода");
//                playerStep = true;

//            }
//        }
//        else if (playerIsSecond)
//        {
//            if (!mobfirst.GetComponent<damage>().IsGo)//добавить и моб 1 не бил в этом раунде
//            {
//                MobDamage(mobfirst);
//            }

//            print("самый скоростной моб бьет первым");
//            if (playerStep)
//            {
//                GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().ShowMenuBattle();
//            }
//            if ((!GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().MenuBattle.activeSelf) & (!playerStep))
//            {
//                GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().ShowMenuNoBattle();
//                if (mobsecond)
//                {
//                    MobDamage(mobsecond);
//                    //  playerStep = true;
//                }
//                if (mobthirst)
//                {
//                    MobDamage(mobthirst);
//                    // playerStep = true;
//                }
//                print("Завершение хода");
//                playerStep = true;
//            }
//        }


//        else if (playerIsThr)
//        {
//            print("первые два быстрых моба бьют");
//            MobDamage(mobfirst, mobsecond);
//            if (playerStep)
//            {
//                GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().ShowMenuBattle();
//            }

//            if ((!GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().MenuBattle.activeSelf) & (!playerStep))
//            {
//                GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().ShowMenuNoBattle();
//                if (mobthirst)
//                {
//                    MobDamage(mobthirst);
//                    // playerStep = true;
//                }
//                print("Завершение хода");
//                playerStep = true;
//            }

//        }
//        else MobDamage(mobfirst, mobsecond, mobthirst); GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().ShowMenuBattle();



//        return mobfirst;
//    }
//    private void Anims(GameObject mob)
//    {
//        if (mob)
//        {


//            //  switch (mob.GetComponent<Animation>().clip.name)
//            switch (mob.GetComponentInChildren<SkinnedMeshRenderer>().name)
//            {
//                case "Slizen": mob.GetComponent<Animation>().Play("DamageSliz", PlayMode.StopAll); break;
//                case "TeloGamunkula": mob.GetComponent<Animation>().Play("DamagGom", PlayMode.StopAll); break;
//                default:
//                    break;
//            }
//        }
//    }
//    public void ContiniusDamag()
//    {
//        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>().MenuBattle.activeSelf)
//        {
//            if (GameObject.FindGameObjectsWithTag("Mob").Length == 2)
//            {
//                if (mobsecond & mobthirst)
//                {
//                    MobDamage(mobsecond, mobthirst);
//                    print("отвечают два моба");
//                }
//                else if (mobsecond & mobfirst)
//                {
//                    MobDamage(mobsecond, mobfirst);
//                    print("отвечают два моба");
//                }
//                else if (mobfirst & mobthirst)
//                {
//                    MobDamage(mobfirst, mobthirst);
//                    print("отвечают два моба");
//                }
//            }
//            if (GameObject.FindGameObjectsWithTag("Mob").Length == 1)
//            {
//                SelectMobDamag();

//            }
//            else if (GameObject.FindGameObjectsWithTag("Mob").Length == 0)
//            {
//                SelectMobDamag();

//            }
//        }
//    }

//    private void SelectMobDamag()
//    {
//        //if (mobfirst&!mobsecond&!mobthirst)
//        //{
//        //    MobDamage(mobfirst);
//        //    print("отвечает моб");
//        //}
//        if (mobsecond & !mobthirst)
//        {
//            MobDamage(mobsecond);
//            print("отвечает моб");
//        }
//        else if (mobthirst & !mobsecond)
//        {
//            MobDamage(mobthirst);
//            print("отвечает моб");
//        }
//    }

//    public void WhoIsDamag()//для привязке к кнопке
//    {
//        WhoIsGo();
//    }
//    public void MobDamage(GameObject m1, GameObject m2, GameObject m3)
//    {
//        if (m1 & m2 & m3)
//        {
//            Anims(m1);
//            Anims(m2);
//            Anims(m3);
//            print(m1.name + "   дамажит");
//            print(m2.name + "   дамажит");
//            print(m3.name + "   дамажит");
//            playerStep = true;
//        }
//        else if (m1 & m2)
//        {
//            Anims(m1);
//            Anims(m2);
//            print(m1.name + "   дамажит");
//            print(m2.name + "   дамажит");
//            playerStep = true;

//        }
//        else if (m1 & m3)
//        {
//            Anims(m1);
//            Anims(m3);
//            print(m1.name + "   дамажит");
//            print(m3.name + "   дамажит");
//            playerStep = true;
//        }
//        else if (m2 & m3)
//        {
//            Anims(m2);
//            Anims(m3);
//            print(m2.name + "   дамажит");
//            print(m3.name + "   дамажит");
//            playerStep = true;
//        }
//        else if (m1)
//        {
//            Anims(m1);
//            print(m1.name + "   дамажит");
//            playerStep = true;
//        }
//        else if (m2)
//        {
//            Anims(m2);
//            print(m2.name + "   дамажит");
//            playerStep = true;
//        }
//        else if (m3)
//        {
//            Anims(m3);
//            print(m3.name + "   дамажит");
//            playerStep = true;
//        }

//    }
//    public void MobDamage(GameObject m1, GameObject m2)
//    {

//        if (m1 & m2)
//        {
//            Anims(m1);
//            Anims(m2);
//            print(m1.name + "   дамажит");
//            print(m2.name + "   дамажит");


//            playerStep = true;
//        }
//        else if (m1)
//        {
//            Anims(m1);
//            print(m1.name + "   дамажит");
//            playerStep = true;
//        }
//        else if (m2)
//        {
//            Anims(m2);
//            print(m2.name + "   дамажит");
//            playerStep = true;
//        }

//    }
//    public void MobDamage(GameObject m1)
//    {
//        if (m1)
//        {
//            Anims(m1);
//            print(m1.name + "   дамажит");
//            playerStep = true;
//            m1.GetComponent<damage>().IsGo = true;
//        }

//    }



//    // Use this for initialization
//    void Start()
//    {
//        playerStep = true;

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (GameObject.FindGameObjectsWithTag("Mob").Length == 0)
//        {
//            playerIsFirst = false;
//            playerIsSecond = false;
//            playerIsThr = false;
//        }

//    }
//}
