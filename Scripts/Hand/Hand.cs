using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace u1w202504
{
    public class Hand : MonoBehaviour, ILeftHand, IRightHand
    {
        [SerializeField] private Rigidbody2D _rigid;
        [SerializeField] private HandArrow _handArrow;
        private Vector2 _aimPos;
        public Vector2 CurPos => _rigid.position;
        public Vector2 ToAimDir => _aimPos - CurPos;

        void IHand.SetAimPos(Vector2 aimPos)
        {
            _aimPos = aimPos;
        }

        void IHand.SetFree()
        {
            _aimPos = CurPos;
        }
        
        void IHand.Hide()
        {
            _handArrow.gameObject.SetActive(false);
            transform.localScale = Vector3.zero;
        }

        private void Reset()
        {
            _rigid = GetComponentInChildren<Rigidbody2D>();
            _handArrow = GetComponentInChildren<HandArrow>();
        }

        private void Awake()
        {
            _aimPos = CurPos;
            _rigid.mass = U1WGlobal.I.HandMass;
            _rigid.OnCollisionEnter2DAsObservable().Subscribe(x => PushMoai(x, ForceMode2D.Impulse)).AddTo(this);
            _rigid.OnCollisionStay2DAsObservable().Subscribe(x => PushMoai(x, ForceMode2D.Force)).AddTo(this);
        }

        private void Update()
        {
            _handArrow.Set(CurPos, _aimPos);
        }

        private void FixedUpdate()
        {
            if (_rigid == null)
            {
                return;
            }

            var dir = Vector2.ClampMagnitude(ToAimDir, U1WGlobal.I.HandPowerRange);
            var aimVelocity = dir * U1WGlobal.I.HandDirPower;
            var difVelocity = aimVelocity - _rigid.linearVelocity;
            _rigid.AddForce(difVelocity, ForceMode2D.Force);
            Debug.DrawLine(CurPos, CurPos + dir, Color.red);
        }

        private void OnDrawGizmos()
        {
            var cachedColor = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(CurPos, U1WGlobal.I.HandPowerRange);
            Gizmos.color = cachedColor;
        }

        private void PushMoai(Collision2D other, ForceMode2D forceMode)
        {
            if (other.gameObject.TryGetComponent(out Moai moai))
            {
                var force = forceMode == ForceMode2D.Force ? ToAimDir * U1WGlobal.I.HandStayPowerToMoaiScale
                    : ToAimDir * U1WGlobal.I.HandImpactPowerToMoaiScale;
                for (var i = 0; i < other.contactCount; i++)
                {
                    var contactPoint = other.GetContact(i);
                    moai.AddForceAtPosition(force, contactPoint.point, forceMode);
                    var color = forceMode == ForceMode2D.Force ? Color.yellow : Color.green;
                    Debug.DrawLine(contactPoint.point, contactPoint.point + force, color, 0.1f);
                }
            }
        }
    }
}