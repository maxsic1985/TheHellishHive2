using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;


public class MenuManager : MonoBehaviour
{
    public Menu currentMenu;
    public AudioClip buttu;
    public AudioSource sound;
    private GameObject Jo;//джостик
    private FirstPersonController RB;//физика


    public CanvasGroup[] panels;
    public GameObject MenuPanel;
    private bool stoping=false;
    private randomMob RM;
    private static MenuManager instance;
    public static MenuManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<MenuManager>();
            }
            return instance;
        }
    }
    private void Start()
    {
        currentMenu = null;
        Jo = GameObject.Find("MobileControl");
        RB = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        RM = GameObject.FindGameObjectWithTag("Player").GetComponent<randomMob>();
 
       
    }
    private void Update()
    {
        //отключение управления при открытии инвентаря или панели игрока
        if (panels!=null)
        {

            for (int i = 0; i < panels.Length; i++)
            {
                if (panels[0].alpha > 0 || panels[1].alpha > 0 || MenuPanel.activeSelf)//открыт инвентарь или панель игрока
                {
                    StopControl();
                }
                else
                {
                    StartControl();
                }
            }
        }
        if (RM!=null && RM.triggerBossBool&& RM.Boss != null)//Если вошли в триггер появления босса то откл управление
        {
            StopControl();
        }
    }
    /// <summary>
    /// Показать меню
    /// </summary>
    /// <param name="menu"></param>
    public void ShowMenu(Menu menu)
    {
        if (currentMenu != menu && !menu.IsOpen)
        {
           
            sound.GetComponent<AudioSource>().PlayOneShot(buttu);
            currentMenu = menu;
            currentMenu.IsOpen = true;
            menu.transform.SetAsLastSibling();
        }
        else if (currentMenu != menu)
        {
            menu.IsOpen = false;
        }
        else if (currentMenu == menu)
        {


            if (menu.GetComponentInChildren<Inventory>())
            {
               // Inventory tmp;
               // tmp = GetComponent<Inventory>();
                Destroy(GameObject.Find("Hover"));
            }
            InventoryManager.Instance.To = null;
            InventoryManager.Instance.From = null;
            currentMenu.IsOpen = false;
            currentMenu = null;
            sound.GetComponent<AudioSource>().PlayOneShot(buttu);
            StartControl();
        }

    }
    /// <summary>
    /// Включить управление
    /// </summary>
    public void StartControl()
    {
        if (Jo!=null&&stoping && !GameObject.Find("DropItem(Clone)"))
        {
            
            Jo.SetActive(true);
            RB.enabled = true;//продолжить перемещение
            Jo.GetComponentInParent<Canvas>().enabled = true;//показ джостик
            stoping = false;
          
        }
    }
    /// <summary>
    /// Отключить управление
    /// </summary>
    public void StopControl()
    {
        if ( Jo!=null)
        {
            stoping = true;
            if (RB.enabled==true)
            {
                              RB.enabled = false;//остановить перемещение
                Jo.GetComponentInParent<Canvas>().enabled = false;
                Jo.SetActive(false);

            }
        }
    }
}
