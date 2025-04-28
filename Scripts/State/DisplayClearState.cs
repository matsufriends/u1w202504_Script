using System.Collections.Generic;
using Arbor;
using Cysharp.Threading.Tasks;
using MornSound;
using MornTransition;
using UnityEngine;
using VContainer;

namespace u1w202504
{
    public class DisplayClearState : StateBehaviour
    {
        [Inject] private MornTransitionCtrl _transition;
        [Inject] private MornSoundVolumeCore _volume;
        [Inject] private CommonSeSource _seSource;
        [SerializeField] private bool _isImmediate;
        [SerializeField] private StateLink _onComplete;

        public override async void OnStateBegin()
        {
            if (_isImmediate)
            {
                _transition.ClearImmediate();
                _volume.FadeImmediate(U1WGlobal.I.TransitionVolumeType, true);
            }
            else
            {
                _seSource.PlayOneShot(U1WGlobal.I.TransitionEndAudioClip);
                var taskList = new List<UniTask>
                {
                    _transition.ClearAsync(CancellationTokenOnEnd),
                    _volume.FadeAsync(
                        new()
                        {
                            SoundVolumeType = U1WGlobal.I.TransitionVolumeType,
                            IsFadeIn = true,
                            Duration = U1WGlobal.I.TransitionVolumeDuration,
                            CancellationToken = CancellationTokenOnEnd,
                        }),
                };
                await UniTask.WhenAll(taskList);
            }

            Transition(_onComplete);
        }
    }
}