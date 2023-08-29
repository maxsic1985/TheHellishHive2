using UnityEngine;
using System.Collections;

public class ambientMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GlobalSFX.ambient = 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<AudioSource>().volume = GlobalSFX.ambient;
    }
}
