using UnityEngine;
using UnityEngine.UI;

using System.Collections;
public enum ItemType { CONSUMEABLE, MAINHEND, TWOHAND, OFFHAND, HEAD, NECK, CHEST, RING, LEGS, BRACERS, BOOTS, TRINKET, SHOULDERS, BELT, GENERIC, GENERICWEAPON, MAGIC };//перечисление возможных типов объектов и инвентаре, оружие, зелье и прочее 
public enum Quality { COMMON, UNCOMMON, RARE, EPIC, LEGENDARY, ARTEFACT };//значимость объекта, для подсветки текста в описании предмета

public class ItemScript : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// нейтральный спрайт у итема
    /// </summary>
    public Sprite spriteNeutral;
    /// <summary>
    /// спрайт у выбранного итема
    /// </summary>
    public Sprite spriteHighlith;

    private Item item;
    #endregion
    #region Prop
    public Item Item
    {
        get { return item; }
        set
        {
           
            item = value;
            if (item!=null)
            {
                spriteHighlith = Resources.Load<Sprite>(value.SpriteHighlighted);
                spriteNeutral = Resources.Load<Sprite>(value.SpriteNeutral);
            }
          
        }
    }
    public void Use(Slot slot)
    {
        item.Use(slot, this);
    }
    public string GetToolTip()
    {
        return item.GetTooltip();
    }
    #endregion
    /// <summary>
    /// Для выкидывания объекта!!! устанавливает характеристики объекту которого выкинули такие же как и у объекта который выкидывали
    /// </summary>
    /// <param name="item"></param>
    //public void SetStats(Item item)
    //{

    //    this.type = item.type;

    //    this.quality = item.quality;

    //    this.neutral = item.neutral;

    //    this.highlith = item.highlith;

    //    this.maxItems = item.maxItems;

    //    this.strenght = item.strenght;

    //    this.agility = item.agility;

    //    this.intellect = item.intellect;

    //    this.stamina = item.stamina;

    //    this.itemNAme = item.itemNAme;

    //    this.description = item.description;

    //    switch (type)
    //    {
    //        case ItemType.mana:
    //            GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
    //            break;
    //        case ItemType.health:
    //            GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;

    //            break;
    //        case ItemType.armor:
    //            GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;

    //            break;
    //        default:
    //            break;
    //    }
    //}

}

