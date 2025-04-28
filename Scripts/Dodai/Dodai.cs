using UnityEngine;

namespace u1w202504
{
    public sealed class Dodai : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;

        public void SetUp(DodaiInfo info)
        {
            _renderer.sprite = info.Sprite;
        }
    }
}