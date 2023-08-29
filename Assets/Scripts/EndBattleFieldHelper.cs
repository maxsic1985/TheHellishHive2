using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
/// <summary>
/// Определяет вознаграждение за победу в битве
/// </summary>
public class EndBattleFieldHelper : MonoBehaviour

{
    public randomMob rr;
    #region Variables
    /// <summary>
    /// ссылка на саму панель
    /// </summary>
    public GameObject EndBattlePanel;
    /// <summary>
    /// Текст, поученные деньги
    /// </summary>
    public Text GiveGoldText;
    /// <summary>
    /// Текст, поученный опыт
    /// </summary>
    public Text GiveEXPText;
    /// <summary>
    /// Текст, поученные вещи
    /// </summary>
    public Text GivePriceText;
    /// <summary>
    /// Поученная голда
    /// </summary>
    private int giveGold;
    /// <summary>
    /// Полученный опыт
    /// </summary>
    private int giveExp;
    /// <summary>
    /// Имя полученной вещи
    /// </summary>
    private string GivePriceName;
    /// <summary>
    /// Путь к расположению папки с префабами
    /// </summary>
    public string prefab;
    /// <summary>
    /// Ссылка на инвентарь
    /// </summary>
    public Inventory inventory;
    #endregion
    #region Voids
    // Update is called once per frame
    public AudioSource vicSorce;// для звука победы.
    public AudioClip muzonVic;// для звука победы.
    private int mu2 = 0;// для звука победы.

    void Update()
    {
        if (EndBattlePanel.activeSelf)
        {
            rr.MobileControlCanvas.GetComponentInChildren<Canvas>().enabled = false;
            GetComponent<FirstPersonController>().m_MouseLook.XSensitivity = 0.0f;
            GetComponent<FirstPersonController>().m_MouseLook.YSensitivity = 0.0f;
            GetComponent<FirstPersonController>().m_RunSpeed = 0.0f;
        }
        if (GameObject.FindGameObjectWithTag("Mob"))
        {
            GivePriceName = GameObject.FindGameObjectWithTag("Mob").GetComponent<Mob>().PriceForMob.name.ToString();
        }

        if (GetComponent<randomMob>().EnbBattle)
        {



            giveExp = GetComponent<SpeedHelper>().TmpExpForBattle;

            giveGold = GetComponent<SpeedHelper>().TmpGoldForBattle;

            EndBattlePanel.SetActive(true);

            GiveGoldText.text = giveGold.ToString();
            GiveEXPText.text = giveExp.ToString();
            GivePriceText.text = GivePriceName.ToString();
            if (GetComponent<randomMob>().EnbBattle & mu2 == 0) // для звука победы.
            {
                vicSorce.GetComponent<AudioSource>().PlayOneShot(muzonVic);
                mu2 = 1;
            }
        }
    }
    /// <summary>
    /// Закрытие панели и приминение получееных за битву наград
    /// </summary>
    public void ClosePanel()
    {
        mu2 = 0; // для звука победы.
        GetComponent<PlayerHelper>().Exp += giveExp;
        PlayerPrefs.SetInt(gameObject.name + "CurExpt", GetComponent<PlayerHelper>().Exp);
        GetComponent<PlayerHelper>().GoldCur += giveGold;
        GetComponent<randomMob>().EnbBattle = false;
        GetComponent<SpeedHelper>().TmpGoldForBattle = 0;
        giveExp = GetComponent<SpeedHelper>().TmpExpForBattle = 0;
        //награждение итем в инвентарь за победу над монстром
        GameObject tm = Resources.Load<GameObject>(prefab + GivePriceName.ToString());
        if (tm)
        {

            inventory.GetComponent<Inventory>().AddItem(tm.GetComponent<ItemScript>());
        }


        EndBattlePanel.SetActive(false);
        rr.MobileControlCanvas.GetComponentInChildren<Canvas>().enabled = true;
        GetComponent<FirstPersonController>().enabled = true;
        GetComponent<FirstPersonController>().m_MouseLook.XSensitivity = 2.5f;
        GetComponent<FirstPersonController>().m_MouseLook.YSensitivity = 2.5f;
        GetComponent<FirstPersonController>().m_RunSpeed = 1.7f;

    }

    #endregion
}
