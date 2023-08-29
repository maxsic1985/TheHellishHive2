//Скрипт для рандомного вызова моба при входе в триггер, моб вызывается при движении игрока
//1. скрипт необходимо повесить на игрока
//2. на скрипт повесить префаб вызываемого моба
//3. указать значения для регулирования для опредеелния значения(рандомного) задерки появления моба в триггере 
//4. Пересекаемому тригеру присвоить тэг RandomPlane
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class randomMob : MonoBehaviour
{
    public Transform effekt1;
    public Transform effekt2;
    public Transform effekt3;


    public AudioSource audioSource1;//для звука битвы.
    private int mu;//для звука битвы.
    public AudioClip muzon;//для звука битвы.
    private GameObject ff;
    public GameObject MobileControlCanvas;
    private Vector3 previus;
    public bool enter;//флаг входа в триггер
    private string coliderzon;
    private bool chkDst;
    public bool addMob;//сигнализация того что моб добавлен на сцену
    public float cnt;//счетчик до появления моба
    private int previusValue;//предыдущее значение координат иигрока для определения стоит он или двигается
    private int counter;//небольшая задержка для определения состояния игрока
    public bool stay;//игрок не двигается
    public bool mobIsKilled;//убили мобов в триггере?
    public int minRndplace1;//задержка на появление моба (минимальное значение диапазона рандома Random.Range(min,max)) 
    public int maxRndplace1;//задержка на появление моба (максимальное значение диапазона рандома Random.Range(min,max)) 
    public GameObject prefabMobType1;//префаб моба
    public GameObject prefabMobType2;//easy
    public GameObject prefabMobType3;//easy
    public GameObject prefabMobType4;//midle
    public GameObject prefabMobType5;//midle
    public GameObject prefabMobType6;//midle
    public GameObject prefabMobType7;//hard
    public GameObject prefabMobType8;//hard
    public GameObject prefabMobType9;//hard
    public GameObject prefabMobType10;//префаб моба
    public GameObject prefabMobType11;//easy
    public GameObject prefabMobType12;//easy
    public GameObject prefabMobType13;//префаб моба
    public GameObject prefabMobType14;//easy
    public GameObject prefabMobType15;
    public GameObject Boss;
    private Transform pos;
    private ihp _hp;
    public Transform Left;
    public Transform Right;
    public Transform Center;
    public GameObject mobLeft;
    public GameObject mobCentr;
    public GameObject mobRight;
    public GameObject MenuNoBattle;
    public GameObject MenuBattle;
    public GameObject ButtonEndRound;
    private bool enbBattle;
    public bool boosIsView;
    private bool isTransformToBattle;
    private float timer;
    private Transform pointOnBoss;//точка около босса
    private Transform bossTransform;//трансформа босса
    public bool MenuBattleBool;
    private int IKillBoss = 0;

    private MenuManager MM;

    public bool EnbBattle
    {
        get { return enbBattle; }
        set { enbBattle = value; }
    }
    public bool triggerBossBool { get; private set; }
    //м-д определения состояния игрока (стоит, двигается)
    //если в течении времени counter положение игрока не изменяется по осям x или z 
    //то устонавливается флаг stay
    //иначе игрок в движении 
    void OnStay(int position)//при вызове метода передается координата отслеживаемого объекта (игрока)
    {
        if (position == previusValue)//предыдущее положение равно текущему?
        {

            previusValue = position;//если да то увеличиваем counter на 1 
            counter += 1;//до тех пор пока она не более либо равен 50
            if (counter >= 50)
            {
                stay = true;//игрок стоит!
            }
        }
        else
        {
            stay = false;
            counter = 0;
            previusValue = position;

        }
    }
    //инициализация отсчета времени до вставки моба в триггере
    private void InitCntToInstMob(int min, int max)
    {
        cnt = Random.Range(min, max);//инициализация счетчика до появления моба 
    }
    void OnTriggerEnter(Collider other)
    {
        //  RaycastHit hit;
        // Physics.Raycast(transform.position, transform.forward, out hit);
        //float dist = Vector3.Distance(transform.position, hit.point);
        //  print(dist);


        enter = true;//игрок в тригере
        if (other == GameObject.Find("TriggerMobEasy").GetComponent<Collider>())
        {

            InitCntToInstMob(minRndplace1, maxRndplace1);//установка границ рандома времени до появления моба
            coliderzon = "TriggerMobEasy";
            print(coliderzon);
        }

        else if (other == GameObject.Find("TriggerMobMidle").GetComponent<Collider>())
        {

            InitCntToInstMob(minRndplace1, maxRndplace1);//установка границ рандома времени до появления моба
            coliderzon = "TriggerMobMidle";
            print(coliderzon);
        }

        else if (other == GameObject.Find("TriggerMobHard").GetComponent<Collider>())
        {

            InitCntToInstMob(minRndplace1, maxRndplace1);//установка границ рандома времени до появления моба
            coliderzon = "TriggerMobHard";
            print(coliderzon);
        }
        else if (other == GameObject.Find("TriggerMobHard2").GetComponent<Collider>())
        {

            InitCntToInstMob(minRndplace1, maxRndplace1);//установка границ рандома времени до появления моба
            coliderzon = "TriggerMobHard2";
            print(coliderzon);
        }
        else if (other == GameObject.Find("TriggerMobHard3").GetComponent<Collider>())
        {

            InitCntToInstMob(minRndplace1, maxRndplace1);//установка границ рандома времени до появления моба
            coliderzon = "TriggerMobHard3";
            print(coliderzon);
        }
        else if (other == GameObject.Find("TriggerBoss").GetComponent<Collider>())
        {
            coliderzon = "TriggerBoss";
            cnt = 0.25f;//уменьшить cnt  чтобы не ждать все время
            print(coliderzon);
        }
    }
    void OnTriggerExit(Collider other)
    {
        other = GameObject.FindGameObjectWithTag("RandomPlane").GetComponent<Collider>();
        enter = false;
        mobIsKilled = false;
        coliderzon = "ddddd";
        print("ExitTrigger");


    }
    private void transformToBatleField()
    {
        isTransformToBattle = true;
        Transform cc = GameObject.FindGameObjectWithTag("battle").GetComponent<Transform>();
        transform.rotation = Quaternion.FromToRotation(Vector3.up, cc.forward);
        transform.position = new Vector3(cc.position.x, cc.position.y, cc.position.z);
    }
    public void transBackFromBatleField()
    {
        if (isTransformToBattle)
        {
            ScreenFader.Fader(3, Color.black);
            transform.position = previus;
            transform.rotation = Quaternion.identity;
            this.GetComponent<CharacterController>().enabled = true;
            isTransformToBattle = false;
        }


    }
    private void CreateMob(GameObject mob1)//метод вставки 1 моба
    {
        print("CreateMob");
        HideMenuNoBattle();

        addMob = true;
        GameObject m1 = (GameObject)Instantiate(mob1, new Vector3(this.transform.position.x + this.transform.forward.x * 2f, 0, this.transform.position.z + this.transform.forward.z * 2), this.transform.rotation);
        m1.transform.SetParent(GameObject.FindGameObjectWithTag("Centr").GetComponent<Transform>());
        this.GetComponent<SpeedHelper>().GetNumberGo(m1);
        StartCoroutine(CameraRotateToDown());//опускаем камеру на мобов
        this.GetComponent<SpeedHelper>().WhoIsDamag();


    }
    private void CreateMob(GameObject mob1, GameObject mob2)//переопределяем метод для вставки 2 мобов
    {
        print("CreateMob");
        ff.SetActive(false);
        HideMenuNoBattle();
        addMob = true;
        GameObject m1 = (GameObject)Instantiate(mob1, new Vector3(this.transform.position.x + this.transform.forward.x * 2f, 0, this.transform.position.z + this.transform.forward.z * 2), this.transform.rotation);
        m1.transform.SetParent(GameObject.FindGameObjectWithTag("Centr").GetComponent<Transform>());
        GameObject m2 = (GameObject)Instantiate(mob2, new Vector3(this.transform.position.x + this.transform.forward.x * 2f + this.transform.right.x * 0.7f, 0, this.transform.forward.z * 2 + this.transform.position.z + this.transform.right.z * 0.7f), this.transform.rotation);
        m2.transform.SetParent(GameObject.FindGameObjectWithTag("Right").GetComponent<Transform>());
        this.GetComponent<SpeedHelper>().GetNumberGo(m1, m2);
        this.GetComponent<SpeedHelper>().WhoIsDamag();
        StartCoroutine(CameraRotateToDown());
    }
    private void CreateMob(GameObject mob1, GameObject mob2, GameObject mob3)//переопределяем метод для вставки 3 мобв
    {
        print("CreateMob");

        addMob = true;
        HideMenuNoBattle();
        GameObject m1 = (GameObject)Instantiate(mob1, new Vector3(this.transform.position.x + this.transform.forward.x * 2f, 0, this.transform.position.z + this.transform.forward.z * 2), this.transform.rotation);
        m1.transform.SetParent(GameObject.FindGameObjectWithTag("Centr").GetComponent<Transform>());
        GameObject m2 = (GameObject)Instantiate(mob2, new Vector3(this.transform.position.x + this.transform.forward.x * 2f + this.transform.right.x * 0.7f, 0, this.transform.forward.z * 2 + this.transform.position.z + this.transform.right.z * 0.7f), this.transform.rotation);
        m2.transform.SetParent(GameObject.FindGameObjectWithTag("Right").GetComponent<Transform>());
        m2.transform.rotation = m1.transform.rotation;
        GameObject m3 = (GameObject)Instantiate(mob3, new Vector3(this.transform.position.x + this.transform.forward.x * 2f + this.transform.right.x * -0.7f, 0, this.transform.forward.z * 2 + this.transform.position.z + this.transform.right.z * -0.7f), this.transform.rotation);
        m3.transform.SetParent(GameObject.FindGameObjectWithTag("Left").GetComponent<Transform>());
        m3.transform.rotation = m2.transform.rotation;
        this.GetComponent<SpeedHelper>().GetNumberGo(m1, m2, m3);
        this.GetComponent<SpeedHelper>().WhoIsDamag();
        StartCoroutine(CameraRotateToDown());
    }
    //методы вешаются на кнопки на сцене, нанесение урона
    public void damagemobLeft()
    {
        if (GameObject.FindGameObjectWithTag("Left").GetComponentInChildren<Mob>() != null)
        {
            if (this.GetComponent<damage>().typeAttack == 1)
            {
                GameObject.FindGameObjectWithTag("Left").GetComponentInChildren<hp>().ImpactDamageOnMob(effekt1);
            }
            else if (this.GetComponent<damage>().typeAttack == 2)
            {
                GameObject.FindGameObjectWithTag("Left").GetComponentInChildren<hp>().ImpactDamageOnMob(effekt2);
            }

            this.GetComponent<damage>().IsGo = true;
            this.GetComponent<SpeedHelper>().playerStep = false;
            hideMenuBattle();
            this.GetComponent<SpeedHelper>().WhoIsDamag();
        }
    }
    public void damagenobRight()
    {
        if (GameObject.FindGameObjectWithTag("Right").GetComponentInChildren<Mob>() != null)
        {
            if (this.GetComponent<damage>().typeAttack == 1)
            {
                GameObject.FindGameObjectWithTag("Right").GetComponentInChildren<hp>().ImpactDamageOnMob(effekt1);
            }
            else if (this.GetComponent<damage>().typeAttack == 2)
            {
                GameObject.FindGameObjectWithTag("Right").GetComponentInChildren<hp>().ImpactDamageOnMob(effekt2);
            }
            this.GetComponent<damage>().IsGo = true;
            this.GetComponent<SpeedHelper>().playerStep = false;
            hideMenuBattle();
            this.GetComponent<SpeedHelper>().WhoIsDamag();
        }
    }
    public void damagenobCentr()
    {
        if (GameObject.FindGameObjectWithTag("Centr").GetComponentInChildren<Mob>() != null)
        {
            if (this.GetComponent<damage>().typeAttack == 1)
            {
                GameObject.FindGameObjectWithTag("Centr").GetComponentInChildren<hp>().ImpactDamageOnMob(effekt1);
            }
            else if (this.GetComponent<damage>().typeAttack == 2)
            {
                GameObject.FindGameObjectWithTag("Centr").GetComponentInChildren<hp>().ImpactDamageOnMob(effekt2);
            }
            print("Centr");
            this.GetComponent<damage>().IsGo = true;
            this.GetComponent<SpeedHelper>().playerStep = false;
            hideMenuBattle();
            this.GetComponent<SpeedHelper>().WhoIsDamag();
        }

    }
    //Спрятать кнопки Атака
    public void HideButtoms()
    {
        mobLeft.SetActive(false);
        mobRight.SetActive(false);
        mobCentr.SetActive(false);
    }
    public void ShowButtons()
    {
        GameObject l = GameObject.FindGameObjectWithTag("Left");
        GameObject R = GameObject.FindGameObjectWithTag("Right");
        GameObject C = GameObject.FindGameObjectWithTag("Centr");

        if (l.GetComponentInChildren<Mob>() != null)
        {
            mobLeft.SetActive(true);
        }
        if (R.GetComponentInChildren<Mob>() != null)
        {
            mobRight.SetActive(true);
        }
        if (C.GetComponentInChildren<Mob>() != null)
        {
            mobCentr.SetActive(true);
        }

    }
    //вызов на сцену мобов различной сложности
    private void AddMobsOnScene(string difficult, int minCnt)//сложность, минимальное количество
    {

        int CntMobOnScene;
        CntMobOnScene = Random.Range(minCnt, 4);
        int mobnumber1;
        int mobnumber2;
        int mobnumber3;
        GameObject[] mbs = { prefabMobType1, prefabMobType2, prefabMobType3, prefabMobType4, prefabMobType5, prefabMobType6, prefabMobType7, prefabMobType8, prefabMobType9, prefabMobType10, prefabMobType11, prefabMobType12, prefabMobType13, prefabMobType14, prefabMobType15 };//помещаем префабы в массив чтобы далее возможно было их оттуда рандомно доставать по индексу расположения объекта в массиве {0,1,2,3,4......}
        switch (difficult)
        {
            case "Low":
                mobnumber1 = Random.Range(0, 3);
                mobnumber2 = Random.Range(0, 3);
                mobnumber3 = Random.Range(0, 3);
                break;
            case "Midle":
                mobnumber1 = Random.Range(3, 6);
                mobnumber2 = Random.Range(3, 6);
                mobnumber3 = Random.Range(3, 6);
                break;
            case "Hard":
                mobnumber1 = Random.Range(6, 9);
                mobnumber2 = Random.Range(6, 9);
                mobnumber3 = Random.Range(6, 9);
                break;
            case "varyHard":
                mobnumber1 = Random.Range(9, 12);
                mobnumber2 = Random.Range(9, 12);
                mobnumber3 = Random.Range(9, 12);
                break;
            case "vary2Hard":
                mobnumber1 = Random.Range(12, 15);
                mobnumber2 = Random.Range(12, 15);
                mobnumber3 = Random.Range(12, 15);
                break;

            default:
                mobnumber1 = Random.Range(0, 15);
                mobnumber2 = Random.Range(0, 15);
                mobnumber3 = Random.Range(0, 15);
                break;
        }
        if ((GameObject.FindGameObjectWithTag("Mob") == null) && (!addMob))//моба нет на сцене?
        {
            print(difficult);
            if (CntMobOnScene == 1)
            {
                CreateMob(mbs[mobnumber1]);
            }
            else if (CntMobOnScene == 2)
            {

                CreateMob(mbs[mobnumber1], mbs[mobnumber2]);
            }
            else if (CntMobOnScene == 3)
            {

                CreateMob(mbs[mobnumber1], mbs[mobnumber2], mbs[mobnumber3]);//создать 3  рандомных типов мобов
            }
        }
        if (GameObject.FindGameObjectWithTag("Mob") != null)
        {

            mbs = GameObject.FindGameObjectsWithTag("Mob");
            //Quaternion rotate = Quaternion.Euler(0, 0, 0);
            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().rotation = rotate;
            this.GetComponent<FirstPersonController>().enabled = false;
            MobileControlCanvas.GetComponentInChildren<Canvas>().enabled = false;
            this.GetComponentInChildren<Camera>().fieldOfView = 50; //чуть приблизить камеру

        }
        else
        {
            MobileControlCanvas.GetComponentInChildren<Canvas>().enabled = true;
            this.GetComponent<FirstPersonController>().enabled = true;
            this.GetComponentInChildren<Camera>().fieldOfView = 60;
        }
    }
    void Start()
    {
        MM = new MenuManager();
        if (PlayerPrefs.GetInt("IKillBoss") == 1)
        {
            Destroy(GameObject.Find("TriggerBoss"));
            Boss.SetActive(false);
        }
        //отключаем кнопки атаки
        mobLeft.SetActive(false);
        mobRight.SetActive(false);
        mobCentr.SetActive(false);
        hideButtonEndRound();
        ff = GameObject.FindGameObjectWithTag("ForFader");
        ff.SetActive(false);
        timer = 0;
        pointOnBoss = GameObject.FindGameObjectWithTag("respToBoss").GetComponent<Transform>();
        bossTransform = Boss.GetComponent<Transform>();

    }
    void Update()
    {


        if (addMob == true & mu == 0)//для звука битвы.
        {

            audioSource1.PlayOneShot(muzon);//для звука битвы.
            mu = 1;//для звука битвы.

        }


        if (addMob == false)//для звука битвы.
        {
            audioSource1.Stop();//для звука битвы.
            mu = 0;//для звука битвы.

        }

        //определяем состояние игрока, движение остановлен
        OnStay((int)this.transform.position.x + (int)this.transform.position.z);//передаем в метод сумму позиции игрока по x и по z (когда игрок стоит по этим осям нет изменений)
        if (enter)//игрок в тригере?
        {

            if ((cnt > -1) && (!stay))//счетчик не вышел и игрок двигается
            {
                // if (chkDst)
                // {
                cnt = cnt - Time.deltaTime;

                // }
                //уменьшаем счетчик каждую секунду
            }
        }
        if (cnt < 0)//&&(chkDst))//счетчик меньше 0
        {

            switch (coliderzon)
            {
                case "TriggerMobEasy":

                    AddMobsOnScene("Low",1); //вызов на сцену легких мобов, в параметре указывается сложность (Low, Midle , Hard)
                    transformToBatleField();
                    break;
                case "TriggerMobMidle":
                    AddMobsOnScene("Midle", 1);//вызов на сцену легких мобов, в параметре указывается сложность (Low, Midle , Hard)
                    transformToBatleField();
                    break;
                case "TriggerMobHard":
                    AddMobsOnScene("Hard", 2);//вызов на сцену легких мобов, в параметре указывается сложность (Low, Midle , Hard)
                    transformToBatleField();
                    break;
                case "TriggerMobHard2":
                    AddMobsOnScene("varyHard", 2);//вызов на сцену легких мобов, в параметре указывается сложность (Low, Midle , Hard)
                    transformToBatleField();
                    break;
                case "TriggerMobHard3":
                    AddMobsOnScene("vary2Hard", 3);//вызов на сцену легких мобов, в параметре указывается сложность (Low, Midle , Hard)
                    transformToBatleField();
                    break;
                case "TriggerBoss":
                    if (!triggerBossBool)
                    {
                        triggerBossBool = true;

                    }

                    ToBoss();

                    timer += Time.deltaTime;//запустить таймер который считает время проведенное в колайдере, для проигрывания анимации вылазки босса
                    break;
                default:
                    triggerBossBool = false;
                    break;
            }


            //если моб уничтожен спрятать кнопку
            HideButtoms();

            // ShowMenuNoBattle();

        }
        else
        {
            previus = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
        if ((GameObject.FindGameObjectWithTag("Mob") == null) && (addMob))//убили мобов, продолжаем движение
        {
            print("убили мобов");

            transBackFromBatleField();
            InitCntToInstMob(minRndplace1, maxRndplace1);
            addMob = false;
            mobIsKilled = true;
            ShowMenuNoBattle();
            EnbBattle = true;
           // MobileControlCanvas.GetComponentInChildren<Canvas>().enabled = true;
            //показать меню боя    
        }
        if (GameObject.FindGameObjectWithTag("Mob") == null)
        {
            //  print("itttt");
            this.GetComponent<FirstPersonController>().enabled = true;
            MobileControlCanvas.SetActive(true);
        }
    }
    ////private bool ChkDist()
    ////    {
    ////        RaycastHit hit;
    ////        RaycastHit hitR;
    ////        Physics.Raycast(transform.position, transform.forward, out hit);
    ////        Physics.Raycast(transform.position, transform.right, out hitR);
    ////        float dist = Vector3.Distance(transform.position, hit.point);
    ////        float distR = Vector3.Distance(transform.position, hitR.point);
    ////        print(distR);
    ////        if ((dist>5f)&&(distR>1.5f))
    ////        {
    ////            chkDst = true;
    ////        }
    ////        else
    ////        {
    ////            chkDst = false;
    ////        }
    ////        return chkDst;
    ////    }//проверка на растояние от стены
    public void ShowMenuBattle()
    {
        MenuNoBattle.SetActive(false);
        MenuBattle.SetActive(true);
        MenuBattleBool = true;
    }
    public void hideMenuBattle()
    {
        print("BattleMenu");
        // MenuNoBattle.SetActive(true);
        MenuBattle.SetActive(false);
        MenuBattleBool = false;
    }
    public void ShowMenuNoBattle()
    {
        MenuNoBattle.SetActive(true);
        MenuBattle.SetActive(false);
        hideButtonEndRound();
        MenuBattleBool = false;
    }
    public void HideMenuNoBattle()
    {
        MenuNoBattle.SetActive(false);
    }
    public void hideButtonEndRound()
    {
        // ButtonEndRound.SetActive(true);
        //   ButtonEndRound.SetActive(false);
    }
    public void showButtonEndRound()
    {
        ButtonEndRound.SetActive(false);
        ButtonEndRound.SetActive(true);
    }
    private IEnumerator CameraRotateToDown()
    {
        ff.SetActive(true);
        ScreenFader.Fader(2, Color.black);//затемнение экрана
        float fadeTime = 1;//скорость анимации поворота камеры 
        float rate = 1.0f / fadeTime;
        float progress = 0.0f;
        while (progress < 1.0)
        {
            //Color tmpColor;
            //  tmpColor= GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Renderer>().material.color;
            //  GetComponent<Text>().color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, Mathf.Lerp(startAlpha, 0, progress));
            //  tmpColor.a = Mathf.Lerp(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Renderer>().material.color.a, 1.0f, Time.deltaTime);
            Quaternion rotate = Quaternion.Euler(Mathf.Lerp(0, 20, progress), 0, 0);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().localRotation = rotate;
            progress += rate * Time.deltaTime;
            yield return null;
        }

    }
    private void ToBoss()//выползание босса и начало битвы
    {
        if (Boss != null)//Если босс жив
        {
            HideMenuNoBattle();//спрятать кнопки
            transform.rotation = Quaternion.Euler(0, 270, 0);//повернуть игрока к мобу
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().rotation = Quaternion.Euler(0, -90, 0);//поднять камеру

            if (!boosIsView)//если босс еще не выполз, начинаем плавно перемещать игрока к точке перед боссом
            {
                if ((int)transform.position.x != (int)pointOnBoss.transform.position.x)//игрок еще не перемещен к точке
                {
                    transform.position = Vector3.MoveTowards(transform.position, pointOnBoss.transform.position, Time.deltaTime * 4);//придать движение к боссу
                    transform.LookAt(bossTransform);//смотреть на босса
                }
                else
                {
                    Boss.GetComponent<Animation>().Play("KRABPriv", PlayMode.StopAll);//анимация выхода босса
                    if (timer > 5f)//ждем время пока не закончится анимация и начинаем бой
                    {
                        Boss.tag = "Mob";
                        this.GetComponent<SpeedHelper>().GetNumberGo(Boss);
                        this.GetComponent<SpeedHelper>().WhoIsDamag();
                        addMob = true;
                        boosIsView = true;

                    }
                }
            }
            else
            {
                Boss.transform.SetParent(GameObject.FindGameObjectWithTag("Centr").GetComponent<Transform>());//переместить босса под место центрального моба
                ff.SetActive(true);
            }
        }
        else
        {
            // если босс уничтожен то удаляем триггер и сохранить инфу о том, что босс уничтожен
            if (GameObject.Find("TriggerBoss") != null)
            {
                Destroy(GameObject.Find("TriggerBoss"));
                print("Do itto it");
                IKillBoss = 1;
                AchievmentManager.Instance.KillBoss = true;
                PlayerPrefs.SetInt("IKillBoss", IKillBoss);
                PlayerPrefs.Save();
            }
        }
    }
}

