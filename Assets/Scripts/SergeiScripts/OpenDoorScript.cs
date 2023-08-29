using UnityEngine;
using System.Collections;

public class OpenDoorScript : MonoBehaviour {
    private RayHitScript op;
	public bool test;
	public AudioClip Open;
    public AudioSource OpenPlit;
    public AudioClip Open2;
    public AudioSource OpenPlit2;
    public Animator animatorPlita2 ;

    public GameObject Kamen;
    public GameObject Kamen2;


    // Use this for initialization
    void Start () {

       // op.GetComponent<RayHitScript>();
    }
	
	// Update is called once per frame
	void Update () {


	}
    public void Button()
    {

        //    if (op.a == 1) 
        //    {
        //        test =!test;
        //    }

        //    if (op.a==1 & test == true)
        //    {

        //        OpenPlit.GetComponent<AudioSource>().PlayOneShot(Open);

        //        transform.GetComponent<Animation>().CrossFade ("OpenPlita");

        //        Kamen.GetComponent<Collider>().enabled = false;


        //    }






        //}
        //public void Button2()
        //{
        //    if (op.c == 1)
        //    {
        //        OpenPlit2.GetComponent<AudioSource>().PlayOneShot(Open2);
        //        animatorPlita2.SetBool("plit2Open", true);
        //        Kamen2.GetComponent<Collider>().enabled = false;
        //    }
        //}
    }
}

