using Arbor;
using MornWeb;
using UnityEngine;

namespace u1w202504
{
    public class OpenWebState : StateBehaviour
    {
        [SerializeField] private string _url;

        public override void OnStateBegin()
        {
            MornWebUtil.Open(_url);
        }
    }
}