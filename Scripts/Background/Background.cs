using MornLib.Extensions;
using UnityEngine;
using UnityEngine.Rendering;

namespace u1w202504
{
    public sealed class Background : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Volume _volume;

        private void Awake()
        {
            var info = U1WGlobal.I.BackgroundInfos.RandomValue();
            _audio.clip = info.Bgm;
            _audio.Play();
            _renderer.sprite = info.Sprite;
            _volume.profile = info.VolumeProfile;
        }
    }
}