using UnityEngine;
using System.Collections;

public class butprod : MonoBehaviour {
	public Animator animator;
    public SaveHalper pr;


	public GameObject but1;
	public GameObject but2;
	public GameObject but3;
	public GameObject but4;
	public float vrem;

    // Use this for initialization
    void Start () {
     

	}
	
	// Update is called once per frame
	void Update () {
        if (pr.Savining == 0)
        {

            animator.SetBool("prod", true);
        }
        else animator.SetBool("prod", false);


		//////////////////////////////////////////////////////////




    }
}
