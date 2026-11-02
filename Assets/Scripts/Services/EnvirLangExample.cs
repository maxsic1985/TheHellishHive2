using UnityEngine.EventSystems;
using YG;

namespace Services
{
    using UnityEngine;
    using UnityEngine.UI;

    namespace YG.Example
    {
        public class EnvirLangExample : MonoBehaviour
        {
            public string ru, en;

            private Text textComponent;
            public Image imageEn;
            public Image imageRu;

            private void Start()
            {
                textComponent = GetComponent<Text>();


                switch (YG2.envir.language)
                {
                    case "ru":
                        textComponent.text = ru;
                        if (imageEn && imageRu != null)
                        {
                            imageRu.enabled = true;
                            imageEn.enabled = false;
                        }

                        break;
                    case "en":
                        textComponent.text = en;
                        if (imageEn && imageRu != null)
                        {
                            imageRu.enabled = false;
                            imageEn.enabled = true;
                        }
                        break;
                    default:
                        textComponent.text = ru;
                        break;
                }
                
            }

            // public void SetColorText()
            // {
            //     var color=new Color(212,175,55,255);
            //     textComponent.color = color;
            // }
            //
            // public void OnPointerEnter(PointerEventData eventData)
            // {
            //    SetColorText();
            // }
            //
            // public void OnPointerExit(PointerEventData eventData)
            // {
            //     Debug.Log($"Курсор ушёл с {gameObject.name}");
            // }
            
        }
    }
}