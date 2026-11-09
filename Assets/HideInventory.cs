using UnityEngine;

namespace Services
{
    public class HideInventory : MonoBehaviour
    {
        private bool firstCicle = false;

        void Update()
        {
            if (firstCicle == false)
            {
                gameObject.SetActive(false);
                firstCicle = true;
            }
        }
    }
}