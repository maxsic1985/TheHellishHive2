using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using YG;

public  class quest : MonoBehaviour {
    public GameObject questBt;
    public AudioClip qu;//звук открытия
    public AudioSource QuestSource;

    public Text Task1;
    public Text Task2;
    public Text Task3;

    //public int Task1Completed;
    //public int Task2Completed;
    //public int Task3Completed;

    private void Start()
    {
        Task1.color=Color.grey;
        Task2.color=Color.grey;
        Task3.color=Color.grey;
    }

    public void LateUpdate()
    {
        Task1.color =  (SaveHalper.Instance.Task1Completed == 1) ? Task1.color = Color.white : Task1.color = Color.grey;
        Task2.color =  (SaveHalper.Instance.Task2Completed == 1) ? Task2.color = Color.white : Task2.color = Color.grey;
        Task3.color =  (SaveHalper.Instance.Task3Completed == 1) ? Task3.color = Color.white : Task3.color = Color.grey;
    }
    

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
