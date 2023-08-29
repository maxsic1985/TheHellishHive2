using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    #region VARIABLES
    private CanvasGroup canvasGroup;
    private Stack<ItemScript> items; //куча
    /// <summary>
    /// Коичество итемов в ячеййке Text
    /// </summary>
    public Text stackText;
    /// <summary>
    /// Спрайт пустого слота
    /// </summary>
    public Sprite slotEmpty;
    /// <summary>
    /// Спрайт выделенного слота
    /// </summary>
    public Sprite slotHighLight;
    /// <summary>
    /// 
    /// </summary>
    public ItemType canContain;
    #endregion
    #region PROPERTIES
    /// <summary>
    /// Нет итемов в выбраном слоте
    /// </summary>
    public bool isEmpty
    {
        get { return items.Count == 0; }
    }
    /// <summary>
    /// ячейка доступна, не переполнена? максимальным кол-ом однотипных итемов в слоте
    /// </summary>
    public bool IsAvalible
    {
        get { return CurrentItem.Item.MaxSize > items.Count; }
    }

    /// <summary>
    /// текущий итем в ячейке
    /// </summary>
    public ItemScript CurrentItem
    {
        get { return items.Peek(); }
    }
    /// <summary>
    /// Итемы в ячейке
    /// </summary>
    public Stack<ItemScript> Items
    {
        get
        {
            return items;
        }

        set
        {
            items = value;
        }
    }
    #endregion
    #region VOIDS
    void Awake()
    {
        items = new Stack<ItemScript>();//создать стэк из итемов
    }

    // Use this for initialization
    void Start()
    {
        //ссылка на RectTransform слота
        RectTransform slotRect = GetComponent<RectTransform>();
        //ссылка на RectTransform количества итемов в слоте
        RectTransform txtRect = stackText.GetComponent<RectTransform>();
        //расчет шкалы текста
        int txtScaleFactory = (int)(slotRect.sizeDelta.x * 0.6);
        //установка максимального и минимального размера шрифта текста количества итемов в слоте
        stackText.resizeTextMaxSize = txtScaleFactory;
        stackText.resizeTextMinSize = txtScaleFactory;
        //установить размер шрифта
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);

        if (transform.parent != null)
        {
            Transform p = transform.parent;
            while (canvasGroup == null && p != null)
            {
                canvasGroup = p.GetComponent<CanvasGroup>();
                p = p.parent;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Добавить итем в слот
    /// </summary>
    /// <param name="item">Добавляемый итем</param>
    public void AddItem(ItemScript item)
    {

        if (isEmpty)
        {
            transform.parent.GetComponent<Inventory>().EmptySlots--;//убавить количество пустых слотов в инвентаре
        }
        items.Push(item);//в кучу добавить выбраный итем
        if (items.Count > 1)//если элемент в куче больше 1
        {
            stackText.text = items.Count.ToString();//вывести количество итемов в слоте
        }
        ChangeSprite(item.spriteNeutral, item.spriteHighlith);//сменить спрайты
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="items"></param>
    public void AddItems(Stack<ItemScript> items)
    {
        this.items = new Stack<ItemScript>(items);
        stackText.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
        ChangeSprite(CurrentItem.spriteNeutral, CurrentItem.spriteHighlith);
    }
    /// <summary>
    /// Подмена спрайтов
    /// </summary>
    /// <param name="neutrall"></param>
    /// <param name="hightlightt"></param>
    private void ChangeSprite(Sprite neutrall, Sprite hightlightt)
    {
        GetComponent<Image>().sprite = neutrall;
        SpriteState st = new SpriteState();
        st.highlightedSprite = hightlightt;
        st.pressedSprite = neutrall;
        GetComponent<Button>().spriteState = st;
    }
    /// <summary>
    /// Использовать Итем в слоте
    /// </summary>
    /// <param name="slot">выбранный слот</param>
    public void UseItem(Slot slot)
    {
        GameObject tmpBtn = GameObject.FindGameObjectWithTag("UseSlot");
        if (!isEmpty)
        {
            slot.items.Peek().Use(this);//удалить верхний итем в стеке и использовать его
            stackText.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
            if (isEmpty)
            {
                ChangeSprite(slotEmpty, slotHighLight);//сменить спрайты
                transform.parent.GetComponent<Inventory>().EmptySlots++;//добавить 1 к пустому слоту              
            }
        }
    }
    /// <summary>
    ///Очистить слот 
    /// </summary>
    public void ClearSlot()
    {

        items.Clear();
        //сменить спрайт на пустой
        ChangeSprite(slotEmpty, slotHighLight);
        //очистить текст
        stackText.text = string.Empty;
        if (transform.parent != null)
        {
            transform.parent.GetComponent<Inventory>().EmptySlots++;
        }
    }
    /// <summary>
    /// Удалить введеное количество итемов
    /// </summary>
    /// <param name="amount">количество</param>
    /// <returns></returns>
    public Stack<ItemScript> RemoveItems(int amount)
    {
        Stack<ItemScript> tmp = new Stack<ItemScript>();

        for (int i = 0; i < amount; i++)
        {
            tmp.Push(items.Pop());
        }

        stackText.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

        return tmp;
    }
    /// <summary>
    /// Удалить итем
    /// </summary>
    /// <returns></returns>
    public ItemScript RemoveItem()
    {

        ItemScript tmp;

        tmp = items.Pop();

        stackText.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

        return tmp;
    }
    /// <summary>
    /// Нажатие на слоты
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        Slot from = GetComponent<Slot>();
        if (eventData.button == PointerEventData.InputButton.Right && !GameObject.Find("Hover") && canvasGroup != null && canvasGroup.alpha > 0)//нажатие на правую кнопку мыши
        {
            UseItem(from);
        }
        else if (eventData.button == PointerEventData.InputButton.Left && !isEmpty && from.CurrentItem.Item.ItemType == ItemType.CONSUMEABLE)
        {
            // Vector2 position;

            //   RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryManager.Instance.canvas.transform as RectTransform, Input.mousePosition, InventoryManager.Instance.canvas.worldCamera, out position);

            //  InventoryManager.Instance.selectStackSize.SetActive(true);

            //  InventoryManager.Instance.selectStackSize.transform.position = InventoryManager.Instance.canvas.transform.TransformPoint(position);

            //   InventoryManager.Instance.selectStackSize.GetComponent<RectTransform>().transform.SetAsLastSibling();//установить элемент на передний фронт в Canvas

            InventoryManager.Instance.SetStackInfo(items.Count);
        }
        //else if (eventData.button == PointerEventData.InputButton.Left && from.CurrentItem.Item.ItemType==ItemType.CONSUMEABLE)
        //{
        //    UseItem(from);
        //   Destroy(GameObject.Find("Hover"));
        //   InventoryManager.Instance.toolObject.SetActive(false);
        //    SwapItems(from,from);



        //}
    }//клики мышкой или тап

    /// <summary>
    /// ППеремещение слотов
    /// </summary>
    public static void SwapItems(Slot from, Slot to)
    {
        ItemType movingType = from.CurrentItem.Item.ItemType;
        if (to != null && from != null)
        {
            bool calcStats = from.transform.parent == CharactersPanel.Instance.transform || to.transform.parent == CharactersPanel.Instance.transform;

            if (movingType == ItemType.TWOHAND && CharactersPanel.Instance.OffHeandSlot.isEmpty || movingType == ItemType.MAINHEND)
            {
                movingType = ItemType.GENERICWEAPON;
            }
            if (to.canContain == ItemType.GENERIC || movingType == to.canContain)
            {
                //если одет двуручный меч то нельзя одевать щит
                if (movingType != ItemType.OFFHAND || (CharactersPanel.Instance.WeaponSlot.isEmpty || CharactersPanel.Instance.WeaponSlot.CurrentItem.Item.ItemType != ItemType.TWOHAND))
                {


                    Stack<ItemScript> tmpTo = new Stack<ItemScript>(to.Items);
                    to.AddItem(from.Items.Pop());//записать итем из from в  to 
                    if (movingType == ItemType.CONSUMEABLE)
                    {

                        if (from.isEmpty)
                        {

                            from.ClearSlot();
                            to.transform.parent.GetComponent<Inventory>().EmptySlots--;
                        }
                        else
                        {
                            from.stackText.text = from.items.Count > 1 ? from.items.Count.ToString() : string.Empty;
                        }
                    }
                    else if (tmpTo.Count == 0)//перемещение в пустую ячейку
                    {
                        //  print("this");
                        //  to.transform.parent.GetComponent<Inventory>().EmptySlots--;

                        from.ClearSlot();  //очистить from слот



                    }
                    else
                    {

                        from.AddItems(tmpTo);//если слот to  содержит итем то скопировать этот итем в from
                    }

                }
                else
                {

                    //если перемещается слот в самом инвентаре или в банке 
                    if (to.transform.parent.GetComponent<Transform>().name == "Inventory" || to.transform.parent.GetComponent<Transform>().name == "ChestInventory")
                    {
                        Stack<ItemScript> tmpTo = new Stack<ItemScript>(to.Items);
                        to.AddItems(from.Items);//записать итем из from в  to 

                        if (tmpTo.Count == 0)
                        {
                            to.transform.parent.GetComponent<Inventory>().EmptySlots--;
                            from.ClearSlot();  //очистить from слот

                        }
                        else if (true)
                        {

                        }
                        else
                        {
                            from.AddItems(tmpTo);//если слот to  содержит итем то скопировать этот итем в from
                        }
                    }
                }
            }
            if (calcStats)
            {
                CharactersPanel.Instance.CalcStats();
            }
        }

    }


    #endregion

}
