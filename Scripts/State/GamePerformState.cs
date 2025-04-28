using Arbor;
using UnityEngine;
using VContainer;

namespace u1w202504
{
    public sealed class GamePerformState : StateBehaviour
    {
        [SerializeField] private StateLink _onFailed;
        [SerializeField] private StateLink _onSuccess;
        [Inject] private MoaiGenerator _moaiGenerator;
        [Inject] private HandCtrl _handCtrl;
        [Inject] private SaveManager _saveManager;
        private float _stopTime;

        public override void OnStateUpdate()
        {
            _handCtrl.HandUpdate();
            var moai = _moaiGenerator.LatestMoai;
            if (moai.CurPos.y < U1WGlobal.I.MoaiDeadY)
            {
                Transition(_onFailed);
                return;
            }

            if (moai.IsSuccess)
            {
                _saveManager.Current.AddMoaiCount(moai.MoaiName);
                _saveManager.Save();
                Transition(_onSuccess);
                return;
            }
        }
    }
}