using Arbor;
using UnityEngine;
using VContainer;

namespace u1w202504
{
    public sealed class SkipState : StateBehaviour
    {
        [SerializeField] private StateLink _nextState;
        public override void OnStateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Transition(_nextState);
            }
        }
    }
}