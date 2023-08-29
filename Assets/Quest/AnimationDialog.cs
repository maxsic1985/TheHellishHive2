using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimationDialog : MonoBehaviour {
    public Text textArea; // текст 
    public string[] srtrings;// строки
    public float speed = 0.1f;//скорость чтения

    public int quest1 = 0;//задание1 для скрипта квест

    public int stringIndex = 0;//номер строки
    public int charIndex = 0;//один символ

    public GameObject QuestPanel;//окно диалога
	public GameObject butt1; 
	public GameObject butt2; 
	public GameObject butt3; 
	public GameObject butt4;
	public Animator ch4;
	public AudioClip qu2;//звук открытия
	public AudioSource QuestSource2;

    public PlayerHelper bonusGold;
    
	void Start ()
	{
		if (quest1==0) 
		{
			butt1.GetComponent<Button> ().interactable = false;//кнопка катакомбы отключить
			butt2.GetComponent<Button>().interactable=false;//инвентарь отключить
			butt3.GetComponent<Button>().interactable=false;//задания отключить
			butt4.GetComponent<Button>().interactable=false;//Характ кнопка отключить
			//ch4.SetBool ("ButtBool4",true);
		}
		if (quest1 == 1) 
		{
			butt1.GetComponent<Button> ().interactable = true;//кнопка катакомбы 
			butt2.GetComponent<Button>().interactable=true;//инвентарь 
			butt3.GetComponent<Button>().interactable=true;//задания 
			butt4.GetComponent<Button>().interactable=true;//Характ кнопка 
			//ch4.SetBool ("ButtBool4",false);
		}

	}
   public IEnumerator DisplayTimer()
    {
        if (quest1 == 0)
        {
            while (1 == 1)
            {
                yield return new WaitForSeconds(speed);
                if (charIndex > srtrings[stringIndex].Length)
                {
                    continue;
                }

                textArea.text = srtrings[stringIndex].Substring(0, charIndex);
                charIndex++;
                if (QuestPanel.activeSelf == false)
                {
                    yield break;//отключить энумеротор
                }
            }
        }
        if (quest1 == 1)
        {
            while (1 == 1)
            {
                yield return new WaitForSeconds(speed);
                if (charIndex > srtrings[stringIndex].Length)
                {
                    continue;
                }

                textArea.text = srtrings[stringIndex=4].Substring(0, charIndex); ;
                charIndex++;
                if (QuestPanel.activeSelf == false)
                {
                    yield break;//отключить энумеротор
                }
            }
         }
    
       }

    public void nextbutt()
    {

        
        if (charIndex < srtrings[stringIndex].Length)
        {
            charIndex = srtrings[stringIndex].Length;
            
        }
        else if (stringIndex < srtrings.Length)
        {
            stringIndex++;
            charIndex = 0;

        }
        if (stringIndex == 3)
        {
            bonusGold.goldCur= bonusGold.goldCur+100;
			quest1 = 1;
			QuestPanel.SetActive(false);
			stringIndex = 0;
			charIndex = 0;
			butt1.GetComponent<Button> ().interactable = true;//кнопка катакомбы 
			butt2.GetComponent<Button>().interactable=true;//инвентарь 
			butt3.GetComponent<Button>().interactable=true;//задания 
			butt4.GetComponent<Button>().interactable=true;//Характ кнопка 
        }


    }
    public void QuestPanelActiv()
    {
		QuestSource2.GetComponent<AudioSource> ().PlayOneShot (qu2);
        QuestPanel.SetActive(!QuestPanel.activeSelf);
        if (QuestPanel.activeSelf == true)
        {
            StartCoroutine(DisplayTimer());
        }
        

    }
    void Update ()
	{	

        if (stringIndex == 5)

        {

           
            QuestPanel.SetActive(false);
            stringIndex = 4;
            charIndex = 0;
        }
        if (QuestPanel.activeSelf==false && quest1 == 0)
        {

            stringIndex = 0;
            charIndex = 0;
           

        }
        if (QuestPanel.activeSelf == false && quest1 == 1)
        {

            stringIndex = 4;
            charIndex = 0;
        }



    }
}
