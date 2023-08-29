using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class Inventory : MonoBehaviour
{
    #region Reference
    public AudioSource sound2;
    public AudioClip SFX;


    private RectTransform inventoryRect;

    /// <summary>
    /// отсутпы, слева и сверху,для конструктора
    /// </summary>
    public float slotPaddingLeft, slotPaddingTop;
    /// <summary>
    /// размер слота в инвентаре, для конструктора
    /// </summary>
    public float slotSize;
    /// <summary>
    /// количество слотов
    /// </summary>
    public int slots;
    /// <summary>
    /// число строк
    /// </summary>
    public int rows;
    /// <summary>
    /// Ширина панели  игрока для переноса объектов на игрока
    /// </summary>
    public float panelWidht;
    /// <summary>
    /// ссылка на панель игрока, для конструктора
    /// </summary>
    public GameObject panelPref;
    /// <summary>
    /// список всех слотов 
    /// </summary>
    private List<GameObject> allSlots;
    /// <summary>
    /// число пустых слотов
    /// </summary>
    private int emptySlots;
    /// <summary>
    /// ссылка на cancasGroup
    /// </summary>
    public CanvasGroup canvasGroup;
    /// <summary>
    /// скрыть/показать инвентарь
    /// </summary>
    private bool fadingIn;
    private bool fadingOut;
    /// <summary>
    /// время открытия/закрытия инвентаря
    /// </summary>
    public float fadeTime;
    /// <summary>
    /// смещение спарйта hoverObject
    /// </summary>
    private float hoverYOffSet;
    /// <summary>
    /// открыт chest или inventory
    /// </summary>
    private bool isOpen;
    public static bool mouseInside;
    private MenuManager MM;
    // private static GameObject toolTip;
    //  private static Text sizeText;//текст спрятанный за фоном

    //  private static Text visualText;//текст спрятанный за фоном
    CanvasGroup cgch;

    public GameObject DropItemPanel;
    #region Для выбрасывания объектов
    /// <summary>
    /// объект для выкидывания из инвентаря
    /// </summary>
    //public GameObject dropItem;
    //private static GameObject playerRef;
    #endregion
    #endregion
    #region Properties
    public bool IsOpen
    {
        get { return isOpen; }
        set { isOpen = value; }
    }
    public int EmptySlots
    {
        get
        {
            return emptySlots;
        }

        set
        {
            emptySlots = value;
        }
    }
    public bool _dropItemYes { get; private set; }
    #endregion
    #region Voids
    void Start()
    {
        MM = FindObjectOfType<MenuManager>();
        _dropItemYes = false;
        isOpen = false;
        CreateLayout();  //Создать инвентарь

        InventoryManager.Instance.MovingSlot = GameObject.Find("MovingSlot").GetComponent<Slot>();
    }
    void Update()
    {

        ///удаление итема если переместить слот за пределы инвентаря
        if (Input.GetMouseButtonUp(0))
        {
            //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && InventoryManager.Instance.From != null)
            //{
            //    if (!eventsystem.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            //    {
            //        InventoryManager.Instance.From.GetComponent<Image>().color = Color.gray;
            //        InventoryManager.Instance.From.ClearSlot();
            //        Destroy(GameObject.Find("Hover"));
            //        InventoryManager.Instance.To = null;
            //        InventoryManager.Instance.From = null;
            //        hoverObject = null;
            //    }
            //}
            if (!mouseInside && InventoryManager.Instance.From != null && !InventoryManager.Instance.eventsystem.IsPointerOverGameObject(-1))//клик за пределами инвентаря и объект выран
            {
                #region Для выкидывания объектов
                ////   Для выбрасывания объекта на сцену раскомментировать
                //   foreach (ItemScript item in InventoryManager.Instance.From.Items)
                //   {
                //       float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);

                //       Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                //       v *= 30;

                //       GameObject dropTmp = (GameObject)GameObject.Instantiate(InventoryManager.Instance.dropItem, playerRef.transform.position - v, Quaternion.identity);
                //       dropTmp.AddComponent<ItemScript>();
                //       dropTmp.GetComponent<ItemScript>().Item=item.Item;

                //   }
                #endregion
                GameObject tmpPanel;
                if (!GameObject.Find("DropItem(Clone)"))
                {
                    tmpPanel = Instantiate(DropItemPanel);
                    tmpPanel.transform.position = new Vector3(0, 10, 0);
                    tmpPanel.transform.SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>().transform, false);
                    Destroy(GameObject.Find("Hover"));
                    if (GameObject.Find("MobileControl"))
                    {
                        GameObject.Find("MobileControl").GetComponent<Canvas>().enabled = false;
                    }

                }



                //   GameObject tmpPanel;
                //  tmpPanel = GameObject.Instantiate(DropItemPanel,transform.position,transform.rotation)as GameObject;
                //  DropItemPanel.transform.SetParent(GameObject.Find("Canvas").transform, true);



            }
            else if (!InventoryManager.Instance.eventsystem.IsPointerOverGameObject(-1) && !InventoryManager.Instance.MovingSlot.isEmpty)
            {
                #region Для выкидывания объектов
                ////  Для выбрасывания объекта на сцену раскомментировать
                //  foreach (ItemScript item in InventoryManager.Instance.From.Items)

                //  {
                //      float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);

                //      Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                //      v *= 30;

                //      GameObject dropTmp = (GameObject)GameObject.Instantiate(InventoryManager.Instance.dropItem, playerRef.transform.position - v, Quaternion.identity);

                //  dropTmp.AddComponent<ItemScript>();
                //  dropTmp.GetComponent<ItemScript>().Item = item.Item;

                //  }
                #endregion
                print("gfgdfg");
                InventoryManager.Instance.MovingSlot.ClearSlot();
                Destroy(GameObject.Find("Hover"));
            }

        }

        ///создание иконки при перемещении слота
        if (InventoryManager.Instance.HoverObject != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryManager.Instance.canvas.transform as RectTransform, Input.mousePosition, InventoryManager.Instance.canvas.worldCamera, out position);
            position.Set(position.x, position.y - hoverYOffSet);
            InventoryManager.Instance.HoverObject.transform.position = InventoryManager.Instance.canvas.transform.TransformPoint(position);
        }

    }
    public void SetBoolForDrop()
    {
        _dropItemYes = true;
        DropItemFromInv();
        if (GameObject.Find("MobileControl"))
        {
            GameObject.Find("MobileControl").GetComponent<Canvas>().enabled = true;
        }

    }
    public void ReSetBoolForDrop()
    {
        _dropItemYes = false;
        DropItemFromInv();
        if (GameObject.Find("MobileControl"))
        {
            GameObject.Find("MobileControl").GetComponent<Canvas>().enabled = true;
        }


    }

    public void DropItemFromInv()
    {
        if (_dropItemYes && InventoryManager.Instance.From != null)
        {
            InventoryManager.Instance.From.GetComponent<Image>().color = Color.gray;
            InventoryManager.Instance.From.ClearSlot();//очистить слот от итемов
            print("just delete");
            if (InventoryManager.Instance.From.transform.parent == CharactersPanel.Instance.transform)
            {
                CharactersPanel.Instance.CalcStats();
            }

            Destroy(GameObject.Find("Hover"));//удалить времееный объект 
                                              //сбросить объекты
            InventoryManager.Instance.To = null;
            InventoryManager.Instance.From = null;
            Destroy(GameObject.Find("DropItem(Clone)"));//удалить времееный объект 
        }
        else
        {
            Destroy(GameObject.Find("DropItem(Clone)"));//удалить времееный объект 

            InventoryManager.Instance.To = null;
            InventoryManager.Instance.From = null;
        }


    }

    /// <summary>
    /// Перемещение инвентаря
    /// </summary>
    public void OnDrag()
    {
        if (isOpen)
        {
            //    MoveInventory(); // перемещение инвентаря 
        }

    }
    public void PointerExit()
    {
        //если открыто окно персонажа то ни чего нельзя удалить
        //if (cgch.alpha > 0)
        //{

        //}
        //else
        //{
        mouseInside = false;
        //}

    }
    public void PointerEnter()
    {
        if (canvasGroup.alpha > 0)
        {
            mouseInside = true;
        }
    }
    /// <summary>
    /// показать описание итема
    /// </summary>
    /// <param name="slot"></param>
    public virtual void ShowToolTlp(GameObject slot)
    {
        InventoryManager.Instance.toolObject.GetComponent<RectTransform>().transform.SetAsLastSibling();//вывести на передний фон
        //логика, если выьранный слот не пустой то показать окно ToolTip
        Slot tmpslot = slot.GetComponent<Slot>();//временная переменая слот
        if (slot.GetComponentInParent<Inventory>().isOpen && !tmpslot.isEmpty && InventoryManager.Instance.HoverObject == null && !InventoryManager.Instance.selectStackSize.activeSelf)
        {
            InventoryManager.Instance.visualTextObject.text = tmpslot.CurrentItem.GetToolTip();
            InventoryManager.Instance.sizeTextObject.text = InventoryManager.Instance.visualTextObject.text;
            InventoryManager.Instance.toolObject.SetActive(true);
            //переместить окно чуть левее и ниже выбранного слота
            float xPos = slot.transform.position.x + slotPaddingLeft;
            float yPos = slot.transform.position.y - slot.GetComponent<RectTransform>().sizeDelta.y - slotPaddingTop;
            InventoryManager.Instance.toolObject.transform.position = new Vector2(xPos, yPos);
        }
    }
    /// <summary>
    /// Скрыть окно описания итема
    /// </summary>
    public void HideToolTlp()
    {
        InventoryManager.Instance.toolObject.SetActive(false);
    }
    /// <summary>
    /// Сохранение инвентаря
    /// </summary>
    public virtual void SaveInventory()
    {
        string content = string.Empty;
        for (int i = 0; i < allSlots.Count; i++)
        {
            Slot tmp = allSlots[i].GetComponent<Slot>();

            if (!tmp.isEmpty)
            {
                content += i + "-" + tmp.CurrentItem.Item.ItemName.ToString() + "-" + tmp.Items.Count.ToString() + ";";

            }
        }
        PlayerPrefs.SetString(gameObject.name + "content", content);
        PlayerPrefs.SetInt(gameObject.name + "slots", slots);
        PlayerPrefs.SetInt(gameObject.name + "rows", rows);
        PlayerPrefs.SetFloat(gameObject.name + "slotPaddingLeft", slotPaddingLeft);
        PlayerPrefs.SetFloat(gameObject.name + "slotPaddingTop", slotPaddingTop);
        PlayerPrefs.SetFloat(gameObject.name + "slotSize", slotSize);
        PlayerPrefs.SetFloat(gameObject.name + "xPos", inventoryRect.position.x);
        PlayerPrefs.SetFloat(gameObject.name + "yPos", inventoryRect.position.y);
        PlayerPrefs.Save();

    }
    /// <summary>
    /// Загрузка инвентаря
    /// </summary>
    public virtual void LoadInventory()
    {

        string content = PlayerPrefs.GetString(gameObject.name + "content");
        slots = PlayerPrefs.GetInt(gameObject.name + "slots");
        rows = PlayerPrefs.GetInt(gameObject.name + "rows");
        slotPaddingLeft = PlayerPrefs.GetFloat(gameObject.name + "slotPaddingLeft");
        slotPaddingTop = PlayerPrefs.GetFloat(gameObject.name + "slotPaddingTop");
        slotSize = PlayerPrefs.GetFloat(gameObject.name + "slotSize");
        //Установить позицию инвентаря
        inventoryRect.position = new Vector3(PlayerPrefs.GetFloat(gameObject.name + "xPos"), PlayerPrefs.GetFloat(gameObject.name + "yPos"), inventoryRect.position.z);
        //Пересоздать инвентарь
        CreateLayout();
        //объедянить строки
        string[] splitContent = content.Split(';');//0-MANA-3
        //Парсинг строки
        for (int x = 0; x < splitContent.Length - 1; x++)
        {

            string[] splitValues = splitContent[x].Split('-');

            int index = int.Parse(splitValues[0]);// 0 Индекс

            string itemName = splitValues[1];//Имя итема

            int amount = int.Parse(splitValues[2]);//3 Количество

            Item tmp = null;

            for (int i = 0; i < amount; i++)
            {

                GameObject loadedItem = Instantiate(InventoryManager.Instance.itemObject);

                if (tmp == null)
                {
                    tmp = InventoryManager.Instance.ItemCont.Consumeables.Find(item => item.ItemName == itemName);
                }

                if (tmp == null)
                {
                    tmp = InventoryManager.Instance.ItemCont.Equipment.Find(item => item.ItemName == itemName);
                }
                if (tmp == null)
                {
                    tmp = InventoryManager.Instance.ItemCont.Weapons.Find(item => item.ItemName == itemName);
                }
                if (tmp == null)
                {
                    tmp = InventoryManager.Instance.ItemCont.Magics.Find(item => item.ItemName == itemName);
                }


                loadedItem.AddComponent<ItemScript>();
                loadedItem.GetComponent<ItemScript>().Item = tmp;
                allSlots[index].GetComponent<Slot>().AddItem(loadedItem.GetComponent<ItemScript>());

                Destroy(loadedItem);
            }
        }
    }
    /// <summary>
    /// Создание каркаса инвентаря
    /// </summary>
    public virtual void CreateLayout()
    {
        if (allSlots != null)
        {
            foreach (GameObject go in allSlots)
            {
                Destroy(go);
            }
        }

        allSlots = new List<GameObject>();//добавить все слоты в лист
        hoverYOffSet = slotSize * 0.01f;
        emptySlots = slots;//запомнить число пустых слотов
        InventoryManager.Instance.InvWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft + 20;//ширина
        InventoryManager.Instance.InvHight = rows * (slotSize + slotPaddingTop) + slotPaddingTop + 50;//высота
        inventoryRect = GetComponent<RectTransform>();//ссылка на RectTransform
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, InventoryManager.Instance.InvWidth);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, InventoryManager.Instance.InvHight);
        //   float ypos = -slotPaddingTop * 30;
        int columns = slots / rows;
        if (this.name != "ChestInventory")
        {
            //GameObject newPanelPref = (GameObject)Instantiate(panelPref);
            //RectTransform PanelRect = newPanelPref.GetComponent<RectTransform>();
            //PanelRect.transform.SetParent(this.transform.parent);
            //PanelRect.localPosition = inventoryRect.localPosition - new Vector3(panelWidht, 0, 0);
            //PanelRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, panelWidht);//
            //PanelRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, InventoryManager.Instance.InvHight);//
            //PanelRect.localScale = Vector3.one;
            //PanelRect.transform.SetParent(this.transform);
        }
        for (int y = 0; y < rows; y++)
        {
            //создание и расположение слотов 
            for (int x = 0; x < columns; x++)
            {
                GameObject newSlot = (GameObject)Instantiate(InventoryManager.Instance.slotPrefab);
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();
                newSlot.name = "Slot";
                newSlot.transform.SetParent(this.transform.parent);
                slotRect.localPosition = inventoryRect.localPosition + new Vector3(10 + slotPaddingLeft * (x + 1) + (slotSize * x), -25 - slotPaddingTop * (y + 1) - (slotSize * y), 0);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * InventoryManager.Instance.canvas.scaleFactor);//установка ширины слота 
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * InventoryManager.Instance.canvas.scaleFactor);//установка высоты слота  
                newSlot.transform.SetParent(this.transform);
                allSlots.Add(newSlot);


            }
        }

    }
    ///добавить в слот идентичный итем
    public bool AddItem(ItemScript item)
    {
        if (item.Item.MaxSize == 1)//если итем не в стаке
        {

            return PlaceEmpty(item);//положить итем в пустой слот ;
        }
        else//если итем в стеке
        {
            foreach (GameObject slot in allSlots)//переберем все слоты
            {//если слот не пустой и тип итема в слоте такой же как и у добавляемого итема
                //и слот свободен для добавления итемов
                Slot tmp = slot.GetComponent<Slot>();
                if (!tmp.isEmpty)
                {
                    if (tmp.CurrentItem.Item.ItemName == item.Item.ItemName && tmp.IsAvalible)
                    {
                        if (!InventoryManager.Instance.MovingSlot.isEmpty && InventoryManager.Instance.Clicked.GetComponent<Slot>() == tmp.GetComponent<Slot>())
                        {
                            continue;
                        }
                        else
                        {
                            //добавить итем в слот и уменьшить кол-во итемов в слоте
                            tmp.AddItem(item);
                            return true;
                        }

                    }
                }
            }
            if (emptySlots > 0)
            {
                return PlaceEmpty(item);
            }
        }
        return false;
    }
    /// <summary>
    /// Передвижение инвентаря
    /// </summary>
    private void MoveInventory()
    {
        Vector2 mousePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryManager.Instance.canvas.transform as RectTransform, new Vector3(Input.mousePosition.x - (inventoryRect.sizeDelta.x / 2 * InventoryManager.Instance.canvas.scaleFactor), Input.mousePosition.y + (inventoryRect.sizeDelta.y / 2 * InventoryManager.Instance.canvas.scaleFactor)), InventoryManager.Instance.canvas.worldCamera, out mousePos);

        transform.position = InventoryManager.Instance.canvas.transform.TransformPoint(mousePos);
    }
    /// <summary>
    /// Возвращает true если итем положили в  пустой слот
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private bool PlaceEmpty(ItemScript item)
    {
        if (emptySlots > 0)
        {
            foreach (GameObject slot in allSlots)
            {
                Slot tmp = slot.GetComponent<Slot>();
                if (tmp.isEmpty)
                {
                    tmp.AddItem(item);

                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// Перемещеение выбранного слота в другой слот
    /// </summary>
    /// <param name="clicked">тапнутый слот</param>
    public void MoveItem(GameObject clicked)
    {

        CanvasGroup cg = clicked.transform.parent.GetComponent<CanvasGroup>();

        if (cg != null && cg.alpha > 0 || clicked.transform.parent.GetComponent<Inventory>().isOpen)

        //  if (cg != null && cg.alpha > 0 || clicked.transform.parent.parent.GetComponent<CanvasGroup>().alpha>0)
        {


            InventoryManager.Instance.Clicked = clicked;//привязываем статическому clicked текущий

            if (!InventoryManager.Instance.MovingSlot.isEmpty)
            {
                Slot tmp = clicked.GetComponent<Slot>();

                if (tmp.isEmpty)//если выбранный слот пустой то мыжно просто положить в него итем
                {
                    tmp.AddItems(InventoryManager.Instance.MovingSlot.Items);//положить все итемы в выбраннный слот
                    InventoryManager.Instance.MovingSlot.Items.Clear();//очистить перемещаемый слот
                    Destroy(GameObject.Find("Hover"));//удалить hover объект
                    print("Происходит именно это");
                    CharactersPanel.Instance.CalcStats();
                }
                else if (!tmp.isEmpty && InventoryManager.Instance.MovingSlot.Items.Peek().Item.ItemName == tmp.CurrentItem.Item.ItemName && tmp.IsAvalible)
                {
                    //объеденить два слота одинакового типа
                    MergeStacks(InventoryManager.Instance.MovingSlot, tmp);
                    print("Merge 511");
                }
            }

            ///
            //назначения слота который будет перемещаться
            //  else if (InventoryManager.Instance.From == null && clicked.transform.parent.GetComponent<Inventory>().isOpen && !Input.GetKey(KeyCode.LeftShift))

            else if (InventoryManager.Instance.From == null && clicked.transform.parent.GetComponent<Inventory>().isOpen && !Input.GetKey(KeyCode.LeftShift))
            {
                if (!clicked.GetComponent<Slot>().isEmpty && !GameObject.Find("Hover"))//если выбранный слот не пустой
                {
                    InventoryManager.Instance.From = clicked.GetComponent<Slot>();//назначаем тот слот на который нажали, что он from
                    InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;//установить выбранному слоту белый цвет
                    CreateHoverIcon();
                }
            }

            ///
            else if (InventoryManager.Instance.To == null && !Input.GetKey(KeyCode.LeftShift))//выбрать слот в который переместится объект
            {
                InventoryManager.Instance.To = clicked.GetComponent<Slot>();//установить слот в который переместится объект
                Destroy(GameObject.Find("Hover"));//удалить перемещаемый объект
            }
            if (InventoryManager.Instance.To != null && InventoryManager.Instance.From != null)//если быбраны слот from и слот to то можем двигать слоты
            {
                if (!InventoryManager.Instance.To.isEmpty && InventoryManager.Instance.From.CurrentItem.Item.ItemName == InventoryManager.Instance.To.CurrentItem.Item.ItemName)
                {
                    print("Merge 539");
                    MergeStacks(InventoryManager.Instance.From, InventoryManager.Instance.To);
                }
                else if (!InventoryManager.Instance.To.isEmpty && InventoryManager.Instance.From.CurrentItem.Item.ItemName != InventoryManager.Instance.To.CurrentItem.Item.ItemName)
                {

                    ////перемещение слота
                    //print("Swap 546");
                    //Slot.SwapItems(InventoryManager.Instance.From, InventoryManager.Instance.To);

                }
                else if (InventoryManager.Instance.To.transform.parent.GetComponent<Transform>().name == "ChestInventory")
                {
                    int GetMoney;
                    //    InventoryManager.Instance.From = clicked.GetComponent<Slot>();
                    GetMoney = InventoryManager.Instance.From.CurrentItem.Item.Price * InventoryManager.Instance.From.Items.Count / 2;
                    PlayerHelper.Instance.GoldCur += GetMoney;
                    print("считаем бабло");
                    InventoryManager.Instance.From.ClearSlot();
                }
                else
                {
                    if (InventoryManager.Instance.To.transform.parent.GetComponent<Transform>().name=="InventoryBattle")
                    {


                        //перемещение слота
						if (InventoryManager.Instance.From.CurrentItem.Item.ItemName != "Свиток телепортации")
						{
							print (InventoryManager.Instance.Clicked.name);

							if(InventoryManager.Instance.Clicked.name == "Slot")
							{
								return;
							}
							else
						   {
							print (InventoryManager.Instance.From.CurrentItem.Item.ItemName);
							Slot.SwapItems (InventoryManager.Instance.From, InventoryManager.Instance.To);
						   }
                        }
                    }
                    else
                    {
                        print(InventoryManager.Instance.From.CurrentItem.Item.ItemName);
                        Slot.SwapItems(InventoryManager.Instance.From, InventoryManager.Instance.To);
                    }


                }
                //Сбросить значения
                InventoryManager.Instance.From.GetComponent<Image>().color = Color.gray;
                InventoryManager.Instance.To = null;
                InventoryManager.Instance.From = null;
                Destroy(GameObject.Find("Hover"));
            }


        }
    }
    /// <summary>
    /// Создание иконки при перетаскивании объекта
    /// </summary>
    private void CreateHoverIcon()
    {
        InventoryManager.Instance.HoverObject = (GameObject)Instantiate(InventoryManager.Instance.iconPrefab);//вставить объект иконки слота при перемещении
        InventoryManager.Instance.HoverObject.GetComponent<Image>().sprite = InventoryManager.Instance.Clicked.GetComponent<Image>().sprite;
        InventoryManager.Instance.HoverObject.name = "Hover";
        //создание ссылок на перемещение
        RectTransform hoverTransform = InventoryManager.Instance.HoverObject.GetComponent<RectTransform>();
        RectTransform clickedTransform = InventoryManager.Instance.Clicked.GetComponent<RectTransform>();
        //установить перемещаемому объекту размер такой же как и у выбранного слота
        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);
        //сделать перемещаемый объект дочерним от canvas
        InventoryManager.Instance.HoverObject.transform.SetParent(GameObject.Find("Canvas").transform, true);
        //подкорректировать размер 
        InventoryManager.Instance.HoverObject.transform.localScale = InventoryManager.Instance.Clicked.gameObject.transform.localScale;

        InventoryManager.Instance.HoverObject.transform.GetChild(0).GetComponent<Text>().text = InventoryManager.Instance.MovingSlot.Items.Count > 1 ? InventoryManager.Instance.MovingSlot.Items.Count.ToString() : string.Empty;

    }
    /// <summary>
    /// Положить Item обратно в инвентарь, если инвентарь закрыть при выбранном слоте
    /// </summary>
    public void PutItemBack()
    {
        if (InventoryManager.Instance.From != null)
        {
            Destroy(GameObject.Find("Hover"));
            InventoryManager.Instance.From.GetComponent<Image>().color = Color.gray;
            InventoryManager.Instance.From = null;
        }
        else if (!InventoryManager.Instance.MovingSlot.isEmpty)
        {
            Destroy(GameObject.Find("Hover"));
            foreach (ItemScript item in InventoryManager.Instance.MovingSlot.Items)
            {
                InventoryManager.Instance.Clicked.GetComponent<Slot>().AddItem(item);
            }

            InventoryManager.Instance.MovingSlot.ClearSlot();
        }

        InventoryManager.Instance.selectStackSize.SetActive(false);
    }
    /// <summary>
    /// Разделение итемов
    /// </summary>
    public void SplitStack()
    {
        InventoryManager.Instance.selectStackSize.SetActive(false);
        if (InventoryManager.Instance.SplitAmount == InventoryManager.Instance.MaxStackCount)
        {
            MoveItem(InventoryManager.Instance.Clicked);

        }
        else if (InventoryManager.Instance.SplitAmount > 0)
        {
            InventoryManager.Instance.MovingSlot.Items = InventoryManager.Instance.Clicked.GetComponent<Slot>().RemoveItems(InventoryManager.Instance.SplitAmount);

            CreateHoverIcon();
        }

    }
    /// <summary>
    /// Изменение текста о кол-ве итемов 
    /// </summary>
    /// <param name="i"></param>
    public void ChangeStackText(int i)
    {
        InventoryManager.Instance.SplitAmount += i;
        if (InventoryManager.Instance.SplitAmount < 0)
        {
            InventoryManager.Instance.SplitAmount = 0;
        }

        if (InventoryManager.Instance.SplitAmount > InventoryManager.Instance.MaxStackCount)
        {
            InventoryManager.Instance.SplitAmount = InventoryManager.Instance.MaxStackCount;
        }

        InventoryManager.Instance.stackTxt.text = InventoryManager.Instance.SplitAmount.ToString();
    }
    /// <summary>
    /// Объединение слотов
    /// </summary>
    /// <param name="destanation"></param>
    /// <param name="source"></param>
    private void MergeStacks(Slot destanation, Slot source)
    {
        int max = destanation.CurrentItem.Item.MaxSize - destanation.Items.Count;

        int count = source.Items.Count < max ? source.Items.Count : max;

        for (int i = 0; i < count; i++)
        {

            destanation.AddItem(source.RemoveItem());
            InventoryManager.Instance.HoverObject.transform.GetChild(0).GetComponent<Text>().text = InventoryManager.Instance.MovingSlot.Items.Count.ToString();
        }
        if (source.Items.Count == 0)
        {

            source.ClearSlot();
            Destroy(GameObject.Find("Hover"));
        }
    }
    /// <summary>
    /// Скрыть инвентарь
    /// </summary>
    /// <returns>ни чего не  взвращаем</returns>
    private IEnumerator FadeOut()
    {
        if (!fadingOut)
        {
            fadingOut = true;
            fadingIn = false;
            StopCoroutine("FadeIn");
            float startAlpha = canvasGroup.alpha;
            float rate = 1.0f / fadeTime;
            float progress = 0.0f;
            while (progress < 1.0f)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);//функция изменяющая значение "startAlpha" до 0 за время "progress"
                progress += rate * Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 0;
            fadingOut = false;//процесс закрытия инвентаря окончен

        }
        //условие чтобы отключать управление если открыт инвентарь или окно персонажа
        if (GameObject.Find("Inventory").GetComponent<CanvasGroup>().alpha > 0)
        {
            if (GameObject.Find("MobileControl"))
            {
                //  GameObject.Find("MobileControl").GetComponent<Canvas>().enabled = false;//включаем джостик
            }

        }

        else
        {
            if (GameObject.Find("MobileControl"))
            {
                //  GameObject.Find("MobileControl").GetComponent<Canvas>().enabled = true;//включаем джостик
            }

        }

    }
    /// <summary>
    /// Показать инвентарь
    /// </summary>
    /// <returns>ни чего не  взвращаем</returns>
    private IEnumerator FadeIn()
    {
        if (!fadingIn)
        {
            fadingOut = false;
            fadingIn = true;
            StopCoroutine("FadeOut");
            float startAlpha = canvasGroup.alpha;
            float rate = 1.0f / fadeTime;
            float progress = 0.0f;
            while (progress < 1.0f)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, progress);//функция изменяющая значение "startAlpha" до 1 за время "progress"
                progress += rate * Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 1;
            fadingIn = false;//процесс открытия инвентаря окончен

        }
        if (name != "ChestInventory") //если это не банк то отключаем перемещение во время открытия инвентаря
        {
            if (GameObject.Find("MobileControl"))
            {
                //  GameObject.Find("MobileControl").GetComponent<Canvas>().enabled = false;//отключаем джостик
            }

        }

    }
    /// <summary>
    /// показать или скрыть инвентарь в зависимости от прозрачности canvasGroup.alpha 
    /// привязываем к кнопке на сцене инвентаря
    /// </summary>
    public void ShowInventory()
    {

        if (canvasGroup.alpha > 0)
        {
            StartCoroutine("FadeOut");//скрыть инвентарь
            PutItemBack();//вернуть выбранный итем обратно
            InventoryManager.Instance.From = null;
            InventoryManager.Instance.To = null;
            HideToolTlp();
            isOpen = false;
            sound2.GetComponent<AudioSource>().PlayOneShot(SFX);
            MM.StartControl();
        }
        else
        {
            MM.StopControl();
            StartCoroutine("FadeIn");
            isOpen = true;
            sound2.GetComponent<AudioSource>().PlayOneShot(SFX);
			InventoryManager.Instance.From = null;
			InventoryManager.Instance.To = null;
        }
    }

    #endregion
}
