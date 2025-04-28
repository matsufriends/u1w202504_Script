using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEditor;
using UnityEngine;
using VContainer;

namespace u1w202504
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Moai : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigid;
        [SerializeField] private SpriteRenderer _moaiFaceRenderer;
        [Inject] private CommonSeSource _seSource;
        private readonly HashSet<Collision2D> _touchHandColliders = new();
        private MoaiInfo _cachedMoaiInfo;
        private float _moaiStopTime;
        public string MoaiName => _cachedMoaiInfo.MoaiName;
        public Vector2 CurPos => _rigid.position;
        public Vector2 Velocity => _rigid.linearVelocity;
        public float AngularVelocity => _rigid.angularVelocity;
        public float Rotation => ClampAngle180(_rigid.rotation);
        private bool IsStop => _rigid.linearVelocity.magnitude <= U1WGlobal.I.MoaiStopLinearVelocity
                               && _rigid.angularVelocity <= U1WGlobal.I.MoaiStopAngularVelocity;
        public bool IsSuccess => _moaiStopTime >= U1WGlobal.I.MoaiStopNeedTime;
        private bool IsStand
        {
            get
            {
                var clampedAngle = ClampAngle180(_rigid.rotation);
                return clampedAngle >= -U1WGlobal.I.MoaiStopSuccessDegree
                       && clampedAngle <= U1WGlobal.I.MoaiStopSuccessDegree;
            }
        }
        private bool IsHandTouching => _touchHandColliders.Count > 0;

        float ClampAngle180(float angle)
        {
            angle %= 360f;
            if (angle > 180f)
                angle -= 360f;
            if (angle < -180f)
                angle += 360f;
            return angle;
        }

        private void Reset()
        {
            _rigid = GetComponent<Rigidbody2D>();
        }

        private void Awake()
        {
            _rigid.OnCollisionEnter2DAsObservable().Subscribe(
                x =>
                {
                    var hand = x.gameObject.GetComponentInParent<Hand>();
                    if (hand != null)
                    {
                        _touchHandColliders.Add(x);
                    }
                }).AddTo(this);
            _rigid.OnCollisionExit2DAsObservable().Subscribe(
                x =>
                {
                    var hand = x.gameObject.GetComponentInParent<Hand>();
                    if (hand != null)
                    {
                        _touchHandColliders.Remove(x);
                    }
                }).AddTo(this);
        }

        private void Update()
        {
            if (IsStop && IsStand && !IsHandTouching)
            {
                _moaiStopTime += Time.deltaTime;
            }
            else
            {
                _moaiStopTime = 0f;
            }
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            var cachedColor = Handles.color;
            Handles.color = Color.gray;
            Handles.Label(CurPos, $"StopTime: {_moaiStopTime}");
            Handles.Label(CurPos + Vector2.up, $"HandTouching: {_touchHandColliders.Count}");
            Handles.color = cachedColor;
#endif
        }

        public void SetUp(MoaiInfo moaiInfo)
        {
            _cachedMoaiInfo = moaiInfo;
            _moaiFaceRenderer.sprite = moaiInfo.Normal;
            _rigid.linearVelocity = Vector2.zero;
            _rigid.angularVelocity = 0f;
        }

        public void AddForceAtPosition(Vector2 force, Vector2 worldPos, ForceMode2D forceMode)
        {
            _rigid.AddForceAtPosition(force, worldPos, forceMode);
        }

        public void Success()
        {
            _moaiFaceRenderer.sprite = _cachedMoaiInfo.Smile;
            _seSource.PlayOneShot(U1WGlobal.I.SmileAudioClip);
        }
    }
}