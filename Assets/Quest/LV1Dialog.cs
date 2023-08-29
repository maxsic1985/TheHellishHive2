using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class LV1Dialog : MonoBehaviour {
	
	public Animator PEMEHb;
	public int stringIndex2 = 0;//номер строки
	public int charIndex2 = 0;//один символ

	public float speed2 = 0.1f;//скорость чтения

	public Text textArea2; // текст 
	public string[] srtrings2;// строки

	public GameObject QuestPanel2;//окно диалога

	public GameObject b1;//интерфейс игрока отключить
	public GameObject b2;//инвентарь отключить
	public GameObject b3;//настройки отключить
	public GameObject b4;//настройки отключить

	public bool ontrigger=false;

	public int triggSave = 0;

   IEnumerator TimerText()//метод печати текста по буквам
	{
		
			while (1 == 1)
			{
				yield return new WaitForSeconds(speed2);
				if (charIndex2 > srtrings2[stringIndex2].Length)
				{
					continue;
				}

				textArea2.text = srtrings2[stringIndex2].Substring(0, charIndex2);
				charIndex2++;
				if (QuestPanel2.activeSelf == false)
				{
					yield break;//отключить энумеротор
				}
			}
		}
	void OnTriggerEnter (Collider col2)//если персонаж в триггере диалог
	{
		if (col2.CompareTag ("Dialog") &&  triggSave==0) 
		{
			StartCoroutine(TimerText());
			QuestPanel2.SetActive (true);//отобразить панель НПС
		    GetComponent<FirstPersonController> ().m_RunSpeed = 0;//скорость хотьбы 0
			b1.GetComponent<Button> ().interactable = false;//интерфейс игрока отключить
			b2.GetComponent<Button>().interactable=false;//инвентарь отключить
			b3.GetComponent<Button>().interactable=false;//настройки отключить
			b4.GetComponent<Button>().interactable=false;//настройки отключить
			GetComponent<FirstPersonController> ().m_MouseLook.XSensitivity=0;
			GetComponent<FirstPersonController> ().m_MouseLook.YSensitivity = 0;

		}
	
    }
	public void nextbutt2()
	{


		if (charIndex2 < srtrings2[stringIndex2].Length)
		{
			charIndex2 = srtrings2[stringIndex2].Length;

		}
		else if (stringIndex2 < srtrings2.Length)
		{
			stringIndex2++;
			charIndex2 = 0;

		}
		if (stringIndex2 == 3)

		{


			QuestPanel2.SetActive(false);
			stringIndex2 = 0;
			charIndex2 = 0;
			Destroy (GameObject.Find ("TriggerDialog"));
			triggSave = 1;
			GetComponent<FirstPersonController> ().m_RunSpeed = 1.8f;//скорость хотьбы  норма
			GetComponent<randomMob> ().cnt=0;
			GetComponent<randomMob> ().enter=false;
			b1.GetComponent<Button> ().interactable = true;//интерфейс игрока 
			b2.GetComponent<Button> ().interactable = true;
			b3.GetComponent<Button>().interactable=true;//настройки
			b4.GetComponent<Button> ().interactable = true;
			GetComponent<FirstPersonController> ().m_MouseLook.XSensitivity=2.5f;
			GetComponent<FirstPersonController> ().m_MouseLook.YSensitivity = 2.5f;


		}
		if (stringIndex2 == 1)

		{
			PEMEHb.SetBool ("PEMBool",true) ;
		}
        else PEMEHb.SetBool ("PEMBool",false) ;

	}
		


	// Use this for initialization
	void Start ()
    {
		if (triggSave == 1) 
		{
			Destroy (GameObject.Find ("TriggerDialog"));

		}
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	
	}
}
