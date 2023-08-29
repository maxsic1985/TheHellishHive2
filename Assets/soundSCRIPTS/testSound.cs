using UnityEngine;
using System.Collections;

public class testSound : MonoBehaviour {
    public AudioSource soundSourch;
    public AudioClip soundtest;
    public float vrem;
    public int reloud;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        vrem += Time.deltaTime;
        if (vrem >= 0.5f)
        {
            reloud = 1;
        }
        if (reloud == 1)
        {
            vrem = 0;
        }
	
	}
    public void soundTes()
    {
        if (reloud == 1)
        {
            soundSourch.GetComponent<AudioSource>().PlayOneShot(soundtest);
            reloud = 0;
        }
    }
}
