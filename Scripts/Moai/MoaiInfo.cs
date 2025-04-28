using UnityEngine;

namespace u1w202504
{
    [CreateAssetMenu(fileName = nameof(MoaiInfo), menuName = nameof(MoaiInfo))]
    public sealed class MoaiInfo : ScriptableObject
    {
        public string MoaiName;
        [Multiline(3)] public string MoaiDescription;
        public float GenerateWeight;
        public Sprite Normal;
        public Sprite Smile;
    }
}