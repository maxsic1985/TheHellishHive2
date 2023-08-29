using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.EventSystems;
using System.Xml.Serialization;
using System.IO;

public class InventoryManager : MonoBehaviour
{
    #region Reference
    /// <summary>
    /// ссылка на слот, для конструктора
    /// </summary>
    public GameObject slotPrefab;
    /// <summary>
    /// ссылка на префаб иконки
    /// </summary>
    public GameObject iconPrefab;

    /// <summary>
    /// ширина и высота инвентаря, для конструктора
    /// </summary>
    private float invWidth, invHight;

    #region Для выбрасывания объектов
    /// <summary>
    /// объект для выкидывания из инвентаря
    /// </summary>
    //public GameObject dropItem;
    //private static GameObject playerRef;
    #endregion

    /// <summary>
    /// 
    /// </summary>
    public GameObject itemObject;
    /// <summary>
    /// переменные для вывода окна описания итема Tooltip
    /// </summary>
    public GameObject toolObject;
    public Text sizeTextObject;//видимый текст
    public Text visualTextObject;//видимый текст
    /// <summary>
    /// ссылка на канвас
    /// </summary>
    public Canvas canvas;
    /// <summary>
    /// слот из и слот куда , при перетаскивании 
    /// </summary>
    private Slot from, to;
    /// <summary>
    /// выбранный слот
    /// </summary>
    private GameObject clicked;
    /// <summary>
    ///количество итемов в чейке текст  
    /// </summary>
    public Text stackTxt;
    /// <summary>
    /// для меню при разделении итемов в иконке
    /// </summary>
    public GameObject selectStackSize;
    /// <summary>
    /// количество итемов в руке
    /// </summary>
    private int splitAmount;
    /// <summary>
    /// максимальное кол-во итемов  которые можно удалить из стека
    /// </summary>
    private int maxStackCount;


    /// <summary>
    ///  перемещаемый слот
    /// </summary>
    private Slot movingSlot;
    /// <summary>
    /// ссылка на eventSystem
    /// </summary>
    public EventSystem eventsystem;
    /// <summary>
    /// ссылка на статический объект иконки итема которая создается при перетаскивании объекта
    /// </summary>
    private GameObject hoverObject;

  


    #endregion
    private static InventoryManager instance;
    #region Properties
    public Slot To
    {
        get { return to; }
        set { to = value; }
    }

    public Slot From
    {
        get { return from; }
        set { from = value; }
    }

    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryManager>();
            }
            return instance;
        }
    }

    public GameObject Clicked
    {
        get { return clicked; }
        set { clicked = value; }
    }

    public int SplitAmount
    {
        get { return splitAmount; }
        set { splitAmount = value; }
    }


    public Slot MovingSlot
    {
        get { return movingSlot; }
        set { movingSlot = value; }
    }

    public GameObject HoverObject
    {
        get { return hoverObject; }
        set { hoverObject = value; }
    }
    public int MaxStackCount
    {
        get { return maxStackCount; }
        set { maxStackCount = value; }
    }


    public float InvHight
    {
        get { return invHight; }
        set { invHight = value; }
    }

    public float InvWidth
    {
        get { return invWidth; }
        set { invWidth = value; }
    }

    public ItemContainer ItemCont
    {
        get
        {
            return itemCont;
        }

        set
        {
            itemCont = value;
        }
    }
    #endregion

    private ItemContainer itemCont = new ItemContainer();
    public void Start()
    {
        Type[] itemTypes = { typeof(Equipment), typeof(Weapon), typeof(Consumeable), typeof(Magic) };

        XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer), itemTypes);
        TextAsset levelXML = (TextAsset)Resources.Load("Items", typeof(TextAsset));
        var stream = new MemoryStream(levelXML.bytes);
        StreamReader textReader = new StreamReader(stream);
        //  TextReader textreader = new StreamReader("jar:file://" + Application.dataPath + "!/assets/Items");

        itemCont = (ItemContainer)serializer.Deserialize(textReader);

      //  textreader.Close();
    }
    /// <summary>
    /// Для разделения отднотипных итемов по разным слотам. 
    /// Установить количество итемов которые можно удалить
    /// </summary>
    /// <param name="maxStackCount"></param>
    public void SetStackInfo(int maxStackCount)
    {
      //  selectStackSize.SetActive(true);//показать UI разделения итемов
      //  toolObject.SetActive(false);
        splitAmount = 0;
        this.maxStackCount = maxStackCount;//запомнить максимальное ко-во
        stackTxt.text = splitAmount.ToString();
    }

}
