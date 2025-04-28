using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace u1w202504
{
    public sealed class MoaiFocusCamera : MonoBehaviour
    {
        [Inject] private MoaiGenerator _moaiGenerator;
        [SerializeField] private CinemachineCamera _camera;

        private void Update()
        {
            var moai = _moaiGenerator.LatestMoai;
            if (moai == null)
            {
                return;
            }

            _camera.Follow = moai.transform;
        }
    }
}