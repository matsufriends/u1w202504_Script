using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace u1w202504
{
    public sealed class InputCtrl : ITickable
    {
        [Inject] private CameraCtrl _cameraCtrl;
        public bool UseLeftHand { get; private set; }
        public bool UseRightHand { get; private set; }

        public Vector2 GetMouseWorldPos()
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 0;
            return _cameraCtrl.Camera.ScreenToWorldPoint(mousePos);
        }

        public void Tick()
        {
            var isUiOver = EventSystem.current.IsPointerOverGameObject();
            if (Input.GetMouseButtonDown(0) && !isUiOver)
            {
                UseLeftHand = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                UseLeftHand = false;
            }

            if (Input.GetMouseButtonDown(1) && !isUiOver)
            {
                UseRightHand = true;
            }

            if (Input.GetMouseButtonUp(1))
            {
                UseRightHand = false;
            }
        }
    }
}