using UnityEngine;

namespace u1w202504
{
    [CreateAssetMenu(fileName = nameof(DodaiInfo), menuName = nameof(DodaiInfo))]
    public sealed class DodaiInfo : ScriptableObject
    {
        public float GenerateWeight;
        public Sprite Sprite;
    }
}