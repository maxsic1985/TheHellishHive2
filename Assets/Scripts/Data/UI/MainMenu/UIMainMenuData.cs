using UnityEngine;
using UnityEngine.AddressableAssets;


namespace HalfDiggers.Runner
{
    [CreateAssetMenu(fileName = nameof(UIMainMenuData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(UIMainMenuData))]
    public class UIMainMenuData : ScriptableObject
    {
        [Header("Menu")] 
        public AssetReferenceGameObject Menu; 

    }
}