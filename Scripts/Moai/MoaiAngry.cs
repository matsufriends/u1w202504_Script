using UnityEngine;

namespace u1w202504
{
    public sealed class MoaiAngry : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigid;
        [SerializeField] private SpriteRenderer _angryRenderer;

        private void Update()
        {
            var pos = _rigid.position;
            var rangeX = U1WGlobal.I.MoaiAngryRangeX;
            var rangeY = U1WGlobal.I.MoaiAngryRangeY;
            var isAngry = pos.x < rangeX.x || pos.x > rangeX.y || pos.y < rangeY.x || pos.y > rangeY.y;
            _angryRenderer.enabled = isAngry;
        }
    }
}