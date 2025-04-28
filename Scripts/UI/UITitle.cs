using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace u1w202504.UI
{
    public sealed class UITitle : MonoBehaviour
    {
        [SerializeField] private Slider _seSlider;
        [SerializeField] private TMP_Text _seTextA;
        [SerializeField] private TMP_Text _seTextB;
        [SerializeField] private Slider _bgmSlider;
        [SerializeField] private TMP_Text _bgmTextA;
        [SerializeField] private TMP_Text _bgmTextB;
        [Inject] private SaveManager _saveManager;

        private void Awake()
        {
            _seSlider.value = _saveManager.Current.SeVolume;
            _bgmSlider.value = _saveManager.Current.BgmVolume;
            _seSlider.OnValueChangedAsObservable().Subscribe(
                x =>
                {
                    _saveManager.Current.SeVolume = x;
                    UpdateText();
                }).AddTo(this);
            _bgmSlider.OnValueChangedAsObservable().Subscribe(
                x =>
                {
                    _saveManager.Current.BgmVolume = x;
                    UpdateText();
                }).AddTo(this);
            _seSlider.OnPointerUpAsObservable().Subscribe(_ => _saveManager.Save()).AddTo(this);
            _bgmSlider.OnPointerUpAsObservable().Subscribe(_ => _saveManager.Save()).AddTo(this);
            UpdateText();
        }

        private void UpdateText()
        {
            _seTextA.text = $"SE  {_saveManager.Current.SeVolume * 100:F0}%";
            _seTextB.text = $"SE  {_saveManager.Current.SeVolume * 100:F0}%";
            _bgmTextA.text = $"BGM  {_saveManager.Current.BgmVolume * 100:F0}%";
            _bgmTextB.text = $"BGM  {_saveManager.Current.BgmVolume * 100:F0}%";
        }
    }
}