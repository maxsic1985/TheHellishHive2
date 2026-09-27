using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PitHandler : MonoBehaviour
{

    private static bool isPit;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (SceneManager.loadedSceneCount == 2)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
            if (player != null)
            {
                //  var cc = player.GetComponent<CharacterController>();
                // if (cc != null) cc.enabled = false;
                isPit = true;
                player.transform.position =  new Vector3(-43.25f, -0.375f, 90.84f);
        
                //   if (cc != null) cc.enabled = true;
            }
        }
    }

    public IEnumerator LoadSceneAndMovePlayer(string sceneName, Vector3 spawnPos)
    {
        // Запускаем асинхронную загрузку
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Ждем, пока сцена полностью загрузится и активируется
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Теперь сцена активна, можно двигать игрока
        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
        if (player != null)
        {
          //  var cc = player.GetComponent<CharacterController>();
           // if (cc != null) cc.enabled = false;
           isPit = true;
            player.transform.position = spawnPos;
        
         //   if (cc != null) cc.enabled = true;
        }
    }

    public void ToBoss()
    {
        if (SaveHalper.Instance.BossIsTrigger == 1)
        {
         //   StartCoroutine(LoadSceneAndMovePlayer("Lvl1", new Vector3(-43.25f, -0.375f, 90.84f)));
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Lvl1");
            //  SceneManager.LoadSceneAsync(2);
            // PlayerHelper.Instance.GameObject().transform.position = new Vector3(-43.25f, -0.375f, 90.84f);
        }
    }
}