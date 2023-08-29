using UnityEngine;
using System.Collections;

public class BattleInventory : Inventory
{
    public Inventory battleInv;
    public Slot[] slotsMassive;
    SpeedHelper sh;
    Inventory tmpInv;
    randomMob rm;
    private damage d;
    private PVP _pvp;
    private damage playerDamage;
    private hp playerHP;

    public override void CreateLayout()
    {
        GetComponentInParent<Transform>().parent.SetAsLastSibling();
    }
    public override void SaveInventory()
    {
        string content = string.Empty;

        for (int i = 0; i < slotsMassive.Length; i++)
        {
            if (!slotsMassive[i].isEmpty)
            {
                content += i + "-" + slotsMassive[i].Items.Peek().Item.ItemName + ";";
            }
            PlayerPrefs.SetString("BattlePanel", content);
            PlayerPrefs.Save();
        }
    }
    public override void LoadInventory()
    {
        foreach (Slot slot in slotsMassive)
        {
            slot.ClearSlot();
        }
        string content = PlayerPrefs.GetString("BattlePanel");
        string[] splirContent = content.Split(';');
        for (int i = 0; i < splirContent.Length - 1; i++)
        {
            string[] splitValues = splirContent[i].Split('-');
            int index = System.Int32.Parse(splitValues[0]);
            string itemName = splitValues[1];

            GameObject loaditem = Instantiate(InventoryManager.Instance.itemObject);
            loaditem.AddComponent<ItemScript>();
            loaditem.GetComponent<ItemScript>().Item = InventoryManager.Instance.ItemCont.Consumeables.Find(x => x.ItemName == itemName);
            slotsMassive[index].AddItem(loaditem.GetComponent<ItemScript>());
        }
    }
    public void UseItemBattle(Slot slot)
    {
        if (!slot.isEmpty)//Если слот не пустой
        {

            if (rm.MenuBattleBool)//если активно меню битвы
            {
                if ((sh.playerStep) && (rm.MenuBattleBool==true))//если ход игрока
                {
                    if (playerHP.HP > 0)
                    {
                        slot.UseItem(slot);//использовать еду или зелье
                        playerDamage.IsGo = true;
                        _pvp.selectPlayer = 0;
                        _pvp.selectTypeAtack = 0;
                        _pvp.SelectTypeAttack();
                        d.typeAttack = 0;
                        sh.playerStep = false;
                        rm.hideMenuBattle();
                        sh.endRound = false;
                        sh.WhoIsDamag();
                    }
                }

            }

        }



    }
    // Use this for initialization
    void Awake()
    {
        playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<damage>();
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<hp>();
        _pvp = FindObjectOfType<PVP>();
        d = FindObjectOfType<damage>();
        rm = FindObjectOfType<randomMob>();
        tmpInv = GameObject.Find("Inventory").GetComponent<Inventory>();
        sh = FindObjectOfType<SpeedHelper>();
        slotsMassive = transform.parent.GetComponentsInChildren<Slot>();
    }
}
