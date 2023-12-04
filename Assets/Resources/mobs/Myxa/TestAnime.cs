using UnityEngine;
using System.Collections;

public class TestAnime : MonoBehaviour {
    private float Damag;
    private float Pain;
    private float Death;
    private float Priv;
    private int mod;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        if (mod == 0 & mod!=4)
        {
            GetComponent<Animation>().Play("IdelKRAB", PlayMode.StopAll);//анимация бездействия.
            Damag = 0;
        }

        if (Input.GetKeyUp(KeyCode.G)& mod==0)
            {
            GetComponent<Animation>().Play("DamagKRAB", PlayMode.StopAll);
            mod = 1;
            }
        if (mod == 1)
        {
            Damag += Time.deltaTime;
        }
        if (Damag >= GetComponent<Animation>()["DamagKRAB"].length)
        {
         
            mod = 0;
            Damag = 0;
        }
        if (Input.GetKeyUp(KeyCode.F) & mod == 0)
        {
            GetComponent<Animation>().Play("PainKRAB", PlayMode.StopAll);
            mod = 2;
        }
        if (mod == 2)
        {
            Pain += Time.deltaTime;
        }
        if (Pain >= GetComponent<Animation>()["PainKRAB"].length)
        {

            mod = 0;
            Pain = 0;
        }

        if (Input.GetKeyUp(KeyCode.R) & mod == 0)
        {
            GetComponent<Animation>().Play("DeathKRAB", PlayMode.StopAll);
            mod = 3;
        }
        if (mod == 3)
        {
            Death += Time.deltaTime;
        }
        if (Death >= GetComponent<Animation>()["DeathKRAB"].length)
        {

            mod = 0;
            Death = 0;
        }
        if (Input.GetKeyUp(KeyCode.T) & mod == 0)
        {
            GetComponent<Animation>().Play("KRABPriv", PlayMode.StopAll);
            mod = 4;
            
        }
        if (mod == 4)
        {
            Priv += Time.deltaTime;
        }
        if (Priv >= GetComponent<Animation>()["KRABPriv"].length)
        {

            mod = 0;
            Priv = 0;
        }


    }
}
