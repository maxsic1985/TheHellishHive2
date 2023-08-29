using UnityEngine;
using System.Collections;
using System;

public class Equipment : Item

{
    public int Intellect { get; set; }
    public int Agility { get; set; }
    public int Stamina { get; set; }
    public int Strenght { get; set; }

    public Equipment()
    { }
    public Equipment(string itemName, string description, ItemType itemType, Quality qulity, string spriteNeutral, string spriteHihglighed, int maxSize, int intellect, int agility, int stamina, int strenght,int price)
        : base(itemName, description, itemType, qulity, spriteNeutral, spriteHihglighed, maxSize,price)
    {
        this.Intellect = intellect;
        this.Agility = agility;
        this.Stamina = stamina;
        this.Strenght = strenght;
    }
    /// <summary>
    /// 
    /// </summary>
    public override void Use(Slot slot, ItemScript item)
    {
        CharactersPanel.Instance.EqipItem(slot, item);         
    }
    public override string GetTooltip()
    {
        
        
        string stats = string.Empty;

        //с новой строки вывести значении сила в ToolTip
        if (Strenght > 0)
        {
            stats += "\nСкорость: + " + Strenght.ToString();
        }
        //с новой строки вывести значение интеллекта в ToolTip
        if (Intellect > 0)
        {
            stats += "\nИнтеллект: + " + Intellect.ToString();
        }
        //с новой строки вывести значение ловкости в ToolTip
        if (Agility > 0)
        {
            stats += "\nЛовкость: + " + Agility.ToString();
        }
        //с новой строки вывести значение удачи в ToolTip
        if (Stamina > 0)
        {
            stats += "\nЗащита: + " + Stamina.ToString();
        }
        string itemTip = base.GetTooltip();

        return string.Format("{0}" + "<size=14>{1}</size>", itemTip, stats);
    }
}
