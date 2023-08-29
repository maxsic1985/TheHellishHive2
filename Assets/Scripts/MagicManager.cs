using UnityEngine;
using System.Collections;

public class MagicManager : MonoBehaviour
{
    GameObject[] tmp;

    private string [] nameItem;

    private int [] index;

    private bool [] blocked;

    public string[] NameItem
    {
        get
        {
            return nameItem;
        }

        set
        {
            nameItem = value;
        }
    }

    public int[] Index
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
        }
    }

    public bool[] Blocked
    {
        get
        {
            return blocked;
        }

        set
        {
            blocked = value;
        }
    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tmp = GameObject.FindGameObjectsWithTag("MagShop");
        for (int i = 0; i < tmp.Length; i++)
        {
        ////    NameItem[i] = tmp[i].GetComponent<IndexHalper>().name;
        //    Index[i] = tmp[i].GetComponent<IndexHalper>().Index;
        //    Blocked[i] = tmp[i].GetComponent<IndexHalper>().Blocked;

        }
    }
}
