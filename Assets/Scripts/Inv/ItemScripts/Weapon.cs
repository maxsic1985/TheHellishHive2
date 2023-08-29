using UnityEngine;
using System.Collections;

public class Weapon : Equipment
{
    public int Atack { get; set; }

    public Weapon()
    { }
    public Weapon(string itemName, string description, ItemType itemType, Quality qulity, string spriteNeutral, string spriteHihglighed, int maxSize, int intellect, int agility, int stamina, int strenght, int atack,int price)
        :  base(itemName,description,itemType,qulity,spriteNeutral,spriteHihglighed,maxSize,intellect,agility,stamina,strenght,price)
    {
        this.Atack = atack;
    }



    public override void Use(Slot slot, ItemScript item)
    {
        CharactersPanel.Instance.EqipItem(slot, item);      

    }

    public override string GetTooltip()
    {
        
        string equipmentTip = base.GetTooltip();
        return string.Format("{0}" + "\nАтака: +" + "<size=14>{1}</size>",equipmentTip,Atack);
    }
}
 