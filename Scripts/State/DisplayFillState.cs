using System.Collections.Generic;
using Arbor;
using Cysharp.Threading.Tasks;
using MornSound;
using MornTransition;
using UnityEngine;
using VContainer;

namespace u1w202504
{
    public class DisplayFillState : StateBehaviour
    {
        [Inject] private CommonSeSource _seSource;
        [Inject] private MornTransitionCtrl _transition;
        [Inject] private MornSoundVolumeCore _volume;
        [SerializeField] private MornTransitionType _transitionType;
        [SerializeField] private bool _isImmediate;
        [SerializeField] private StateLink _onComplete;

        public override async void OnStateBegin()
        {
            if (_isImmediate)
            {
                _transition.FillImmediate(_transitionType);
                _volume.FadeImmediate(U1WGlobal.I.TransitionVolumeType, false);
            }
            else
            {
                _seSource.PlayOneShot(U1WGlobal.I.TransitionStartAudioClip);
                var taskList = new List<UniTask>
                {
                    _transition.FillAsync(_transitionType, CancellationTokenOnEnd),
                    _volume.FadeAsync(
                        new()
                        {
                            SoundVolumeType = U1WGlobal.I.TransitionVolumeType,
                            IsFadeIn = false,
                            Duration = U1WGlobal.I.TransitionVolumeDuration,
                            CancellationToken = CancellationTokenOnEnd
                        }),
                };
                await UniTask.WhenAll(taskList);
            }

            Transition(_onComplete);
        }
    }
}