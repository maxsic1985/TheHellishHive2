using UnityEngine;
using System.Collections;

public class soundMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GlobalSFX.sound = 0.5f;

    }
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<AudioSource>().volume = GlobalSFX.sound;
    }
}
