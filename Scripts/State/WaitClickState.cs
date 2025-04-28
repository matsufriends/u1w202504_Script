using Arbor;
using UnityEngine;
using VContainer;

namespace u1w202504
{
    public sealed class WaitClickState : StateBehaviour
    {
        [SerializeField] private StateLink _nextState;
        public override void OnStateUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Transition(_nextState);
            }
        }
    }
}