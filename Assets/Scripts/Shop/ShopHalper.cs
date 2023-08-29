using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopHalper : MonoBehaviour
{
    public AudioSource bay1;
    public AudioClip bay;
    private ArchievmentBtn actiBtn;

    public ScrollRect scrRCT;
    private Inventory inv;
    // Use this for initialization
    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();

    }
    /// <summary>
    /// Купить зелье или свиток, привязать к кнопке в магазине
    /// </summary>
    /// <param name="item">порядковый номер итема в документе xml</param>
    public void ShopConsumeable(GameObject item)
    {

        if (inv.EmptySlots > 0)//наличие свободных ячеек в инвентаре
        {
            int it = item.GetComponent<IndexHalper>().Index;

            if (InventoryManager.Instance.ItemCont.Consumeables[it].Price <= PlayerHelper.Instance.GoldCur)//наличие необходимого золота
            {
                bay1.GetComponent<AudioSource>().PlayOneShot(bay);
                GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);
                tmp.AddComponent<ItemScript>();
                ItemScript newConsumeable = tmp.GetComponent<ItemScript>();
                newConsumeable.Item = InventoryManager.Instance.ItemCont.Consumeables[it];
                inv.AddItem(newConsumeable);
                Destroy(tmp);
                PlayerHelper.Instance.GoldCur -= newConsumeable.Item.Price;
            }

        }

    }

    public void ShopMagic(GameObject item)
    {

        if (inv.EmptySlots > 0)//наличие свободных ячеек в инвентаре
        {
            int it = item.GetComponent<IndexHalper>().Index;
            switch (InventoryManager.Instance.ItemCont.Magics[it].ItemName)
            {
                case "Магия огня":
                    if (InventoryManager.Instance.ItemCont.Magics[it].Price <= PlayerHelper.Instance.GoldCur)//наличие необходимого золота
                    {
                        if (PlayerHelper.Instance.LvlPlayer >= 2)
                        {
                            
                            GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);
                            tmp.AddComponent<ItemScript>();
                            ItemScript newMagic = tmp.GetComponent<ItemScript>();
                            newMagic.Item = InventoryManager.Instance.ItemCont.Magics[it];
                            inv.AddItem(newMagic);
                            Destroy(tmp);
                            PlayerHelper.Instance.GoldCur -= newMagic.Item.Price;
                        }


                    }
                    else
                    {
                      
                    }
                    break;
                default: break;
            }
          
        }

    }

    public void ShopEqiepment(GameObject item)
    {
        if (inv.EmptySlots > 0)
        {
            int it = item.GetComponent<IndexHalper>().Index;
            if (InventoryManager.Instance.ItemCont.Equipment[it].Price <= PlayerHelper.Instance.GoldCur)//наличие необходимого золота
            {
               bay1. GetComponent<AudioSource>().PlayOneShot(bay);
                GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);
                tmp.AddComponent<ItemScript>();
                ItemScript newEqip = tmp.GetComponent<ItemScript>();
                newEqip.Item = InventoryManager.Instance.ItemCont.Equipment[it];
                inv.AddItem(newEqip);
                Destroy(tmp);
                PlayerHelper.Instance.GoldCur -= newEqip.Item.Price;
            }
        }
    }

    public void ShopWeapon(GameObject item)
    {
        if (inv.EmptySlots > 0)
        {
            int it = item.GetComponent<IndexHalper>().Index;
            if (InventoryManager.Instance.ItemCont.Weapons[it].Price <= PlayerHelper.Instance.GoldCur)//наличие необходимого золота
            {
                bay1.GetComponent<AudioSource>().PlayOneShot(bay);
                GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);
                tmp.AddComponent<ItemScript>();
                ItemScript newWeap = tmp.GetComponent<ItemScript>();
                newWeap.Item = InventoryManager.Instance.ItemCont.Weapons[it];
                inv.AddItem(newWeap);
                Destroy(tmp);
                PlayerHelper.Instance.GoldCur -= newWeap.Item.Price;

            }
        }
    }


}
