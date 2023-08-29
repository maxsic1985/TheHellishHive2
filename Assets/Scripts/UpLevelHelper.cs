using UnityEngine;
using System.Collections;

public class UpLevelHelper : MonoBehaviour
{
    private PlayerHelper playerHelper;
    // Use this for initialization
    void Start ()
    {
        playerHelper = GetComponent<PlayerHelper>();
      
    }
	
	// Update is called once per frame
	void Update () {
       
        switch (playerHelper.LvlPlayer)
        {

            case 2:
                if (playerHelper.GetLVL)
                {
                    playerHelper.baseSpeed += 2;
                    playerHelper.baseAtack += 1;
                    playerHelper.baseStamina += 0;
                    playerHelper.baseIntellect += 1;
                    playerHelper.ManaCur = playerHelper.ManaMax;
                    GetComponent<hp>().HP = playerHelper.HpMax;
                    playerHelper.GetLVL = false;
                    print("Speed"+playerHelper.baseSpeed +","+ "Atack"+playerHelper.baseAtack + "," + "Stamina"+playerHelper.baseStamina + "," +"Intellect"+playerHelper.Intelect);
                    SaveBaseAttribut(playerHelper.baseSpeed, playerHelper.baseAtack, playerHelper.baseStamina, playerHelper.baseIntellect);
                    CharactersPanel.Instance.CalcStats();
                }

                break;
            case 3:
                if (playerHelper.GetLVL)
                {
                    playerHelper.baseSpeed += 2;
                    playerHelper.baseAtack += 2;
                    playerHelper.baseStamina += 1;
                    playerHelper.ManaCur = playerHelper.ManaMax;
                    GetComponent<hp>().HP = playerHelper.HpMax;
                    playerHelper.GetLVL = false;
                    SaveBaseAttribut(playerHelper.baseSpeed, playerHelper.baseAtack, playerHelper.baseStamina, playerHelper.baseIntellect);
                    CharactersPanel.Instance.CalcStats();

                }
                break;
            case 4:
                if (playerHelper.GetLVL)
                {
                    playerHelper.baseSpeed += 1;
                    playerHelper.baseAtack += 1;
                    playerHelper.baseStamina += 1;
                    playerHelper.baseIntellect += 1;
                    playerHelper.ManaCur = playerHelper.ManaMax;
                    GetComponent<hp>().HP = playerHelper.HpMax;
                    playerHelper.GetLVL = false;
                    SaveBaseAttribut(playerHelper.baseSpeed, playerHelper.baseAtack, playerHelper.baseStamina, playerHelper.baseIntellect);
                    CharactersPanel.Instance.CalcStats();

                }
                break;
            case 5:
                if (playerHelper.GetLVL)
                {
                    playerHelper.baseSpeed += 1;
                    playerHelper.baseAtack += 1;
                    playerHelper.baseStamina += 0;
                    playerHelper.baseIntellect += 1;
                    playerHelper.ManaCur = playerHelper.ManaMax;
                    GetComponent<hp>().HP = playerHelper.HpMax;
                    playerHelper.GetLVL = false;
                    SaveBaseAttribut(playerHelper.baseSpeed, playerHelper.baseAtack, playerHelper.baseStamina, playerHelper.baseIntellect);
                    CharactersPanel.Instance.CalcStats();

                }
                break;
            case 6:
                if (playerHelper.GetLVL)
                {
                    playerHelper.baseSpeed += 1;
                    playerHelper.baseAtack += 1;
                    playerHelper.baseStamina += 1;
                    playerHelper.baseIntellect += 1;
                    playerHelper.ManaCur = playerHelper.ManaMax;
                    GetComponent<hp>().HP = playerHelper.HpMax;
                    playerHelper.GetLVL = false;
                    SaveBaseAttribut(playerHelper.baseSpeed, playerHelper.baseAtack, playerHelper.baseStamina, playerHelper.baseIntellect);

                    CharactersPanel.Instance.CalcStats();

                }
                break;
            case 7:
                if (playerHelper.GetLVL)
                {
                    playerHelper.baseSpeed += 1;
                    playerHelper.baseAtack += 1;
                    playerHelper.baseStamina += 1;
                    playerHelper.baseIntellect += 1;
                    playerHelper.ManaCur = playerHelper.ManaMax;
                    GetComponent<hp>().HP = playerHelper.HpMax;
                    playerHelper.GetLVL = false;
                    SaveBaseAttribut(playerHelper.baseSpeed, playerHelper.baseAtack, playerHelper.baseStamina, playerHelper.baseIntellect);

                    CharactersPanel.Instance.CalcStats();

                }
                break;
            case 8:
                if (playerHelper.GetLVL)
                {
                    playerHelper.baseSpeed += 1;
                    playerHelper.baseAtack += 1;
                    playerHelper.baseStamina += 1;
                    playerHelper.baseIntellect += 1;
                    playerHelper.ManaCur = playerHelper.ManaMax;
                    GetComponent<hp>().HP = playerHelper.HpMax;
                    playerHelper.GetLVL = false;
                    SaveBaseAttribut(playerHelper.baseSpeed, playerHelper.baseAtack, playerHelper.baseStamina, playerHelper.baseIntellect);

                    CharactersPanel.Instance.CalcStats();

                }
                break;
            case 9:
                if (playerHelper.GetLVL)
                {
                    playerHelper.baseSpeed += 1;
                    playerHelper.baseAtack += 1;
                    playerHelper.baseStamina += 1;
                    playerHelper.baseIntellect += 1;
                    playerHelper.ManaCur = playerHelper.ManaMax;
                    GetComponent<hp>().HP = playerHelper.HpMax;
                    playerHelper.GetLVL = false;
                    SaveBaseAttribut(playerHelper.baseSpeed, playerHelper.baseAtack, playerHelper.baseStamina, playerHelper.baseIntellect);

                    CharactersPanel.Instance.CalcStats();

                }
                break;
            case 10:
                if (playerHelper.GetLVL)
                {
                    playerHelper.baseSpeed += 1;
                    playerHelper.baseAtack += 1;
                    playerHelper.baseStamina += 1;
                    playerHelper.baseIntellect += 1;
                    playerHelper.ManaCur = playerHelper.ManaMax;
                    GetComponent<hp>().HP = playerHelper.HpMax;
                    playerHelper.GetLVL = false;
                    SaveBaseAttribut(playerHelper.baseSpeed, playerHelper.baseAtack, playerHelper.baseStamina, playerHelper.baseIntellect);

                    CharactersPanel.Instance.CalcStats();

                }
                break;
            default:
                break;
        }
    }

    private void SaveBaseAttribut(int speed,int atack, int stamina, int intellect )
    {
        if (Application.loadedLevel !=1)
        {
            PlayerPrefs.SetInt("BaseSpeedt", speed);
            PlayerPrefs.SetInt("BaseAtackt", atack);
            PlayerPrefs.SetInt("BaseStaminat", stamina);
            PlayerPrefs.SetInt("BaseIntellectt", intellect);
        }      
    }
}
