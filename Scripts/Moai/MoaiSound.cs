using System;
using MornLib.Extensions;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using VContainer;

namespace u1w202504
{
    public sealed class MoaiSound : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigid;
        [Inject] private CommonSeSource _seSource;

        private void Awake()
        {
            _rigid.OnCollisionEnter2DAsObservable().Subscribe(
                x =>
                {
                    var clip = U1WGlobal.I.StoneAudioClips.RandomValue();
                    var range = U1WGlobal.I.StoneSoundVelocityRange;
                    var volume = Mathf.InverseLerp(range.x, range.y, x.relativeVelocity.magnitude);
                    _seSource.PlayOneShot(clip, volume);
                }).AddTo(this);
        }
    }
}