using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

public class AnimationDialogPit : MonoBehaviour
{
    public Text textArea; // текст 
    private string[] currentString;
    public string[] srtringsRu; // строки
    public string[] srtringsEn; // строки
    public float speed = 0.1f; //скорость чтения
    public int stringIndex = 0; //номер строки
    public int charIndex = 0; //один символ
    public GameObject QuestPanel; //окно диалога
    public AudioClip qu2; //звук открытия
    public AudioSource QuestSource2;
    

    void Start()
    {
        switch (YG2.envir.language)
        {
            case "ru":
                currentString = srtringsRu;
                break;
            case "en":
                currentString = srtringsEn;
                break;
            default:
                currentString = srtringsRu;
                break;
        }
    }

    public IEnumerator DisplayTimer()
    {
 
            while (1 == 1)
            {
                yield return new WaitForSeconds(speed);
                if (charIndex > currentString[stringIndex].Length)
                {
                    continue;
                }

                textArea.text = currentString[stringIndex].Substring(0, charIndex);
                charIndex++;
                if (QuestPanel.activeSelf == false)
                {
                    yield break; //отключить энумеротор
                }
            }
            
    }

    public void nextbutt()
    {
        if (charIndex < currentString[stringIndex].Length)
        {
            charIndex = currentString[stringIndex].Length;
        }
        else if (stringIndex < currentString.Length)
        {
            stringIndex++;
            charIndex = 0;
        }

        if (stringIndex == 3)
        {
          
            QuestPanel.SetActive(false);
            stringIndex = 0;
            charIndex = 0;
        }
    }

    public void QuestPanelActiv()
    {
        if(SaveHalper.Instance.BossIsTrigger==1) return;
            
        QuestSource2.GetComponent<AudioSource>().PlayOneShot(qu2);
        QuestPanel.SetActive(!QuestPanel.activeSelf);
        if (QuestPanel.activeSelf == true)
        {
            StartCoroutine(DisplayTimer());
        }
    }
}