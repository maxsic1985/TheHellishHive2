using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MagicHalper : MonoBehaviour {

    private bool fadingIn;
    public CanvasGroup canvasGroup;
    private ArchievmentBtn actiBtn;
    private ArchievmentHalper ah;
    public ScrollRect scrRCT;
    private int index;
    GameObject[] tmp;
    GameObject ga;
    GameObject gaGO;
    GameObject WeaponShopBtn;
    GameObject WeaponShop;
    GameObject ConsBtn;
    GameObject ConsShop;
    void Start()
    {
        tmp = GameObject.FindGameObjectsWithTag("MagShop");
        gaGO = GameObject.Find("EqiepmentShop");
        ga = GameObject.Find("EqiepmentShopBtn");
        WeaponShop = GameObject.Find("WeaponShop");
        WeaponShopBtn = GameObject.Find("WeaponShopBtn");
        ConsShop = GameObject.Find("ConsumeableShop");
        ConsBtn = GameObject.Find("ConsumShopBtn");

        actiBtn = GameObject.Find("BuyButtonM").GetComponent<ArchievmentBtn>();
        actiBtn.Click();
      //  UpdateValue(0);
    }

    void Update()
    {
        for (int i = 0; i < tmp.Length; i++)
        {
            UpdateValue(i);

            switch (InventoryManager.Instance.ItemCont.Magics[i].ItemName)
            {
                case "Магия огня":
                    if (InventoryManager.Instance.ItemCont.Magics[i].Price <= PlayerHelper.Instance.GoldCur)//наличие необходимого золота
                    {
                        if (PlayerHelper.Instance.LvlPlayer >= 2)
                        {
                            tmp[i].GetComponent<IndexHalper>().image2.color = Color.white;
                            tmp[i].GetComponent<IndexHalper>().Blocked = false;
                        }



                        else
                        {
                            tmp[i].GetComponent<IndexHalper>().image2.color = Color.black;
                            tmp[i].GetComponent<IndexHalper>().Blocked = true;

                        }
                    }
                    break;
                default: break;
            }

            //if (PlayerHelper.Instance.GoldCur < InventoryManager.Instance.ItemCont.Consumeables[i].Price)
            //{
            //    tmp[i].GetComponent<IndexHalper>().image2.color = Color.black;//затемнить те итемы на которых не хватает денег
            //}
            //else
            //{
            //    tmp[i].GetComponent<IndexHalper>().image2.color = Color.white;//затемнить те итемы на которых не хватает денег

            //}
        }
        if (canvasGroup.alpha > 0)
        {
            ConsBtn.SetActive(false);
            ConsShop.SetActive(false);
            WeaponShopBtn.SetActive(false);
            WeaponShop.SetActive(false);
            ga.SetActive(false);
            gaGO.SetActive(false);
          
        }
        else
        {
            ga.SetActive(true);
            gaGO.SetActive(true);
            WeaponShopBtn.SetActive(true);
            WeaponShop.SetActive(true);
            ConsBtn.SetActive(true);
            ConsShop.SetActive(true);
        }
    }

    private void UpdateValue(int i)
    {
        tmp[i].GetComponent<IndexHalper>().Index = i;
        tmp[i].GetComponent<IndexHalper>().iname.text = InventoryManager.Instance.ItemCont.Magics[i].ItemName;
        tmp[i].GetComponent<IndexHalper>().description.text = InventoryManager.Instance.ItemCont.Magics[i].Description;
        tmp[i].GetComponent<IndexHalper>().price.text = InventoryManager.Instance.ItemCont.Magics[i].Price.ToString();
        tmp[i].GetComponent<IndexHalper>().Sprite = Resources.Load<Sprite>(InventoryManager.Instance.ItemCont.Magics[i].SpriteNeutral);
        tmp[i].GetComponent<IndexHalper>().image2.sprite = tmp[i].GetComponent<IndexHalper>().Sprite;
    }

    /// <summary>
    /// показать или скрыть инвентарь в зависимости от прозрачности canvasGroup.alpha 
    /// привязываем к кнопке на сцене инвентаря
    /// </summary>
    public void ShowInventory()
    {
        if (canvasGroup.alpha > 0)
        {
            StartCoroutine("FadeOut");//скрыть инвентарь
        }
        else
        {
            StartCoroutine("FadeIn");
        }
    }

    public void ChangeCategory(GameObject button)
    {
        ArchievmentBtn achievmentButton = button.GetComponent<ArchievmentBtn>();
        scrRCT.content = achievmentButton.achievmentList.GetComponent<RectTransform>();
        achievmentButton.Click();
        actiBtn.Click();
        actiBtn = achievmentButton;
    }

}
