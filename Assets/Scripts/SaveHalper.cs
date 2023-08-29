using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
/// <summary>
/// Мэнеджер для управления загрузкой/сохранением
/// </summary>
public class SaveHalper : MonoBehaviour
{
	public GameObject PanelExit2;
	public AudioClip Exitmenu3;//звук открытия
	public AudioSource ExitSource3;
    #region Var
    public AudioClip SFX;//звук кнопки нажатия
    public AudioSource soundSFX;
    public AudioClip SFX2;//звук кнопки нажатия
    public AudioSource soundSFX2;
    private static SaveHalper instance;
    /// <summary>
    /// Кнопка продолжить добавлена бля изменения цвета в том случае если сохранений не обнаружено
    /// </summary>
    public Button buttonContinius;
    /// <summary>
    /// Флаг имеется ли сохранение, 0-нет сохранения, 1- есть сохраненная игра
    /// </summary>
    [SerializeField]
    public int Savining = 0;
    public AnimationDialog savequest;
	public LV1Dialog savequest2;
    /// <summary>
    /// флаг новая игра, устанавливаетя при нажатии на кнопку новая игра, сбрасывает значение Savining в 0
    /// </summary>
    private bool newGame;
    #endregion

    public static SaveHalper Instance
    {
        get
        {

            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<SaveHalper>();
            }
            return instance;

        }

    }

    public bool ISaveIt { get; private set; }
    #region Voids
    void Awake()
    {
        Savining = PlayerPrefs.GetInt(gameObject.name + "Savining");
    }
    void Start()
    {

        //загрузить значение Savining из префаба
      
        //В ГЛАВНОМ МЕНЮ БЛОКИРОВКА КНОПКИ ПРОДОЛЖИТЬ
        if (Application.loadedLevel == 0)
        {
            if (Savining == 0)
            {


                buttonContinius.enabled = false;
                var button = buttonContinius;

            }
        }
        //если имеется сохранение и загружаемая сцена не меню и не рынок то загрузить основную сцену с загрузкой инвентаря и прочим
        else if (Savining == 1 && Application.loadedLevel == 2)
        {
            Load();
        }
        //ЗАГРУЖАЕМАЯ СЦЕНА РЫНОК И ИМЕЕТСЯ СОХРАНЕНИЕ ТО ЗАГРУЗИТЬ ИХ
        else if (Application.loadedLevel == 1 && Savining == 1)
        {
            Load();
        }
    }
    /// <summary>
    /// Сохранить
    /// Сохраняет флаг Savining в 1 
    /// Сохраняет инвентари
    /// Сохраняет параметры игрока
    /// </summary>
    public void Save()
    {
        SaveBag();
        PlayerPrefs.SetInt( "quest1", savequest.quest1);
		PlayerPrefs.SetInt( "triggSave", savequest2.triggSave);
       
        PlayerPrefs.SetInt(gameObject.name + "Savining", Savining = 1);
        GameObject[] inventories = GameObject.FindGameObjectsWithTag("Inventory");
        foreach (GameObject inventory in inventories)
        {
            inventory.GetComponent<Inventory>().SaveInventory();
        }
        PlayerHelper.Instance.SavePlayer();
      
        print("Игра Сохранена");
        ISaveIt = true;
    }
	public void Quit11()
	{
		LoadManager.levelName = "MainMenu";
		SceneManager.LoadScene ("LoadScene");
	}
	public void ExitNo2()
	{
		ExitSource3.GetComponent<AudioSource>().PlayOneShot(Exitmenu3);
		PanelExit2.SetActive(false);

	}
	public void ExitLVL0()
	{
		ExitSource3.GetComponent<AudioSource>().PlayOneShot(Exitmenu3);
		PanelExit2.SetActive(! PanelExit2.activeSelf);

	}
    public void SaveInventory()
    {
        PlayerPrefs.SetInt(gameObject.name + "Savining", Savining = 1);
        GameObject[] inventories = GameObject.FindGameObjectsWithTag("Inventory");
        foreach (GameObject inventory in inventories)
        {
            inventory.GetComponent<Inventory>().SaveInventory();
        }
    }
    public void SaveBag()
    {
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("ni4ka");
       foreach(GameObject _tmp in tmp)
        {
            print(_tmp);
            if (PlayerPrefs.GetInt(_tmp.name + "tmp") == 1)
            {
                PlayerPrefs.SetInt(_tmp.name, 1);
              //  PlayerPrefs.SetInt(_tmp.name + "tmp", 0);
            }
        }
    }
    /// <summary>
    /// Загрузить 
    /// </summary>
    public void Load()
    {
		savequest2.triggSave =PlayerPrefs.GetInt("triggSave");
        savequest.quest1 = PlayerPrefs.GetInt("quest1");
        GameObject[] inventories = GameObject.FindGameObjectsWithTag("Inventory");
        foreach (GameObject inventory in inventories)
        {
           
            inventory.GetComponent<Inventory>().LoadInventory();
          
        }
        GameObject.FindObjectOfType<PlayerHelper>().LoadPlayer();
    }
    /// <summary>
    /// Продолжить игру 
    /// вешается на кнопки в меню "новая игра" и "продолжить игру"
    /// </summary>
    /// <param name="newgame">в инспекторе выставляется флаг новая игра </param>
    public void ForButtonsContOrNewGame(bool newgame)
    {
        soundSFX2.PlayOneShot(SFX2);

        newGame = newgame;
        if (!newGame)//ПРОДОЛЖИТЬ
        {
            PlayerPrefs.SetInt(gameObject.name + "Savining", Savining = 1);
            LoadManager.levelName = "GameMenu";
            SceneManager.LoadScene("LoadScene");
            PlayerPrefs.DeleteKey("BaseSpeedt");
            PlayerPrefs.DeleteKey("BaseAtackt");
            PlayerPrefs.DeleteKey("BaseStaminat");
            PlayerPrefs.DeleteKey("BaseIntellectt");
            PlayerPrefs.DeleteKey(gameObject.name + "CurExpt");
            PlayerPrefs.DeleteKey("MaxExpt");
            GameObject[] tmp = GameObject.FindGameObjectsWithTag("ni4ka");
            foreach (GameObject _tmp in tmp)
            {
                PlayerPrefs.DeleteKey(_tmp.name + "tmp");         
            }

        }
        else//НОВАЯ ИГРА
		{   
			soundSFX2.PlayOneShot(SFX2);
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt(gameObject.name + "Savining", Savining = 0);
            LoadManager.levelName = "GameMenu";
            SceneManager.LoadScene("LoadScene");
        }


    }
    /// <summary>
    /// Приаязать к кнопке для входа в лабиринт
    /// </summary>
    public void LoadFirstLvl()
    {
        //СОХРАНИТЬ ПОКУПКИ И ПРОДАЖИ
        Save();
        soundSFX.PlayOneShot(SFX);//для звука кнопки.
        LoadManager.levelName = "Lvl1";
        SceneManager.LoadScene("LoadScene");
    }
    #endregion
}