using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public static string levelName;
    public static  bool UsePit;

    private void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Загружена новая сцена: " + scene.name);
      
   
            if (UsePit)
            {
                PlayerHelper.Instance.transform.position = new Vector3(-43.25f, -0.375f, 90.84f);
                UsePit = false;
            }
        
        
        
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
   
}