using UnityEngine;
using System.Collections;

public class OpenTresureBox : MonoBehaviour {
    public RayHitScript use;//доступ к скрепту с луч
    public bool Proverka=false;//проверка открыт ,закрыт
    public AudioClip OpenBox;
    public AudioClip OpenBox2;
    public AudioClip OpenBox3;//звук открытия
    public AudioSource OpenBoxSource;//выбор проигрователя
    private float vremOpen;
    private float vremClose;

    // Use this for initialization
    void Start () {
  

    }
	
	// Update is called once per frame
	void Update () {


    }
    public void TresureBox()
    {
        //if (Proverka==false)
        //    {
        //if (use.TresureBox1 == 1 )
        //{
            
        //        GetComponent<Animation>().CrossFade("box_open");
        //        OpenBoxSource.GetComponent<AudioSource>().PlayOneShot(OpenBox);
        //    //    gameObject.tag = "Untagged";
        //        print("yet1");
        //        Proverka = true;
        //    }
           
          

        //}
        //else
        //{
        //    GetComponent<Animation>().CrossFade("box_close");
        //    Proverka = false;
        //}

    }
    public void TresureBox2()
    {

        //if (use.TresureBox2 == 1)
        //{
        //    GetComponent<Animation>().CrossFade("box_open2");
        //    OpenBoxSource.GetComponent<AudioSource>().PlayOneShot(OpenBox2);
        //  //  gameObject.tag = "Untagged";
        //    print("yet2");


        //}

    }
    public void TresureBox3()
    {

        //if (use.TresureBox3 == 1)
        //{
        //    GetComponent<Animation>().CrossFade("box_open3");
        //    OpenBoxSource.GetComponent<AudioSource>().PlayOneShot(OpenBox3);
        //   // gameObject.tag = "Untagged";
        //    print("yet3");


        //}

    }

}
