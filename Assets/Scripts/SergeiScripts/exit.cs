using UnityEngine;
using System.Collections;

public class exit : MonoBehaviour {
    public GameObject PanelExit;
    public AudioClip Exitmenu;//звук открытия
    public AudioSource ExitSource;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ExitLVL0()
    {
        ExitSource.GetComponent<AudioSource>().PlayOneShot(Exitmenu);
        PanelExit.SetActive(!PanelExit.activeSelf);

    }
    public void ExitNo()
    {
        ExitSource.GetComponent<AudioSource>().PlayOneShot(Exitmenu);
        PanelExit.SetActive(false);

    }
    public void ExitYas()
    {
        ExitSource.GetComponent<AudioSource>().PlayOneShot(Exitmenu);
		LoadManager.levelName = "MainMenu";
		UnityEngine.SceneManagement.SceneManager.LoadScene ("LoadScene");

    }
   
}
