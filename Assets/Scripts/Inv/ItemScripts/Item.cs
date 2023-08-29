using UnityEngine;
using System.Collections;

public abstract class Item
{

    public ItemType ItemType { get; set; }

    public Quality Quality { get; set; }

    public string SpriteNeutral { get; set; }

    public string SpriteHighlighted { get; set; }

    public int MaxSize { get; set; }

    public string ItemName { get; set; }

    public string Description { get; set; } 

    public int Price { get; set; }

    public Item()
    {

    }

    public Item(string itemName, string description, ItemType itemType, Quality qulity, string spriteNeutral, string spriteHihglighed, int maxSize, int price)
    {
        this.ItemName = itemName;
        this.Description = description;
        this.ItemType = itemType;
        this.Quality = qulity;
        this.SpriteNeutral = spriteNeutral;
        this.SpriteHighlighted = spriteHihglighed;
        this.MaxSize = maxSize;
        this.Price = price;


    }

    public abstract void Use(Slot slot, ItemScript item);

    public virtual string GetTooltip()
    {
        string stats = string.Empty;
        string color = string.Empty;
        string newLine = string.Empty;

        if (Description != string.Empty)
        {
            newLine = "\n";//перевод строки
        }

        switch (Quality)//установка цветов текста в описании итема 
        {
            case Quality.COMMON:
                color = "white";
                break;
            case Quality.UNCOMMON:
                color = "lime";
                break;
            case Quality.RARE:
                color = "navy";
                break;
            case Quality.EPIC:
                color = "magenta";
                break;
            case Quality.LEGENDARY:
                color = "orange";
                break;
            case Quality.ARTEFACT:
                color = "red";
                break;
        }
       
        return string.Format("<color=" + color + "><size=16>{0}</size></color><size=14><i><color=lime>" + newLine + "{1}</color></i>\n{3}</size>", ItemName, Description, ItemType.ToString().ToLower(), "Цена: "+ Price.ToString());

    }
}