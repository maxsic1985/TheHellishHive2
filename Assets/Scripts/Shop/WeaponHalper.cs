using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class WeaponHalper : MonoBehaviour {



    private bool fadingIn;
    public CanvasGroup canvasGroup;
    private bool fadingOut;
    public float fadeTime;

    private ArchievmentBtn actiBtn;
    private ArchievmentHalper ah;
    public ScrollRect scrRCT;
    private int index;
    GameObject[] tmp;
    GameObject gaBtn;
    GameObject gaGO;
    GameObject WeaponShopBtn;
    GameObject WeaponShop;
    GameObject MagicBtn;
    GameObject MagicShop;
    void Start()
    {
        gaGO = GameObject.Find("ConsumeableShop");
        gaBtn = GameObject.Find("ConsumShopBtn");
        MagicShop = GameObject.Find("MagShop");
        MagicBtn = GameObject.Find("MagicShopBtn");
        WeaponShop = GameObject.Find("EqiepmentShop");
        WeaponShopBtn = GameObject.Find("EqiepmentShopBtn");

        tmp = GameObject.FindGameObjectsWithTag("WeapShop");
        actiBtn = GameObject.Find("BuyButtonW").GetComponent<ArchievmentBtn>();
        actiBtn.Click();

    }

    void Update()
    {
        if (canvasGroup.alpha > 0)
        {
            gaBtn.SetActive(false);
            gaGO.SetActive(false);
            MagicShop.SetActive(false);
            MagicBtn.SetActive(false);
            WeaponShopBtn.SetActive(false);
            WeaponShop.SetActive(false);

            for (int i = 0; i < tmp.Length; i++)
            {
                tmp[i].GetComponent<IndexHalper>().Index = i;
                tmp[i].GetComponent<IndexHalper>().iname.text = InventoryManager.Instance.ItemCont.Weapons[i].ItemName;
                tmp[i].GetComponent<IndexHalper>().description.text = InventoryManager.Instance.ItemCont.Weapons[i].Description;
                tmp[i].GetComponent<IndexHalper>().price.text = InventoryManager.Instance.ItemCont.Weapons[i].Price.ToString();
                tmp[i].GetComponent<IndexHalper>().Sprite = Resources.Load<Sprite>(InventoryManager.Instance.ItemCont.Weapons[i].SpriteNeutral);
                tmp[i].GetComponent<IndexHalper>().image2.sprite = tmp[i].GetComponent<IndexHalper>().Sprite;
                tmp[i].GetComponent<RectTransform>().transform.SetAsLastSibling();//для утого чтобы упорядочить

                if (PlayerHelper.Instance.GoldCur < InventoryManager.Instance.ItemCont.Weapons[i].Price)
                {
                    tmp[i].GetComponent<IndexHalper>().image2.color = Color.black;//затемнить те итемы на которых не хватает денег
                }
                else
                {
                    tmp[i].GetComponent<IndexHalper>().image2.color = Color.white;//затемнить те итемы на которых не хватает денег

                }
            }
        }
        else
        {
            gaGO.SetActive(true);
            gaBtn.SetActive(true);
            MagicShop.SetActive(true);
            MagicBtn.SetActive(true);
            WeaponShopBtn.SetActive(true);
            WeaponShop.SetActive(true);
        }

    }
    /// <summary>
    /// Скрыть инвентарь
    /// </summary>
    /// <returns>ни чего не  взвращаем</returns>
    private IEnumerator FadeOut()
    {
        if (!fadingOut)
        {
            fadingOut = true;
            fadingIn = false;
            StopCoroutine("FadeIn");
            float startAlpha = canvasGroup.alpha;
            float rate = 1.0f / fadeTime;
            float progress = 0.0f;
            while (progress < 1.0f)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);//функция изменяющая значение "startAlpha" до 0 за время "progress"
                progress += rate * Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 0;
            fadingOut = false;//процесс закрытия инвентаря окончен

        }
    }
    /// <summary>
    /// Показать инвентарь
    /// </summary>
    /// <returns>ни чего не  взвращаем</returns>
    private IEnumerator FadeIn()
    {
        if (!fadingIn)
        {
            fadingOut = false;
            fadingIn = true;
            StopCoroutine("FadeOut");
            float startAlpha = canvasGroup.alpha;
            float rate = 1.0f / fadeTime;
            float progress = 0.0f;
            while (progress < 1.0f)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, progress);//функция изменяющая значение "startAlpha" до 1 за время "progress"
                progress += rate * Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 1;
            fadingIn = false;//процесс открытия инвентаря окончен

        }
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
