using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Consumeable : Item
{
    public int Health { get; set; }

    public int Mana { get; set; }

    public Consumeable()
    {
    }

    public Consumeable(string itemName, string description, ItemType itemType, Quality qulity, string spriteNeutral,
        string spriteHihglighed, int maxSize, int healh, int mana, int price)
        : base(itemName, description, itemType, qulity, spriteNeutral, spriteHihglighed, maxSize, price)
    {
        this.Health = healh;
        this.Mana = mana;
    }

    public override void Use(Slot slot, ItemScript item)
    {
        Debug.Log("Used " + slot.CurrentItem.Item.ItemName);
        Debug.Log("HP " + Health);
        Debug.Log("MP " + Mana);
        slot.RemoveItem();
        switch (ItemName)
        {
            case "Корень маны":
                PlayerHelper.Instance.ManaCur +=
                    maxPotionUse(Mana, PlayerHelper.Instance.ManaMax, PlayerHelper.Instance.ManaCur);
                break;
            case "Mana Root":
                PlayerHelper.Instance.ManaCur +=
                    maxPotionUse(Mana, PlayerHelper.Instance.ManaMax, PlayerHelper.Instance.ManaCur);
                break;

            case "Зелье маны":
                PlayerHelper.Instance.ManaCur +=
                    maxPotionUse(Mana, PlayerHelper.Instance.ManaMax, PlayerHelper.Instance.ManaCur);
                break;
            case "Mana Potion":
                PlayerHelper.Instance.ManaCur +=
                    maxPotionUse(Mana, PlayerHelper.Instance.ManaMax, PlayerHelper.Instance.ManaCur);
                break;

            case "Лечебное зелье":
                PlayerHelper.Instance.HpCur +=
                    maxPotionUse(Health, PlayerHelper.Instance.HpMax, PlayerHelper.Instance.HpCur);
                break;
            case "Healing Potion":
                PlayerHelper.Instance.HpCur +=
                    maxPotionUse(Health, PlayerHelper.Instance.HpMax, PlayerHelper.Instance.HpCur);
                break;

            case "Бинт":
                PlayerHelper.Instance.HpCur +=
                    maxPotionUse(Health, PlayerHelper.Instance.HpMax, PlayerHelper.Instance.HpCur);
                break;
            case "Bandage":
                PlayerHelper.Instance.HpCur +=
                    maxPotionUse(Health, PlayerHelper.Instance.HpMax, PlayerHelper.Instance.HpCur);
                break;
            //для травы проверяем имеется ли на сцене объект AchievmentManager чтобы не было ошибки при использовании
            //на рынке и если используем траву в бою то открывается достижение
            case "Лечебная трава":
                PlayerHelper.Instance.HpCur +=
                    maxPotionUse(Health, PlayerHelper.Instance.HpMax, PlayerHelper.Instance.HpCur);
                if (AchievmentManager.Instance && GameObject.FindObjectOfType<_randomMob>().addMob)
                {
                    AchievmentManager.Instance.UseTrava = true;
                }

                break;

            case "Healing Herb":
                PlayerHelper.Instance.HpCur +=
                    maxPotionUse(Health, PlayerHelper.Instance.HpMax, PlayerHelper.Instance.HpCur);
                if (AchievmentManager.Instance && GameObject.FindObjectOfType<_randomMob>().addMob)
                {
                    AchievmentManager.Instance.UseTrava = true;
                }

                break;

            case "Зелье восстановления":
                PlayerHelper.Instance.HpCur +=
                    maxPotionUse(Health, PlayerHelper.Instance.HpMax, PlayerHelper.Instance.HpCur);
                PlayerHelper.Instance.ManaCur +=
                    maxPotionUse(Mana, PlayerHelper.Instance.ManaMax, PlayerHelper.Instance.ManaCur);
                break;
            case "Restoration Potion":
                PlayerHelper.Instance.HpCur +=
                    maxPotionUse(Health, PlayerHelper.Instance.HpMax, PlayerHelper.Instance.HpCur);
                PlayerHelper.Instance.ManaCur +=
                    maxPotionUse(Mana, PlayerHelper.Instance.ManaMax, PlayerHelper.Instance.ManaCur);
                break;

            case "Зелье исцеления":
                PlayerHelper.Instance.HpCur +=
                    maxPotionUse(Health, PlayerHelper.Instance.HpMax, PlayerHelper.Instance.HpCur);
                break;
            case "Antidote":
                PlayerHelper.Instance.HpCur +=
                    maxPotionUse(Health, PlayerHelper.Instance.HpMax, PlayerHelper.Instance.HpCur);
                break;

            case "Свиток телепортации":
                SaveHalper.Instance.Save();
                LoadManager.levelName = "GameMenu";
                SceneManager.LoadScene("LoadScene");
                break;
            case "Scroll of Teleportation":
                SaveHalper.Instance.Save();
                LoadManager.levelName = "GameMenu";
                SceneManager.LoadScene("LoadScene");
                break;

            default:
                break;
        }

        SaveHalper.Instance.Save();
    }

    /// <summary>
    /// Перезагруженный м-т вывода информации об итеме при выборе итема
    /// </summary>
    /// <returns></returns>
    public override string GetTooltip()
    {
        string stats = string.Empty;
        //с новой строки вывести значении хп
        if (Health > 0)
        {
            stats += "\n+" + Health.ToString() + "Health";
        }

        //с новой строки вывести значении mp
        if (Mana > 0)
        {
            stats += "\n+" + Mana.ToString() + "Mana";
        }

        string itemTip = base.GetTooltip();

        return string.Format("{0}", itemTip);
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
        int x = maxValue -
                curentValue; //временная переменная в которой хранится разность м-у максимальным и текущим значением
        if (x >= potionValue) //если разность больше воссполнямого значения баночки то прибавить значение восполнения банк 
        {
            return potionValue;
        }

        return x; //иначе прибавить разност
    }

    private IEnumerator GoTOMainMenu()
    {
        yield return null;
    }
}