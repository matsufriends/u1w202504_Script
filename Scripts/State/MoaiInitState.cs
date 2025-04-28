using Arbor;
using UnityEngine;
using VContainer;

namespace u1w202504
{
    public sealed class MoaiInitState : StateBehaviour
    {
        [SerializeField] private bool _isTitle;
        [SerializeField] private StateLink _nextState;
        [Inject] private MoaiGenerator _moaiGenerator;
        [Inject] private DodaiGenerator _dodaiGenerator;

        public override void OnStateBegin()
        {
            _moaiGenerator.Generate(_isTitle);
            _dodaiGenerator.Generate(_isTitle);
            Transition(_nextState);
        }
    }
}