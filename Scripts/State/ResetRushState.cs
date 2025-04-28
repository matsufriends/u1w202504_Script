using Arbor;
using VContainer;

namespace u1w202504
{
    public class ResetRushState : StateBehaviour
    {
        [Inject] private RushService _rushService;

        public override void OnStateBegin()
        {
            _rushService.ResetRushCount();
        }
    }
}