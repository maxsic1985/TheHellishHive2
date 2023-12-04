using System;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HellishHive2
{
    [Serializable]
    public struct Resource
    {
        public string ID;
        public string Name;
        public ResourceType Type;
        public AssetReferenceSprite Sprite;
        public ScriptableObject AdditionalData;
    }
}