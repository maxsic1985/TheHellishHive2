using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
public class AchievmentManager : MonoBehaviour
{
	public GameObject www;
	public GameObject Jo2;
	public AudioClip Exitmenu3;//звук открытия
	public AudioSource ExitSource3;
    #region Variable
    public GameObject achievmentPrefab2;//префаб окна достижения

    public GameObject achievmentCanvas;//канвас с окном достижений

    public Sprite[] sprites;//массив спрайтов для достижений в префабе

    public ScrollRect scrollRect;

    public GameObject achievmentMenu;

    private ArchievmentBtn _archBtn;

    public GameObject visualAchievment; //префаб всплывающего достижения

    public Dictionary<string, Achievment> achievments = new Dictionary<string, Achievment>(); //словарь всех достижений

    public Sprite unlockedSprite;

    public Text textPoint;

    private static AchievmentManager instance;

    public static AchievmentManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<AchievmentManager>();
            }
            return instance;
        }
    }
    private Scene _scene;
    private randomMob _rmob;
    private PlayerHelper _ph;
    private bool useTrava = false;
    private bool chestBox = false;
    private bool key;
    private bool secretDore;
    private bool pobeg;
    private bool killBoss;
    private int _cntMob;
    #endregion
    public bool UseTrava
    {
        get
        {
            return useTrava;
        }

        set
        {
            useTrava = value;
        }
    }
    public bool ChestBox
    {
        get
        {
            return chestBox;
        }

        set
        {
            chestBox = value;
        }
    }
    public bool Key
    {
        get
        {
            return key;
        }

        set
        {
            key = value;
        }
    }
    public bool SecretDore
    {
        get
        {
            return secretDore;
        }

        set
        {
            secretDore = value;
        }
    }
    public bool Pobeg
    {
        get
        {
            return pobeg;
        }

        set
        {
            pobeg = value;
        }
    }
    public bool KillBoss
    {
        get
        {
            return killBoss;
        }

        set
        {
            killBoss = value;
        }
    }
    public int CntMob
    {
        get
        {
            return _cntMob;
        }

        set
        {
            _cntMob = value;
        }
    }
    void Start()
    {
        _scene = SceneManager.GetActiveScene();
       

        _archBtn = GameObject.Find("BaseButton").GetComponent<ArchievmentBtn>();
        CreateAchievment("Base", "Искатель приключений", "Спуститься в лабиринт", 1, 0);
        CreateAchievment("Base", "Первый враг", "Встретить врага", 5, 1);
        CreateAchievment("Base", "Кладоискатель", "Найти ящик", 10, 2);
        CreateAchievment("Base", "Тайны лабиринта", "Открыть потайную дверь тайной кнопкой ", 50, 3);
        CreateAchievment("Base", "Найти ключ", "Найти ключ от главной двери", 150, 4);
        CreateAchievment("Base", "Везунчик", "Успешно сбежать с поля боя", 50, 5);
        CreateAchievment("Base", "Охотник за сокровищами", "Собрать 1000 монет", 200, 6);
        CreateAchievment("Base", "Подмастерье", "Получить пятый уровень", 50, 7);
        CreateAchievment("Base", "Мастер", "Получить десятый уровень", 150, 8);

        CreateAchievment("Warrior", "Воин", "Убить врага", 10, 9);
        CreateAchievment("Warrior", "Гладиатор", "Убить десять врагов", 50, 10);
        CreateAchievment("Warrior", "Алхимик", "Использовать траву в бою", 50, 11);
        CreateAchievment("Warrior", "Рыцарь", "Убить Босса", 150, 12);
        CreateAchievment("Warrior", "Мясник", "Убить 100 врагов", 500, 13);

        _archBtn.Click();


        _rmob = FindObjectOfType<randomMob>();
        _ph = FindObjectOfType<PlayerHelper>();

        foreach (GameObject achievmentList in GameObject.FindGameObjectsWithTag("AchievmentList"))///Прячем вторую ветку достижений по умолчанию
        {
            achievmentList.SetActive(false);
        }
        achievmentCanvas.SetActive(false);
        print(achievments.Count);
    }
    /// <summary>
    /// Создание нового достижения, добавление в словарь 
    /// </summary>
    /// <param name="parent">родительский компонент для трансформы</param>
    /// <param name="tittle">название достижения</param>
    /// <param name="description">описание</param>
    /// <param name="point">количество награды очков</param>
    /// <param name="spriteindex">последовательный номер спрайта из AchievmentManager</param>
    private void CreateAchievment(string parent, string tittle, string description, int point, int spriteindex)
    {

        GameObject achievment = (GameObject)Instantiate(achievmentPrefab2);

        Achievment newAchievment = new Achievment(tittle, description, point, achievment, spriteindex);

        achievments.Add(tittle, newAchievment);

        SetAchievmentInfo(parent, achievment, tittle);


    }
    /// <summary>
    /// Выставление параметров для нового достижения 
    /// </summary>
    /// <param name="parent">родительский объект</param>
    /// <param name="achievment">объект достижений</param>
    /// <param name="tittle">название достижения</param>
    public void SetAchievmentInfo(string parent, GameObject achievment, string tittle)
    {
        achievment.transform.SetParent(GameObject.Find(parent).transform);
        achievment.transform.localScale = new Vector3(1, 1, 1);
        #region  берет дочерние по порядку компоненты из префаба achievmentPrefab2  HeadText, DescriptionText, Point, CostImage
        achievment.transform.GetChild(0).GetComponent<Text>().text = tittle;
        achievment.transform.GetChild(1).GetComponent<Text>().text = achievments[tittle].Description;
        achievment.transform.GetChild(2).GetComponent<Text>().text = achievments[tittle].Points.ToString();
        achievment.transform.GetChild(3).GetComponent<Image>().sprite = sprites[achievments[tittle].SpriteIndex];
        #endregion
    }
    /// <summary>
    /// Переключение окон достижений, обычные/военные
    /// Вешается на кнопки переключений rfntujhbq
    /// </summary>
    /// <param name="button">объект на котором весит компонент ArchievmentBtn</param>
    public void ChangeCategory(GameObject button)
	{
		ExitSource3.GetComponent<AudioSource>().PlayOneShot(Exitmenu3);
        ArchievmentBtn abtn = button.GetComponent<ArchievmentBtn>();
        scrollRect.content = abtn.achievmentList.GetComponent<RectTransform>();
        abtn.Click();
        _archBtn.Click();
        _archBtn = abtn;
    }
    /// <summary>
    /// Показать меню достижений привязывается к кнопке
    /// </summary>
    public void ShowMenu()
    {
        if (achievmentCanvas.activeSelf == false)
        {
			Jo2.GetComponentInParent<Canvas>().enabled = false;
			www.GetComponent<FirstPersonController> ().m_RunSpeed = 0;
			ExitSource3.GetComponent<AudioSource>().PlayOneShot(Exitmenu3);
            achievmentCanvas.SetActive(true);
        }
        else
        {
			Jo2.GetComponentInParent<Canvas>().enabled = true;
			www.GetComponent<FirstPersonController> ().m_RunSpeed = 2.3f;
			ExitSource3.GetComponent<AudioSource>().PlayOneShot(Exitmenu3);
            achievmentCanvas.SetActive(false);
        }
    }
    /// <summary>
    /// Открыть достижение
    /// </summary>
    /// <param name="tittle">название достижения</param>
    public void EarnAcheivment(string tittle)
    {
        if (achievments[tittle].EarnAchievment())
        {
            GameObject ach = (GameObject)Instantiate(visualAchievment);
            SetAchievmentInfo("EarnAchievment", ach, tittle);
            textPoint.text = "Очков Достижений:" + PlayerPrefs.GetInt("Points");
            StartCoroutine(HideAchievment(ach));
        }
    }
    /// <summary>
    /// Удалить всплывающее окно достижений через некоторое время
    /// </summary>
    /// <param name="visAch"></param>
    /// <returns></returns>
    public IEnumerator HideAchievment(GameObject visAch)
    {
        yield return new WaitForSeconds(3);
        Destroy(visAch);
    }
    void Update()
    {

        ///Base
        if (_scene.name == "Lvl1")
        {
            EarnAcheivment("Искатель приключений");
        }
        if (_rmob.addMob)
        {
            EarnAcheivment("Первый враг");
        }
        if (chestBox)
        {
            EarnAcheivment("Кладоискатель");
        }
        if (secretDore)
        {
            EarnAcheivment("Тайны лабиринта");
        }

        if (key)
        {
            EarnAcheivment("Найти ключ");
        }
        if (Pobeg)
        {
            EarnAcheivment("Везунчик");
        }
        if (_ph.GoldCur >= 1000)
        {
            EarnAcheivment("Охотник за сокровищами");
        }
        if (_ph.LvlPlayer >= 5)
        {
            EarnAcheivment("Подмастерье");
        }
        if (_ph.LvlPlayer >= 10)
        {
            EarnAcheivment("Мастер");
        }
        ///warriors
        if (_cntMob >= 1)
        {
            EarnAcheivment("Воин");
        }

        if (_cntMob >= 10)
        {
            EarnAcheivment("Гладиатор");
        }

        if (UseTrava)
        {
            EarnAcheivment("Алхимик");
        }
        if (KillBoss)
        {
            EarnAcheivment("Рыцарь");
        }

        if (_cntMob >= 100)
        {
            EarnAcheivment("Мясник");
        }


        ///Control
        if (Input.GetKeyDown(KeyCode.S))
        {
            ShowMenu();
        }
    }
}
