using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace u1w202504
{
    public sealed class UIMoaiStatus : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moaiText;
        [SerializeField] private Slider _velSlider; // 0 ~ 0.2
        [SerializeField] private Slider _rotSlider; // 0.4 ~ 0.6
        [Inject] private MoaiGenerator _moaiGenerator;
        [Inject] private SaveManager _saveManager;

        private void Update()
        {
            var moai = _moaiGenerator.LatestMoai;
            if (moai == null)
            {
                _moaiText.text = "";
            }
            else
            {
                var count = _saveManager.Current.GetMoaiCount(moai.MoaiName);
                _moaiText.text = $"{moai.MoaiName}\n{count} オコシ";
            }

            var vel = moai?.Velocity.magnitude ?? 0;
            var velRate = vel / (U1WGlobal.I.MoaiStopLinearVelocity * 10);
            var angVel = moai?.AngularVelocity ?? 0;
            var angVelRate = angVel / (U1WGlobal.I.MoaiStopAngularVelocity * 10);
            _velSlider.value = Mathf.Max(velRate, angVelRate);
            var rot = moai?.Rotation ?? 0;
            var rotLimit = U1WGlobal.I.MoaiStopSuccessDegree;
            var rotSlider = Mathf.InverseLerp(-rotLimit * 5, rotLimit * 5, rot);
            _rotSlider.value = rotSlider;
        }
    }
}