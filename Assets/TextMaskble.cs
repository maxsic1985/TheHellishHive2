using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextMaskble : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Button _button;
    private Text text;
    private float alpha;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _button = GetComponent<Button>();
         alpha = 0;
         text = gameObject.GetComponentInChildren<Text>(); 
         text.color = new Color(text.color.r,text.color.g,text.color.b,alpha);
    }

    // Update is called once per frame
   

    public void OnSelect(BaseEventData eventData)
    {
        alpha = 255;
        text.color = new Color(text.color.r,text.color.g,text.color.b,alpha);
        Debug.LogWarning("Select");
      
    }

    public void OnDeselect(BaseEventData eventData)
    {
        alpha = 0;
        text.color = new Color(text.color.r,text.color.g,text.color.b,alpha);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.LogWarning("Point");
        alpha = 255;
        text.color = new Color(text.color.r,text.color.g,text.color.b,alpha);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        alpha = 0;
        text.color = new Color(text.color.r,text.color.g,text.color.b,alpha);
    }
}