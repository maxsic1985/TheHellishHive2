using Unity.VisualScripting;
using UnityEngine;

public class CheetScript : MonoBehaviour
{
    public string typeMob;

    public void ResetStats()
    {
        PlayerHelper.Instance.baseSpeed = 5;
        PlayerHelper.Instance.baseStamina = 5;
        PlayerHelper.Instance.baseAtack = 15;
        PlayerHelper.Instance.baseIntellect = 5;

        PlayerHelper.Instance.Speed = 0;
        PlayerHelper.Instance.Stamina = 0;
        PlayerHelper.Instance.Atack = 0;
        PlayerHelper.Instance.Intelect = 0;

        PlayerHelper.Instance.LvlPlayer = 1;
        PlayerHelper.Instance.ManaCur = 0;
        PlayerHelper.Instance.HpCur = 25;
        PlayerHelper.Instance.Exp = 0;
    }

    public void UpStats(int up)
    {
        var speed = PlayerHelper.Instance.baseSpeed += up;
        var stamina = PlayerHelper.Instance.baseStamina += up;
        var atack = PlayerHelper.Instance.baseAtack += up;
        var intellect = PlayerHelper.Instance.baseIntellect += up;
        PlayerHelper.Instance.SetStats(atack, speed, stamina, intellect);
    }

    public void UpHpMP()
    {
        PlayerHelper.Instance.ManaCur += 100;
        PlayerHelper.Instance.HpCur += 100;
    }

    public void UpExp(int exp)
    {
        PlayerHelper.Instance.Exp += exp;
    }

    public void MobSpawn(int cnt)
    {
        FindAnyObjectByType<randomMob>().AddMobsOnScene(typeMob, cnt);
    }

    public void Bossss()
    {
        PlayerHelper.Instance.GameObject().transform.position = GameObject.FindGameObjectWithTag("respToBoss")
            .GetComponent<Transform>().localPosition;
        FindAnyObjectByType<randomMob>().ToBoss();
    }
}