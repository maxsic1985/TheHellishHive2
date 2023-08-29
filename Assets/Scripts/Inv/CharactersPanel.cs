using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class CharactersPanel : Inventory
{

    #region Variables
    /// <summary>
    /// массив слотов на панели персонажа
    /// </summary>
    public Slot[] eqipmentSlots;
    /// <summary>
    /// синглтон для панели персонажа
    /// </summary>
    private static CharactersPanel instance;
    #endregion
    #region Properties
    public static CharactersPanel Instance//Singleton
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CharactersPanel>();
            }
            return CharactersPanel.instance;
        }

    }
    /// <summary>
    /// Своийство для проверки что в руке  меч 
    /// </summary>
    public Slot WeaponSlot
    {
        get { return eqipmentSlots[1]; }
    }
    /// <summary>
    /// Своийство для проверки, что в руке щит
    /// </summary>
    public Slot OffHeandSlot
    {
        get { return eqipmentSlots[2]; }
    }
    #endregion
    #region Voids

    void Awake()
    {
        eqipmentSlots = transform.parent.GetComponentsInChildren<Slot>();
    }

    public override void CreateLayout()
    {
        GetComponentInParent<Transform>().parent.parent.SetAsLastSibling();
    }
    /// <summary>
    /// Перемещение итема на панель персонажа
    /// </summary>
    /// <param name="slot"></param>
    /// <param name="item"></param>
    public void EqipItem(Slot slot, ItemScript item)
    {
        if (item.Item.ItemType == ItemType.MAINHEND || item.Item.ItemType == ItemType.TWOHAND && OffHeandSlot.isEmpty)
        {
            Slot.SwapItems(slot, WeaponSlot);

        }
        else
        {
            Slot.SwapItems(slot, Array.Find(eqipmentSlots, x => x.canContain == item.Item.ItemType));
        }
    }
    /// <summary>
    /// Описание итема
    /// </summary>
    /// <param name="slot"></param>
    public override void ShowToolTlp(GameObject slot)
    {
        InventoryManager.Instance.toolObject.GetComponent<RectTransform>().transform.SetAsLastSibling();//вывести на передний фон
        //логика, если выьранный слот не пустой то показать окно ToolTip
        Slot tmpslot = slot.GetComponent<Slot>();//временная переменая слот
        if (slot.GetComponentInParent<Inventory>().IsOpen && !tmpslot.isEmpty && InventoryManager.Instance.HoverObject == null && !InventoryManager.Instance.selectStackSize.activeSelf)
        {

            InventoryManager.Instance.visualTextObject.text = tmpslot.CurrentItem.GetToolTip();
            InventoryManager.Instance.sizeTextObject.text = InventoryManager.Instance.visualTextObject.text;
            InventoryManager.Instance.toolObject.SetActive(true);
            InventoryManager.Instance.toolObject.transform.position = slot.transform.position;

        }
    }

    public void CalcStats()
    {
        int atack = 0;
        int strenght = 0;
        int stamina = 0;
        int intellect = 0;
     

        foreach (Slot slot in eqipmentSlots)
        {

            if (!slot.isEmpty && slot.CurrentItem.Item.ItemType != ItemType.CONSUMEABLE)
            {
                Equipment e = (Equipment)slot.CurrentItem.Item;

                strenght += e.Strenght;
                stamina += e.Stamina;
                intellect += e.Intellect;
                if (slot.CurrentItem.Item.ItemType == ItemType.TWOHAND || slot.CurrentItem.Item.ItemType == ItemType.MAINHEND)
                {
                    Weapon w = (Weapon)slot.CurrentItem.Item;
                    atack += w.Atack;
                }
            }
            else
            {
                slot.UseItem(slot);
            }

        }

        PlayerHelper.Instance.SetStats(atack, strenght, stamina, intellect);
    }

    public override void SaveInventory()
    {
        string content = string.Empty;

        for (int i = 0; i < eqipmentSlots.Length; i++)
        {
            if (!eqipmentSlots[i].isEmpty)
            {
                content += i + "-" + eqipmentSlots[i].Items.Peek().Item.ItemName + ";";
            }
            PlayerPrefs.SetString("CharPanel", content);
            PlayerPrefs.Save();
        }
    }

    public override void LoadInventory()
    {
        foreach (Slot slot in eqipmentSlots)
        {
            slot.ClearSlot();
        }
        string content = PlayerPrefs.GetString("CharPanel");
        string[] splirContent = content.Split(';');
        for (int i = 0; i < splirContent.Length - 1; i++)
        {
            string[] splitValues = splirContent[i].Split('-');
            int index = Int32.Parse(splitValues[0]);
            string itemName = splitValues[1];

            GameObject loaditem = Instantiate(InventoryManager.Instance.itemObject);
            loaditem.AddComponent<ItemScript>();
            // if (index == 1 || index == 2)  индекс формируется из content типа 2-кожанный браслет;
            if (index == 1)
            {
                loaditem.GetComponent<ItemScript>().Item = InventoryManager.Instance.ItemCont.Weapons.Find(x => x.ItemName == itemName);
            }

            else
            {
                loaditem.GetComponent<ItemScript>().Item = InventoryManager.Instance.ItemCont.Equipment.Find(x => x.ItemName == itemName);
            }
            eqipmentSlots[index].AddItem(loaditem.GetComponent<ItemScript>());

            CalcStats();

        }
    }




    #endregion
}
