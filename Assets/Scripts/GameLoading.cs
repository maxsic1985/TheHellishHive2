using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameLoading : MonoBehaviour
{
    public GameObject loadingIcon;
    private AsyncOperation async;
    public string nameScene;
    IEnumerator Start()
    {
        loadingIcon.SetActive(true);
        async = SceneManager.LoadSceneAsync(LoadManager.levelName,LoadSceneMode.Single);
        while (!async.isDone)
        {     
            yield return true;
        }
    }
}