using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PitHandler : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (SceneManager.loadedSceneCount == 2)
        {
            Debug.Log("Here");
            GameObject player = PlayerHelper.Instance.GameObject();
            if (player != null)
            {
                //  var cc = player.GetComponent<CharacterController>();
                // if (cc != null) cc.enabled = false;

                Debug.Log("Here");
                player.transform.position = new Vector3(-43.25f, -0.375f, 90.84f);

                //   if (cc != null) cc.enabled = true;
            }
        }
    }

    public void ToBoss()
    {
        if (SaveHalper.Instance.BossIsTrigger == 1)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Lvl1");
        }
    }
}