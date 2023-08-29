using UnityEngine;
using System.Collections;

public class Magic : Item 
{

	
    public int Level { get; set; }

    public int Mana { get; set; }

    public bool Blocked { get; set; }

    public Magic()
    { }

    public Magic(string itemName, string description, ItemType itemType, Quality qulity, string spriteNeutral, string spriteHihglighed, int maxSize, int level, int mana, int price)
        : base(itemName, description, itemType, qulity, spriteNeutral, spriteHihglighed, maxSize, price)
    {
        this.Level = level;
        this.Mana = mana;
    }

    public override void Use(Slot slot, ItemScript item)
    {
        //Debug.Log("Used " + ItemName);
        //slot.RemoveItem();
        //switch (ItemName)
        //{
        //    case "Зелье маны": PlayerHelper.Instance.ManaCur += maxPotionUse(80, PlayerHelper.Instance.ManaMax, PlayerHelper.Instance.ManaCur); break;
        //    case "Лечебное зелье": PlayerHelper.Instance.HpCur += maxPotionUse(80, PlayerHelper.Instance.HpMax, PlayerHelper.Instance.HpCur); break;
        //    case "Свиток телепортации": SaveHalper.Instance.Save(); SceneManager.LoadScene(1); ; break;
        //    default:
        //        break;
        //}
    }
    /// <summary>
    /// Перезагруженный м-т вывода информации об итеме при выборе итема
    /// </summary>
    /// <returns></returns>
    public override string GetTooltip()
    {
        string stats = string.Empty;
        //с новой строки вывести значении хп
        if (Level > 0)
        {
            stats += "\n+" + Level.ToString() + "Health";
        }
        //с новой строки вывести значении mp
        if (Mana > 0)
        {
            stats += "\n+" + Mana.ToString() + "Mana";
        }

        string itemTip = base.GetTooltip();

        return string.Format("{0}" + "<size=14> {1}</size>", itemTip, stats);
    }
    /// <summary>
    /// расчет количества восполненного хп или мп после использования баночки
    /// чтобы воссполняемое количество не превышало максимально возможное на текущем уровне персонажа
    /// </summary>
    /// <param name="potionValue">кол-во восполняемого хп или мп</param>
    /// <param name="maxValue">максимальное значение на данном уровне</param>
    /// <param name="curentValue">текущее значение</param>
    /// <returns></returns>
    private int maxPotionUse(int potionValue, int maxValue, int curentValue)
    {
        int x = maxValue - curentValue;//временная переменная в которой хранится разность м-у максимальным и текущим значением
        if (x>potionValue)//если разность больше воссполнямого значения баночки то прибавить значение восполнения банк 
        {
            return potionValue;
        }
        else
        {
            return x; //иначе прибавить разность
        }
    }

    private IEnumerator GoTOMainMenu()
    {
       
         yield return null;
    }
}
