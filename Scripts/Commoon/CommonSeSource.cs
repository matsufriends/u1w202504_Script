using MornSound;
using UnityEngine;

namespace u1w202504
{
    public sealed class CommonSeSource : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        private int _nextIndex;

        public void PlayOneShot(AudioClip clip, float volume = 1f)
        {
            _audioSource.MornPlayOneShot(clip, volume);
        }
    }
}