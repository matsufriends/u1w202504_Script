using Arbor;
using VContainer;

namespace u1w202504
{
    public sealed class HandUpdateState : StateBehaviour
    {
        [Inject] private HandCtrl _handCtrl;

        public override void OnStateUpdate()
        {
            _handCtrl.HandUpdate();
        }
    }
}