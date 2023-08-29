using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LoadManager : MonoBehaviour
{
    public static string levelName;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
