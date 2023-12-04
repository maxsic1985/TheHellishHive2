using UnityEngine;
using UnityEngine.AddressableAssets;


namespace HalfDiggers.Runner
{
    [CreateAssetMenu(fileName = nameof(UILeadBoardMenuData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(UILeadBoardMenuData))]
    public class UILeadBoardMenuData : ScriptableObject
    {
        [Header("Menu")] 
        public AssetReferenceGameObject Menu; 

    }
}