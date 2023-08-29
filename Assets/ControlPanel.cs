using UnityEngine;
using System.Collections;

public class ControlPanel : MonoBehaviour {
	public AudioClip Exitmenu2;//звук открытия
	public AudioSource ExitSource2;
	public GameObject GM;
	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}

    public void ShowPanel()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
			ExitSource2.GetComponent<AudioSource>().PlayOneShot(Exitmenu2);
          
        }
        else
        {
			GM.SetActive (false);
            gameObject.SetActive(false);
			ExitSource2.GetComponent<AudioSource>().PlayOneShot(Exitmenu2);
        }
    }
}
