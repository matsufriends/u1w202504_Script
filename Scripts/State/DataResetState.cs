using Arbor;
using UnityEngine;
using VContainer;

namespace u1w202504
{
    public class DataResetState : StateBehaviour
    {
        [Inject] private SaveManager _saveManager;
        public override void OnStateBegin()
        {
            _saveManager.DataReset();
        }
    }
}