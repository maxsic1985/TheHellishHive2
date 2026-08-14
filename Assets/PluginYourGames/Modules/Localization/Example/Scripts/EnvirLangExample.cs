using UnityEngine;
using UnityEngine.UI;

namespace YG.Example
{
    public class EnvirLangExample : MonoBehaviour
    {
        public string ru, en;

        private Text textComponent;

        private void Start()
        {
            textComponent = GetComponent<Text>();

#if EnvirData_yg
            switch (YG2.envir.language)
            {
                case "ru":
                    textComponent.text = ru;
                    break;
                case "en":
                    textComponent.text = en;
                    break;
                default:
                    textComponent.text = ru;
                    break;
            }
#else
            textComponent.text = "Envir Data not import";
#endif
        }
    }
}