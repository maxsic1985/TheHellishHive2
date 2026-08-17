using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using YG;

public class quest : MonoBehaviour {
    public GameObject questBt;
    public AudioClip qu;//звук открытия
    public AudioSource QuestSource;

    public string[] _currsrtringsQuest;// строки
    public string[] srtringsQuest;// строки
    public string[] srtringsQuestEn;// строки
    public Text textQuest; // текст 
    public int stringIndex2 = 0;

    public AnimationDialog npc;

    public void Quest()
    {
        switch (YG2.envir.language)
        {
            case "ru":
                _currsrtringsQuest = srtringsQuest;
                break;
            case "en":
                _currsrtringsQuest = srtringsQuestEn;
                break;
            default:
                _currsrtringsQuest = srtringsQuest;
                break;
        }
        
        
        QuestSource.GetComponent<AudioSource>().PlayOneShot(qu);
        questBt.SetActive(!questBt.activeSelf);
        if (npc.quest1 == 1)
        {
            textQuest.text = _currsrtringsQuest[stringIndex2 = 0];//1 задание
        }

    }
}
