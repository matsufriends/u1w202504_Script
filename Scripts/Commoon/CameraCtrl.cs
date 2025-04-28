using UnityEngine;

namespace u1w202504
{
    public sealed class CameraCtrl : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        public Camera Camera => _camera;
    }
}