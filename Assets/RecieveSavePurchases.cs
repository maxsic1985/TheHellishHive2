using System;
using UnityEngine;
using YG;

public class RecieveSavePurchases : MonoBehaviour
{
    private void Start()
    {
        Debug.LogWarning(YG2.GetState("coins"));
    }

    private void OnEnable()
    {
        YG2.onPurchaseSuccess += SuccessPurchased;
        YG2.onPurchaseFailed += FailedPurchased;
        
    }

    private void OnDisable()
    {
        YG2.onPurchaseSuccess -= SuccessPurchased;
        YG2.onPurchaseFailed -= FailedPurchased;
    }

    private void SuccessPurchased(string id)
    {
        // Ваш код для обработки покупки, например:
        string coinsKey = "coins";
        
        int coins = YG2.GetState(coinsKey);

        if (id == "Coinsx5")
            YG2.SetState(coinsKey, coins + 5);
        else if (id == "Coinsx10")
            YG2.SetState(coinsKey, coins + 10);
        else if (id == "Coinsx100")
            YG2.SetState(coinsKey, coins + 100);
        else if (id == "Coinsx500")
            YG2.SetState(coinsKey, coins + 500);
        
        Debug.LogWarning(YG2.GetState("coins"));
    }

    private void FailedPurchased(string id)
    {
        // Покупка не была совершена
    }

}
