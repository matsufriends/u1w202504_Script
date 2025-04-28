using System.Collections.Generic;
using MornGlobal;
using MornSound;
using UnityEngine;

namespace u1w202504
{
    [CreateAssetMenu(fileName = nameof(U1WGlobal), menuName = nameof(U1WGlobal))]
    public sealed class U1WGlobal : MornGlobalBase<U1WGlobal>
    {
        [Header("Rush")]
        public float RushLeftTimeMax = 30f;
        public float RushLeftTimeMin = 10f;
        public float RushLeftTimeDif = 0.5f;
        [Header("Sound")]
        public AudioClip SmileAudioClip;
        public AudioClip TransitionStartAudioClip;
        public AudioClip TransitionEndAudioClip;
        public AudioClip[] StoneAudioClips;
        public Vector2 StoneSoundVelocityRange = new Vector2(1, 2f);
        [Header("Transition")]
        public MornSoundVolumeType TransitionVolumeType;
        public float TransitionVolumeDuration;
        [Header("Hand")]
        public float HandMass = 3;
        public float HandPowerRange = 3;
        public float HandDirAcceleration = 10;
        public float HandDirPower => HandMass * HandDirAcceleration;
        public float HandImpactPowerToMoaiScale = 1;
        public float HandStayPowerToMoaiScale = 1;
        [Header("HandArrow")]
        public float HandArrowHideLength = 1f;
        public float HandArrowShowLength = 2f;
        public Color HandArrowHideColor = Color.clear;
        public Color HandArrowShowColor = Color.white;
        public float HandArrowTriangleOffset = 0.5f;
        public float HandArrowTriangleAngle = 30;
        public float HandArrowTriangleLength = 1;
        [Header("Moai")]
        public MoaiInfo MoaiTitleInfo;
        public List<MoaiInfo> MoaiInfos;
        public Vector2 MoaiAngryRangeX;
        public Vector2 MoaiAngryRangeY;
        public float MoaiDeadY = -5;
        public float MoaiStopLinearVelocity = 0.1f;
        public float MoaiStopAngularVelocity = 0.1f;
        public float MoaiStopNeedTime = 1f;
        public float MoaiStopSuccessDegree = 10;
        public Vector2 MoaiSpawnPos = new(0, 5);
        public Vector2 MoaiSpawnPosDif = new(1, 2f);
        public Vector2 MoaiSpawnDegree = new(90, 270);
        public float MoaiNoHaveRateScale = 10;
        [Header("Ground")]
        public DodaiInfo DodaiTitleInfo;
        public List<DodaiInfo> DodaiInfos;
        public Vector2 DodaiSpawnPos = new(0, -5);
        [Header("Background")]
        public List<BackgroundInfo> BackgroundInfos;
        protected override string ModuleName => nameof(U1WGlobal);

        public static void Log(string message)
        {
            I.LogInternal(message);
        }

        public static void LogError(string message)
        {
            I.LogErrorInternal(message);
        }

        public static void LogWarning(string message)
        {
            I.LogWarningInternal(message);
        }

        public static void SetDirty(Object target)
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(target);
#endif
        }
    }
}