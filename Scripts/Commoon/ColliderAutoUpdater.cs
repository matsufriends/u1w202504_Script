using System.Collections.Generic;
using UnityEngine;

namespace u1w202504
{
    [ExecuteAlways]
    [RequireComponent(typeof(PolygonCollider2D), typeof(SpriteRenderer))]
    public sealed class ColliderAutoUpdater : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private PolygonCollider2D _collider;
        [SerializeField] private Sprite _cachedSprite;

        private void Reset()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<PolygonCollider2D>();
        }

        private void Update()
        {
            if (_renderer == null || _collider == null)
            {
                return;
            }

            if (_renderer.sprite == _cachedSprite)
            {
                return;
            }

            _cachedSprite = _renderer.sprite;
            var pathCount = _cachedSprite.GetPhysicsShapeCount();
            _collider.pathCount = pathCount;
            for (var i = 0; i < _cachedSprite.GetPhysicsShapeCount(); i++)
            {
                var path = new List<Vector2>();
                _cachedSprite.GetPhysicsShape(i, path);
                _collider.SetPath(i, path.ToArray());
            }

            U1WGlobal.SetDirty(_renderer);
            U1WGlobal.SetDirty(_collider);
            U1WGlobal.SetDirty(this);
        }
    }
}