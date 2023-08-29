using UnityEngine;
using System.Collections;

public class OpenResh : MonoBehaviour {
    public RayHitScript use2;
    public AudioClip Openresh;//звук открытия
    public AudioSource OpenReshSource;
    public GameObject zam;
    public bool kay=true;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OpenZamok()
    {

        //if (use2.zamok == 1 & kay==true)
        //{
        //    GetComponent<Animation>().CrossFade("open_resh");
        //    OpenReshSource.GetComponent<AudioSource>().PlayOneShot(Openresh);
        // //   zam.tag = "Untagged";



        //}

    }
}
