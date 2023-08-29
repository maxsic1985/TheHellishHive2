using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// магазин
/// </summary>
public class ButtonUpHelper : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Префаб объекта который добавится в инвентарь
    /// </summary>
    public GameObject UpPrefab;
    /// <summary>
    /// Текст прибавки к основной характеристики итема в магазине 
    /// </summary>
    public Text DamageText;
    /// <summary>
    /// Цена на итем
    /// </summary>
    public Text PriceText;
    /// <summary>
    /// Число прибавки к основной характеристики
    /// </summary>
    public int Damage = 10;
    /// <summary>
    /// Число цены
    /// </summary>
    public int Price = 500;
    /// <summary>
    /// иконка спрайт
    /// </summary>
    public Image IcoImage;
    //ссылка на PlayerHelper
    PlayerHelper _playerHelper;
    //Ссылка на инвентарь Inventory
    public Inventory inventory;
    #endregion
    #region Voids
    void Start()
    {
       _playerHelper = GameObject.FindObjectOfType<PlayerHelper>();
       DamageText.text = Damage.ToString();
       PriceText.text = Price.ToString();
    }
    void Update()
    {
        var button = GetComponent<Button>();
        var colors = button.colors;
        //если у игрока хватает денег
        if (_playerHelper.GoldCur >= Price)
        {
            //сменить цвет слота в магазине
            colors.normalColor = new Color(209, 231, 109, 255);
            button.colors = colors;
            button.enabled = true;
        }
        else if (_playerHelper.GoldCur < Price)
        {
            colors.normalColor = new Color(209, 231, 109, 100);
            button.colors = colors;
            button.enabled = false;

        }

    }
    /// <summary>
    /// Покупка в магазине, добавляет в инвентарь объект UpPrefab
    /// </summary>
    public void UpClick()
    {
        if (_playerHelper.GoldCur >= Price)
        {
            _playerHelper.GoldCur -= Price;
            inventory.GetComponent<Inventory>().AddItem(GameObject.Find(UpPrefab.name.ToString()).GetComponent<ItemScript>());
            Destroy(gameObject);
        }
    }
    #endregion
}
