using UnityEngine;
using System.Collections;
using Services;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using YG;

public class LV1Dialog : MonoBehaviour {
	
	public Animator PEMEHb;
	public int stringIndex2 = 0;//номер строки
	public int charIndex2 = 0;//один символ

	public float speed2 = 0.1f;//скорость чтения

	public Text textArea2; // текст 
	public string[] srtrings2;// строки
	public string[] srtrings2En;// строки
	[FormerlySerializedAs("currentString2")] public string[] _NpcInputText;// строки

	public GameObject QuestPanel2;//окно диалога

	public GameObject b1;//интерфейс игрока отключить
	public GameObject b2;//инвентарь отключить
	public GameObject b3;//настройки отключить
	public GameObject b4;//настройки отключить

	public bool ontrigger=false;

	public int triggSave = 0;
	private FirstPersonController _fps;


	private void Awake()
	{
		_fps = GetComponent<FirstPersonController>();
	}

	IEnumerator TimerText()//метод печати текста по буквам
	{
		
			while (1 == 1)
			{
				yield return new WaitForSeconds(speed2);
				if (charIndex2 > _NpcInputText[stringIndex2].Length)
				{
					continue;
				}

				textArea2.text = _NpcInputText[stringIndex2].Substring(0, charIndex2);
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
			QuestPanel2.SetActive (true);//отобразить панель НПС
			
			SetMenuInteractble(false);
			ControlsService.LockControls(_fps);
			StartCoroutine(TimerText());

		}
	
    }

	private void SetMenuInteractble(bool interactble)
	{
		b1.GetComponent<Button>().interactable = interactble; //интерфейс игрока отключить
		b2.GetComponent<Button>().interactable = interactble; //инвентарь отключить
		b3.GetComponent<Button>().interactable = interactble; //настройки отключить
		b4.GetComponent<Button>().interactable = interactble; //настройки отключить
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
			GetComponent<randomMob> ().cnt=0;
			GetComponent<randomMob> ().enter=false;
			SetMenuInteractble(true);
			ControlsService.UnLockControls(_fps);
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
	    
	    switch (YG2.envir.language)
	    {
		    case "ru":
			    _NpcInputText = srtrings2;
			    break;
		    case "en":
			    _NpcInputText = srtrings2En;
			    break;
		    default:
			    _NpcInputText = srtrings2;
			    break;
	    } 
	    
		if (triggSave == 1) 
		{
			Destroy (GameObject.Find ("TriggerDialog"));

		}
	
	}

}
