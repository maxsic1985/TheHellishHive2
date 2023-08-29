using UnityEngine;
using System.Collections;

public class testMusic : MonoBehaviour {
    public AudioSource musicSourch2;
    public AudioClip musictest2;
    public float vrem2;
    public int reloud2;

    // Use this for initialization
    void Start () {
        reloud2 = 1;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        vrem2 += Time.deltaTime;
        if (vrem2 >= 3f)
        {
            reloud2 = 1;
        }
        if (reloud2 == 1)
        {
            vrem2 = 0;
        }
    }
    public void musicTes2()
    {
        if (reloud2 == 1)
        {
            musicSourch2.GetComponent<AudioSource>().PlayOneShot(musictest2);
            reloud2 = 0;
        }
    }
}
