using UnityEngine;
using UnityEngine.Rendering;

namespace u1w202504
{
    [CreateAssetMenu(fileName = nameof(BackgroundInfo), menuName = nameof(BackgroundInfo))]
    public sealed class BackgroundInfo : ScriptableObject
    {
        public AudioClip Bgm;
        public Sprite Sprite;
        public VolumeProfile VolumeProfile;
    }
}