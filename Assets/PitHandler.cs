using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PitHandler : MonoBehaviour
{
    public void ToBoss()
    {
        if (SaveHalper.Instance.BossIsTrigger == 1)
        {
            LoadManager.UsePit = true;
            SceneManager.LoadScene("Lvl1");
        }
    }
}