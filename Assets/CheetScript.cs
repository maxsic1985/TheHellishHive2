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
        FindAnyObjectByType<_randomMob>().AddMobsOnScene(typeMob, cnt);
    }

    public void Bossss()
    {
        // PlayerHelper.Instance.GameObject().transform.position = GameObject.FindGameObjectWithTag("respToBoss")
        //     .GetComponent<Transform>().localPosition;
        // FindAnyObjectByType<randomMob>().ToBoss();
        PlayerHelper.Instance.GameObject().transform.position = new Vector3(-43.25f, -0.375f, 90.84f);
            // UnityEditor.TransformWorldPlacementJSON:{"position":{"x":-43.25803756713867,"y":-0.37553271651268008,"z":90.84435272216797},"rotation":{"x":0.0,"y":0.6592525839805603,"z":0.0,"w":-0.751921534538269},"scale":{"x":1.0,"y":1.0,"z":1.0}}
        
        
        
        
    }
}