using Arbor;
using VContainer;

namespace u1w202504
{
    public sealed class TitlePerformState : StateBehaviour
    {
        [Inject] private MoaiGenerator _moaiGenerator;
        [Inject] private HandCtrl _handCtrl;
        private float _stopTime;

        public override void OnStateUpdate()
        {
            var moai = _moaiGenerator.LatestMoai;
            if (moai.CurPos.y < U1WGlobal.I.MoaiDeadY)
            {
                _moaiGenerator.Respawn();
            }

            _handCtrl.HandUpdate();
        }
    }
}