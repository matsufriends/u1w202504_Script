using VContainer;
using VContainer.Unity;

namespace u1w202504
{
    public sealed class HandCtrl
    {
        [Inject] private ILeftHand _leftHand;
        [Inject] private IRightHand _rightHand;
        [Inject] private InputCtrl _inputCtrl;

        public void HideHand()
        {
            _leftHand.Hide();
            _rightHand.Hide();
        }

        public void HandUpdate()
        {
            if (_inputCtrl.UseLeftHand)
            {
                _leftHand.SetAimPos(_inputCtrl.GetMouseWorldPos());
            }
            else
            {
                _leftHand.SetFree();
            }

            if (_inputCtrl.UseRightHand)
            {
                _rightHand.SetAimPos(_inputCtrl.GetMouseWorldPos());
            }
            else
            {
                _rightHand.SetFree();
            }
        }
    }
}