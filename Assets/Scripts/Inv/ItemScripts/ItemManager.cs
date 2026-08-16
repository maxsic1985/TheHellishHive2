using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Xml.Serialization;
using YG;

public enum Category {EQUIPMENT, WEAPON, CONSUMEABLE, MAGIC }

public class ItemManager : MonoBehaviour
{
    #region Variables
    public Category category;

    public ItemType itemType;

    public Quality quality;

    public string spriteNeutral;

    public string spriteHighlighted;

    public string itemName;

    public string description;

    public int maxSize;

    public int intellect;

    public int agility;

    public int stamina;

    public int strenght;

    public int atack;

    public int health;

    public int mana;

    public int price;

    public int level;

    #endregion

    public void CreateItem()
    {
        ItemContainer itemContainer = new ItemContainer();

        Type[] itemTypes = { typeof(Equipment), typeof(Weapon), typeof(Consumeable) , typeof(Magic) };

        // FileStream fs = new FileStream(Path.Combine(Application.streamingAssetsPath, "Items.xml"), FileMode.Open);
        Stream fs = null;
        switch (YG2.envir.language)
        {
            case "ru":
                 fs = new FileStream(Application.dataPath + "/Resources/Items.xml", FileMode.Open,  FileAccess.ReadWrite);
                break;
            case "en":
                 fs = new FileStream(Application.dataPath + "/Resources/ItemsEn.xml", FileMode.Open,  FileAccess.ReadWrite);
                break;
            default:
                 fs = new FileStream(Application.dataPath + "/Resources/Items.xml", FileMode.Open,  FileAccess.ReadWrite);
                break;
        } 
        
      

        XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer), itemTypes);

        itemContainer = (ItemContainer)serializer.Deserialize(fs);

        serializer.Serialize(fs, itemContainer);

        fs.Close();

        switch (category)
        {
            case Category.EQUIPMENT:
                itemContainer.Equipment.Add(new Equipment(itemName, description, itemType, quality, spriteNeutral, spriteHighlighted, maxSize, intellect, agility, stamina, strenght,price));
                break;
            case Category.WEAPON:
                itemContainer.Weapons.Add(new Weapon(itemName, description, itemType, quality, spriteNeutral, spriteHighlighted, maxSize, intellect, agility, stamina, strenght,atack,price));            
                break;
            case Category.CONSUMEABLE:
                itemContainer.Consumeables.Add(new Consumeable(itemName, description, itemType, quality, spriteNeutral, spriteHighlighted, maxSize,health,mana,price));
                break;
            case Category.MAGIC:
                itemContainer.Magics.Add(new Magic(itemName, description, itemType, quality, spriteNeutral, spriteHighlighted, maxSize, level, mana, price));
                break;
            default:
                break;
        }
        switch (YG2.envir.language)
        {
            case "ru":
                fs = new FileStream(Application.dataPath + "/Resources/Items.xml", FileMode.Create, FileAccess.ReadWrite);
                break;
            case "en":
                fs = new FileStream(Application.dataPath + "/Resources/ItemsEn.xml", FileMode.Create, FileAccess.ReadWrite);
                break;
            default:
                fs = new FileStream(Application.dataPath + "/Resources/Items.xml", FileMode.Create, FileAccess.ReadWrite);
                break;
        } 
      
        serializer.Serialize(fs, itemContainer);
        fs.Close();
    }

    public void Load0()

    {

        Application.LoadLevel(0);
    }

}
