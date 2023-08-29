using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArchievmentBtn : MonoBehaviour
{
    public GameObject achievmentList;

    public Sprite neutral, highlith;

    private Image sprite;
    // Use this for initialization
    void Awake()
    {
        sprite = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Click()
    {
        
        if (sprite.sprite==neutral)
        {

          
            sprite.sprite = highlith;
            
            achievmentList.SetActive(true);

        }

        else
        {
          sprite.sprite = neutral;

            achievmentList.SetActive(false);
        }
    }
}
