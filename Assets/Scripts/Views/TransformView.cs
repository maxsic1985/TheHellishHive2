using UnityEngine;



namespace HellishHive2
{
    public sealed class TransformView : BaseView
    {
        [SerializeField] private Transform _transform;

        public Transform Transform => _transform;
    }
}