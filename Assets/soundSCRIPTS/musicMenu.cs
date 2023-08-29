using UnityEngine;
using System.Collections;

public class musicMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GlobalSFX.music = 0.5f;


    }
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<AudioSource>().volume = GlobalSFX.music;
    }
}
