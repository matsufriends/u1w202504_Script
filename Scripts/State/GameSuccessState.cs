using Arbor;
using UnityEngine;
using VContainer;

namespace u1w202504
{
    public sealed class GameSuccessState : StateBehaviour
    {
        [SerializeField] private StateLink _nextState;
        [Inject] private MoaiGenerator _moaiGenerator;
        private float _stopTime;
        public override void OnStateBegin()
        {
            var moai = _moaiGenerator.LatestMoai;
            moai.Success();
        }
    }
}