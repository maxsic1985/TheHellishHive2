using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class IndexHalper : MonoBehaviour
{
    [SerializeField]
    private int index;

    public Text iname;
    public Text description;


    public Text price;
    private Sprite sprite;

    public bool blocked = true;

    public Sprite Sprite
    {
        get { return sprite; }
        set { sprite = value; }
    }
    public Image image2;
    public int Index
    {
        get { return index; }
        set { index = value; }
    }

    public bool Blocked
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
}
