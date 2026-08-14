using UnityEngine;
using YG;

public class AdRewards : MonoBehaviour
{
    public string rewardID;
    public int coins;

    // Вызов рекламы за вознаграждение
    public void MyRewardAdvShow()
    {
        YG2.RewardedAdvShow(rewardID, () =>
        {
            // По желанию, воспользуйтесь ID вознаграждения
            if (rewardID == "coins")
                PlayerHelper.Instance.GoldCur += coins;
        });
    }
}
