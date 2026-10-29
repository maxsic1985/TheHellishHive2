using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class RecieveSavePurchases : MonoBehaviour
{
    public Text CoinsCnt;
    [SerializeField] private string coinsKey = "coins";
    private void Start()
    {
        CoinsCnt.text = YG2.GetState(coinsKey).ToString();
       
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        YG2.onPurchaseSuccess += SuccessPurchased;
        YG2.onPurchaseFailed += FailedPurchased;
        CoinsCnt.text = YG2.GetState(coinsKey).ToString();
        
    }

    private void OnDisable()
    {
        YG2.onPurchaseSuccess -= SuccessPurchased;
        YG2.onPurchaseFailed -= FailedPurchased;
    }

    private void SuccessPurchased(string id)
    {
        int coins = YG2.GetState(coinsKey);

        if (id == "Coinsx5")
            YG2.SetState(coinsKey, coins + 5);
        else if (id == "Coinsx10")
            YG2.SetState(coinsKey, coins + 10);
        else if (id == "Coinsx100")
            YG2.SetState(coinsKey, coins + 100);
        else if (id == "Coinsx500")
            YG2.SetState(coinsKey, coins + 500);
        
        CoinsCnt.text = YG2.GetState(coinsKey).ToString();
    }

    private void FailedPurchased(string id)
    {
   
    }

    private void Update()
    {
      //  CoinsCnt.text = YG2.GetState(coinsKey).ToString();
    }
}
