using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetSFX : MonoBehaviour {

    public GameObject buttSetting;
	public GameObject RZ;
    public AudioSource butSet;
    public AudioClip buttSet;


    // Use this for initialization
    void Start () {
  

    }
	
	// Update is called once per frame
	void Update () {
        
     

    }
    public void setMusic(float volue)
    {
        GlobalSFX.music = volue;
    }
    public void setSound(float volue)
    {
        GlobalSFX.sound = volue;
    }
    public void setAmbient(float volue)
    {
        GlobalSFX.ambient = volue;
    }
    public void buttonSET()
    {
        buttSetting.SetActive(!buttSetting.activeSelf); //скрытие меню настроек
        butSet.GetComponent<AudioSource>().PlayOneShot(buttSet);

    }
	public void RZcancel()//отключает панель авторы
	{

		RZ.SetActive (false);

	}
}//ГЛОБАЛЬНЫЕ НАСТРОЙКИ ЗВУКА!!!!!!!!!!

