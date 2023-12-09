using UnityEngine;



namespace HellishHive2
{
    public sealed class CameraView : BaseView
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private TransformView _transformView;

        public TransformView TransformView => _transformView;
        public Camera Camera => _camera;
    }
}