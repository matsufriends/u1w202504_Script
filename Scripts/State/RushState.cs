using Arbor;
using TMPro;
using UnityEngine;
using VContainer;

namespace u1w202504
{
    public class RushState : StateBehaviour
    {
        [SerializeField] private TMP_Text _comboText;
        [SerializeField] private RectTransform _leftSlider;
        [SerializeField] private TMP_Text _timeTextA;
        [Inject] private HandCtrl _handCtrl;
        [Inject] private RushService _rushService;
        private float _leftTimeInit;
        private float _leftTime;

        public override void OnStateAwake()
        {
            var rushCount = _rushService.RushCount;
            var max = U1WGlobal.I.RushLeftTimeMax;
            var min = U1WGlobal.I.RushLeftTimeMin;
            var dif = U1WGlobal.I.RushLeftTimeDif;
            _leftTimeInit = Mathf.Max(max - rushCount * dif, min);
            _leftTime = _leftTimeInit;
        }

        private void Update()
        {
            _comboText.text = $"{_rushService.RushCount}コンボ";
            if (_leftTime > 0)
            {
                _leftTime = Mathf.Max(_leftTime - Time.deltaTime, 0);
                var rate = _leftTime / _leftTimeInit;
                _leftSlider.localScale = new Vector3(rate, 1, 1);
                _timeTextA.text = $"アト {_leftTime:F1}秒";
            }
            else
            {
                _leftSlider.localScale = new Vector3(0, 1, 1);
                _timeTextA.text = "アト 0秒";
                _handCtrl.HideHand();
            }
        }
    }
}