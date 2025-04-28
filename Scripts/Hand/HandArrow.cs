using Shapes;
using UnityEngine;

namespace u1w202504
{
    [ExecuteAlways]
    public sealed class HandArrow : MonoBehaviour
    {
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _end;
        [SerializeField] private Line _line;
        [SerializeField] private Triangle _triangle;
        private Vector2 _cachedStartPos;
        private Vector2 _cachedEndPos;

        private void Reset()
        {
            _line = GetComponentInChildren<Line>();
            _triangle = GetComponentInChildren<Triangle>();
        }

        private void Update()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            Vector2 startPos = _start.position;
            Vector2 endPos = _end.position;
            if (startPos == _cachedStartPos && endPos == _cachedEndPos)
            {
                return;
            }

            _cachedStartPos = startPos;
            _cachedEndPos = endPos;
            var dif = endPos - startPos;
            if (dif.sqrMagnitude < 0.0001f)
            {
                _line.enabled = false;
                _triangle.enabled = false;
                return;
            }

            var length = dif.magnitude;
            var colorT = Mathf.InverseLerp(U1WGlobal.I.HandArrowHideLength, U1WGlobal.I.HandArrowShowLength, length);
            var color = Color.Lerp(U1WGlobal.I.HandArrowHideColor, U1WGlobal.I.HandArrowShowColor, colorT);
            _line.Color = color;
            _triangle.Color = color;
            _line[0] = startPos;
            _line[1] = endPos;
            var dir = dif.normalized;
            var triOffset = dir * U1WGlobal.I.HandArrowTriangleOffset;
            var triAngle = U1WGlobal.I.HandArrowTriangleAngle;
            var triLength = U1WGlobal.I.HandArrowTriangleLength;
            _triangle[0] = triOffset + endPos;
            _triangle[1] = triOffset + endPos - (Vector2)(Quaternion.Euler(0, 0, triAngle) * dir * triLength);
            _triangle[2] = triOffset + endPos - (Vector2)(Quaternion.Euler(0, 0, -triAngle) * dir * triLength);
            _line.enabled = true;
            _triangle.enabled = true;
        }

        public void Set(Vector2 startPos, Vector2 endPos)
        {
            _start.position = startPos;
            _end.position = endPos;
            Update();
        }
    }
}