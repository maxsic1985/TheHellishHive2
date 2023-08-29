using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class quest : MonoBehaviour {
    public GameObject questBt;
    public AudioClip qu;//звук открытия
    public AudioSource QuestSource;

    public string[] srtringsQuest;// строки
    public Text textQuest; // текст 
    public int stringIndex2 = 0;

    public AnimationDialog npc;

    public void Quest()
    {
        QuestSource.GetComponent<AudioSource>().PlayOneShot(qu);
        questBt.SetActive(!questBt.activeSelf);
        if (npc.quest1 == 1)
        {
            textQuest.text = srtringsQuest[stringIndex2 = 0];//1 задание
        }

    }
}
