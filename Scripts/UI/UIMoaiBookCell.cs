using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace u1w202504
{
    public sealed class UIMoaiBookCell : MonoBehaviour
    {
        [SerializeField] private GameObject _lock;
        [SerializeField] private GameObject _unlock;
        [SerializeField] private Image _moaiLockImage;
        [SerializeField] private Image _moaiUnlockImage;
        [SerializeField] private Image _moaiCursorImage;
        [SerializeField] private TMP_Text _moaiName;
        [Inject] private SaveManager _saveManager;
        private MoaiInfo _moaiInfo;

        private void Awake()
        {
            _moaiCursorImage.OnPointerEnterAsObservable().Subscribe(
                _ =>
                {
                    _moaiUnlockImage.sprite = _moaiInfo.Smile;
                }).AddTo(this);
            _moaiCursorImage.OnPointerExitAsObservable().Subscribe(
                _ =>
                {
                    _moaiUnlockImage.sprite = _moaiInfo.Normal;
                }).AddTo(this);
        }

        public void SetUp(MoaiInfo moaiInfo)
        {
            _moaiInfo = moaiInfo;
            var count = _saveManager.Current.GetMoaiCount(moaiInfo.MoaiName);
            var isLocked = count == 0;
            _moaiLockImage.sprite = moaiInfo.Normal;
            _moaiUnlockImage.sprite = moaiInfo.Normal;
            _lock.SetActive(isLocked);
            _unlock.SetActive(!isLocked);
            _moaiName.text = isLocked ? "???" : $"{moaiInfo.MoaiName}\n{count} オコシ";
        }
    }
}