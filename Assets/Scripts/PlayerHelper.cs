using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHelper : MonoBehaviour
{
    #region Variables
    private static PlayerHelper instance;
    /// <summary>
    /// Уровень повысился
    /// </summary>
    private bool getLVL;
    /// <summary>
    /// Индикатор маны
    /// </summary>
    public Slider manaPlayer;
    /// <summary>
    /// Индикатор опыта
    /// </summary>
    public Slider expPlayer;
    /// <summary>
    /// Индикатор жизней
    /// </summary>
    public Slider hPText;
    //ссылки на текстовые объекты
    public Text GoldText;
    public Text AtackPlayerText;
    public Text DefPlayerText;
    public Text texlvl;//UI текст на который вывести lvl
    public Text texlvl2;//UI текст на который вывести lvl
    public Text HPtext;
    public Text HPtextBAR;
    public Text MPtextBAR;
    public Text MPtext;
    public Text LVLMenutext;
    public Text namePlayer;
    public Text statsText;

    public GameObject TextPobeg;
    private Text _textPobeg;
    float timer = 0;



    //звук кнопки открытие инвентаря
    public AudioClip buttu1;
    public AudioSource sfx2;//звук кнопки открытие инвентаря
    /// <summary>
    /// Имя персонажа
    /// </summary>
    public string namePlayerString;
    //максимальные значения
    public int hpMax = 23;
    private int manaMax = 50;
    private int maxexp = 100;
    private int GoldMax = 99999;
    //начальные значения
    public int manaCur = 0;
    private int hpCur = 0;
    public int goldCur = 10;
    public int baseIntellect;
    public int baseAtack;
    public int baseStamina;
    public int baseSpeed;
    private ihp _ihp;
    private int curLevel;
    [SerializeField]
    private int lvlPlayer = 1;//уровень героя
    public int exp;//опыт
    //Инвентари
    public Inventory inventory;
    // private Inventory chest; //банк
    public Inventory CharPanel;


    private int intellect;
    private int atack;
    [SerializeField]
    private int stamina;

    public int speed;//

    SpeedHelper sh;
    Inventory tmpInv;
    randomMob rm;
    private damage d;
    private PVP _pvp;
    private damage playerDamage;
    private hp playerHP;

    public GameObject ShowMenuBtn;
    public GameObject ShowInvBtn;
    public GameObject ShowExitBtn;
	public GameObject ShowDostigBtn;
    private SaveHalper _savehalper;

    #endregion
    #region Prop
    /// <summary>
    /// def
    /// </summary>
   
    public int Stamina
    {
        get { return stamina; }
        set { stamina = value; }
    }
    public static PlayerHelper Instance
    {
        get
        {

            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerHelper>();
            }
            return instance;

        }

    }
    /// <summary>
    /// Физ Атака Героя
    /// </summary>
    public int Atack
    {
        get { return atack; }
        set { atack = value; }
    }
    /// <summary>
    /// Накопленный опыт
    /// </summary>
    public int Exp
    {
        get
        {
            return exp;
        }

        set
        {
            exp = value;
        }
    }
    /// <summary>
    /// Свойство золото
    /// </summary>
    public int GoldCur
    {
        get
        {
            return goldCur;
        }

        set
        {
            goldCur = value;
        }
    }
    /// <summary>
    /// Свойство текущее значение маны
    /// </summary>
    public int ManaCur
    {
        get
        {
            return manaCur;
        }

        set
        {
            manaCur = value;
        }
    }
    /// <summary>
    /// Свойство текущий уровень героя
    /// </summary>
    public int LvlPlayer
    {
        get
        {
            return lvlPlayer;
        }

        set
        {
            lvlPlayer = value;
        }
    }
    /// <summary>
    /// Свойство флаг о повышении уровня героя
    /// </summary>
    public bool GetLVL
    {
        get
        {
            return getLVL;
        }

        set
        {
            getLVL = value;
        }
    }
    /// <summary>
    /// Свойство скорость героя IQ
    /// </summary>
    public int Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }
    /// <summary>
    /// Свойство максимальное значение хп
    /// </summary>
    public int HpMax
    {
        get
        {
            return hpMax;
        }

        set
        {
            hpMax = value;
        }
    }
    /// <summary>
    /// Свойство максимальное значение мп
    /// </summary>
    public int ManaMax
    {
        get
        {
            return manaMax;
        }

        set
        {
            manaMax = value;
        }
    }
    public int HpCur
    {
        get
        {
            return _hp.HP;
        }

        set
        {
            _hp.HP = value;
        }
    }
    public int Intelect
    {
        get
        {
            return intellect;
        }

        set
        {
            intellect = value;
        }
    }
    //ссылка на компонент хп
    hp _hp;
    private int _hpCur;

    #endregion
    #region Voids
    void Awake()
    {
       
        _pvp = FindObjectOfType<PVP>();
        _hp = GameObject.FindObjectOfType<hp>();
        rm = FindObjectOfType<randomMob>();
        if (TextPobeg != null)
        {
            _textPobeg = TextPobeg.GetComponent<Text>();
        }
    }
    void Start()
    {
     
        _savehalper = GameObject.Find("SaveManager").GetComponent<SaveHalper>();
        print(_savehalper.Savining);
        if (_savehalper.Savining == 0 && Application.loadedLevel == 1)
        {
            PlayerPrefs.SetInt("BaseSpeed", 5);
            PlayerPrefs.SetInt("BaseAtack", 15);
            PlayerPrefs.SetInt("BaseStamina", 5);
            PlayerPrefs.SetInt("BaseIntellect", 10);
            print("its mr");
        }
        else
        {
            baseSpeed = PlayerPrefs.GetInt("BaseSpeed");
            baseAtack = PlayerPrefs.GetInt("BaseAtack");
            baseStamina = PlayerPrefs.GetInt("BaseStamina");
            baseIntellect = PlayerPrefs.GetInt("BaseIntellect");
            maxexp= PlayerPrefs.GetInt("MaxExp");
            exp = PlayerPrefs.GetInt(gameObject + "Exp");
            print(maxexp);
        }              
        SetStats(0, 0,0,0);
        getLVL = false; 
        //вывод опыта слайдер
        if (expPlayer)
        {
            expPlayer.maxValue = maxexp;
            expPlayer.value = exp;
        }
        if (hPText)
        {
            //вывод кол-ва хп слайдер
            hPText.maxValue = hpMax;
            hPText.value = _hp._hp;
        }

        //вывод кол-ва мп слайдер
        if (manaPlayer)
        {
            manaPlayer.maxValue = manaMax;
            manaPlayer.value = manaCur;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Логика гибели игрока, при гибели отправить в столицу , сохранить прогресс и удалить половину денег
        if (HpCur <= 0)
        {
            if (Application.loadedLevel != 1)
            {
                print(" StartCoroutine(Death)");
                StartCoroutine("Death");
            }
        }	
        if (Application.loadedLevel != 1)
        {
            //Блокирование кнопок инвентаря при движении
            if (!rm.stay)
            {
                ShowMenuBtn.GetComponent<Button>().enabled = false;
                ShowMenuBtn.GetComponent<Image>().color = Color.gray;

                ShowInvBtn.GetComponent<Button>().enabled = false;
                ShowInvBtn.GetComponent<Image>().color = Color.gray;


                ShowExitBtn.GetComponent<Button>().enabled = false;
                ShowExitBtn.GetComponent<Image>().color = Color.gray;

				ShowDostigBtn.GetComponent<Button>().enabled = false;
				ShowDostigBtn.GetComponent<Image>().color = Color.gray;
            }
            else
            {
                ShowMenuBtn.GetComponent<Button>().enabled = true;
                ShowMenuBtn.GetComponent<Image>().color = Color.white;

                ShowInvBtn.GetComponent<Button>().enabled = true;
                ShowInvBtn.GetComponent<Image>().color = Color.white;


                ShowExitBtn.GetComponent<Button>().enabled = true;
                ShowExitBtn.GetComponent<Image>().color = Color.white;

				ShowDostigBtn.GetComponent<Button>().enabled = true;
				ShowDostigBtn.GetComponent<Image>().color = Color.white;
            }
        }
    
        //вывод информации о состоянии героя на UI

        if (GoldText && namePlayer && texlvl && manaPlayer && hPText)
        {
            GoldText.text = GoldCur.ToString();
            namePlayer.text = namePlayerString;
			if (LvlPlayer >= 10) {
				texlvl2.text = "Max Lvl";
				texlvl.text = "Max Lvl";
			}
			else
			{
				texlvl2.text = lvlPlayer.ToString ();
				texlvl.text = lvlPlayer.ToString ();
			}
            HPtext.text = "" + HpCur + "/" + hpMax.ToString();
            HPtextBAR.text = "" + HpCur + "/" + hpMax.ToString();
            MPtextBAR.text = "" + ManaCur + "/" + manaMax.ToString();
            MPtext.text = "" + ManaCur + "/" + manaMax.ToString();
            manaPlayer.value = ManaCur;
            manaPlayer.maxValue = manaMax;
            LVLMenutext.text = "" + Exp + "/" + maxexp.ToString();
            hPText.value = HpCur;
            hPText.maxValue = hpMax;
          
          
            expPlayer.value = Exp;
            expPlayer.maxValue = maxexp;
        }
        else if (GoldText)
        {
            GoldText.text = GoldCur.ToString();
        }
        //накопленый опыт больше максимального
        if (Exp >= maxexp && exp!=0 && Application.loadedLevel!=1)
        {
            print("MaxExp" + maxexp);
			if (lvlPlayer < 10) {
				lvlPlayer += 1;
				getLVL = true;
				Exp = 0;
				PlayerPrefs.SetInt (gameObject.name + "CurExpt", Exp);
			} else 
			{
				return;
			}
        }
        //в зависимости от уровня героя изменить максимальные  значения хп и мп
        switch (lvlPlayer)
        {
            case 1: maxexp = 12; manaMax = 0; hpMax = 26; PlayerPrefs.SetInt("MaxExpt", maxexp); break;
            case 2: maxexp = 34; manaMax = 34; hpMax = 34; PlayerPrefs.SetInt("MaxExpt", maxexp); break;
            case 3: maxexp = 100; manaMax = 42; hpMax = 42; PlayerPrefs.SetInt("MaxExpt", maxexp); break;
            case 4: maxexp = 200; manaMax = 54; hpMax = 54; PlayerPrefs.SetInt("MaxExpt", maxexp); break;
            case 5: maxexp = 400; manaMax = 68; hpMax = 68; PlayerPrefs.SetInt("MaxExpt", maxexp); break;
            case 6: maxexp =650; manaMax = 80; hpMax = 80; PlayerPrefs.SetInt("MaxExpt", maxexp); break;
            case 7: maxexp = 800; manaMax = 94; hpMax = 94; PlayerPrefs.SetInt("MaxExpt", maxexp); break;
            case 8: maxexp = 1000; manaMax = 110; hpMax = 110; PlayerPrefs.SetInt("MaxExpt", maxexp); break;
            case 9: maxexp = 1200; manaMax = 122; hpMax = 122; PlayerPrefs.SetInt("MaxExpt", maxexp); break;
            case 10: maxexp =2000; manaMax = 150; hpMax = 150; PlayerPrefs.SetInt("MaxExpt", maxexp); break;
            default: break;
        }

        if (TextPobeg && TextPobeg.activeSelf)//Прячем текст о попытки побега через 2 секунды
        {
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                TextPobeg.SetActive(false);
                timer = 0;
            }
        }
    }
    ///
    private IEnumerator Death()
    {
        ScreenFader.Fader(3, Color.red);
        yield return new WaitForSeconds(1.5f);
        GoldCur = GoldCur / 2;
        HpCur = HpMax / 2;
        SaveHalper.Instance.Save();
        LoadManager.levelName = "GameMenu";
        SceneManager.LoadScene("LoadScene");
    }
    #region Для выкидывания объекта
    /// <summary>
    /// Пересечение с колайдером
    /// </summary>
    /// <param name="collision"></param>
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag=="Item")
    //    {
    //if (inventory.AddItem(collision.gameObject.GetComponent<Item>());)
    //{
    //       Destroy(collision.gameObject);
    //}


    //    }
    //}
    #endregion
    //Открыть окно персонажа
    public void OpenCharPanel()
    {   
       CharPanel.GetComponent<Inventory>().ShowInventory();          
    }
    /// <summary>
    /// Вывод окно персонажа
    /// </summary>
    public void OpenInvPanel()
    {
      
            inventory.GetComponent<Inventory>().ShowInventory();
            sfx2.GetComponent<AudioSource>().PlayOneShot(buttu1);//звук кнопки открытие инвентаря
                                                
       
    }
    /// <summary>
    /// Вывод информации об герое в окно персонажа
    /// </summary>
    /// <param name="agility"></param>
    /// <param name="speed"></param>
    /// <param name="stamina"></param>
    /// <param name="intellect"></param>
    public void SetStats(int atack, int speed, int stamina, int intellect)
    {
        if (Application.loadedLevel != 1)
        {
            this.Atack = atack + baseAtack;
            this.Speed = speed +baseSpeed;
            this.Stamina = stamina + baseStamina;
            this.intellect = intellect + baseIntellect;
            if (statsText)
            {

                statsText.text = string.Format("Защ: {0}\nСкор: {1}\nИнт: {2}\nУрон: {3}", this.Stamina, this.Speed, this.intellect, this.Atack);

            }
        }
    }
    /// <summary>
    /// Сохранение
    /// </summary>
    public void SavePlayer()
    {
        string content = string.Empty;

        PlayerPrefs.SetInt(gameObject.name + "CurLVL", lvlPlayer);
        PlayerPrefs.SetInt(gameObject.name + "CurHP", HpCur);
        PlayerPrefs.SetInt(gameObject.name + "CurMP", manaCur);
        PlayerPrefs.SetInt(gameObject.name + "CurGold", goldCur);
        PlayerPrefs.SetInt(gameObject.name + "LVLNAME", curLevel);
        PlayerPrefs.SetString(gameObject.name + "PlayerName", namePlayerString);
        if (PlayerPrefs.GetInt("BaseSpeedt")!=0)
        {
             baseSpeed = PlayerPrefs.GetInt("BaseSpeedt");
             baseAtack = PlayerPrefs.GetInt("BaseAtackt");
             baseStamina = PlayerPrefs.GetInt("BaseStaminat");
             baseIntellect = PlayerPrefs.GetInt("BaseIntellectt");

            PlayerPrefs.SetInt("BaseSpeed", baseSpeed);
            PlayerPrefs.SetInt("BaseAtack", baseAtack);
            PlayerPrefs.SetInt("BaseStamina", baseStamina);
            PlayerPrefs.SetInt("BaseIntellect", baseIntellect);
        }
     //  if (PlayerPrefs.GetInt(gameObject.name + "CurExpt") != 0)
      //  {
            exp = PlayerPrefs.GetInt(gameObject.name + "CurExpt");          
            PlayerPrefs.SetInt(gameObject.name + "CurExp",exp);          
      //  }
       if (PlayerPrefs.GetInt("MaxExpt")> 0)
        {
            maxexp = PlayerPrefs.GetInt("MaxExpt");
            PlayerPrefs.SetInt("MaxExp", maxexp);
            print(maxexp);
        }


    }
    /// <summary>
    /// Продолжение сохраненной игры
    /// </summary>
    public void LoadPlayer()
    {
        curLevel = PlayerPrefs.GetInt(gameObject.name + "LVLNAME");
        lvlPlayer = PlayerPrefs.GetInt(gameObject.name + "CurLVL");
        _hp.HP = PlayerPrefs.GetInt(gameObject.name + "CurHP");
        ManaCur = PlayerPrefs.GetInt(gameObject.name + "CurMP");
        GoldCur = PlayerPrefs.GetInt(gameObject.name + "CurGold");
        Exp = PlayerPrefs.GetInt(gameObject.name + "CurExp");
        maxexp= PlayerPrefs.GetInt("MaxExp");
        namePlayerString = PlayerPrefs.GetString(gameObject.name + "PlayerName");

        baseSpeed = PlayerPrefs.GetInt("BaseSpeed");
        baseAtack = PlayerPrefs.GetInt("BaseAtack");
        baseStamina = PlayerPrefs.GetInt("BaseStamina");
        baseIntellect = PlayerPrefs.GetInt("BaseIntellect");

    }
    void OnTriggerEnter(Collider other)
    {
        if (other == GameObject.FindGameObjectWithTag("dore").GetComponent<Collider>())
        {
            SaveHalper.Instance.Save();
            LoadManager.levelName = "GameMenu";
            SceneManager.LoadScene("LoadScene");
        }

    }
    //Привязать к кнопке побега из битвы
    public void RunOutBattle()
    {
        sh = GetComponent<SpeedHelper>();
        d = GetComponent<damage>();
        
        GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
        int _cntMobs = mobs.Length;
        print(_cntMobs);

        int shansPobega = speed + Random.Range(0, 3 * speed);//шанс побега зависит от скорости игрока и количества мобов на сцене
        print("шанс:" + shansPobega + "Нужно" + speed * _cntMobs);
        if (shansPobega > speed * _cntMobs && !rm.boosIsView)//если удачно то улетаем в стольну и сохраняемся
        {
            AchievmentManager.Instance.Pobeg = true;
          //  SaveHalper.Instance.Save();
          //  Application.LoadLevel(1);
          rm.transBackFromBatleField();
            GameObject[] mobsonScene = GameObject.FindGameObjectsWithTag("Mob");
            for (int i = 0; i < mobsonScene.Length; i++)
            {
                Destroy(mobsonScene[i]);
            }
            rm.EnbBattle = true;
            _pvp.selectTypeAtack = 0;
        }
        else//иначе пропускаем ход
        {
            rm.hideMenuBattle();
          
            playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<damage>();
        
        
         
      
            TextPobeg.SetActive(true);
            _textPobeg.text = "не повезло!";
            playerDamage.IsGo = true;
            _pvp.selectPlayer = 0;
            _pvp.selectTypeAtack = 0;
            _pvp.SelectTypeAttack();
            d.typeAttack = 3;
            sh.playerStep = false;
            sh.endRound = true;

            sh.WhoIsDamag();
        }

    }
    #endregion
}
