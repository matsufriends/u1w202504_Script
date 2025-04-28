using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using unityroom.Api;

namespace u1w202504
{
    [Serializable]
    public sealed class SaveData
    {
        [SerializeField] private float _seVolume;
        [SerializeField] private float _bgmVolume;
        [SerializeField] private List<string> _unlockMoai = new();
        [SerializeField] private List<int> _unlockMoaiCount = new();
        private readonly Subject<float> _seVolumeSubject = new();
        private readonly Subject<float> _bgmVolumeSubject = new();
        public IObservable<float> OnSeChanged => _seVolumeSubject;
        public IObservable<float> OnBgmChanged => _bgmVolumeSubject;
        public float SeVolume
        {
            get => _seVolume;
            set
            {
                _seVolume = value;
                _seVolumeSubject.OnNext(value);
            }
        }
        public float BgmVolume
        {
            get => _bgmVolume;
            set
            {
                _bgmVolume = value;
                _bgmVolumeSubject.OnNext(value);
            }
        }
        public int FoundNum => _unlockMoai.Count;
        public int StandNum => _unlockMoaiCount.Sum();

        public SaveData()
        {
            SeVolume = 1.0f;
            BgmVolume = 1.0f;
        }

        public int GetMoaiCount(string moaiName)
        {
            var index = _unlockMoai.IndexOf(moaiName);
            if (index >= 0)
            {
                return _unlockMoaiCount[index];
            }

            return 0;
        }

        public void AddMoaiCount(string moaiName)
        {
            var index = _unlockMoai.IndexOf(moaiName);
            if (index >= 0)
            {
                _unlockMoaiCount[index]++;
            }
            else
            {
                _unlockMoai.Add(moaiName);
                _unlockMoaiCount.Add(1);
            }

            Validate();
            UnityroomApiClient.Instance.SendScore(1, FoundNum, ScoreboardWriteMode.HighScoreDesc);
            // UnityroomApiClient.Instance.SendScore(2, StandNum, ScoreboardWriteMode.HighScoreDesc);
        }

        public void Validate()
        {
            // ゆかたモアイ を おねえモアイ に変換する
            var index = _unlockMoai.IndexOf("ゆかたモアイ");
            if (index >= 0)
            {
                var count = _unlockMoaiCount[index];
                _unlockMoai.RemoveAt(index);
                _unlockMoaiCount.RemoveAt(index);
                var newIndex = _unlockMoai.IndexOf("おねえモアイ");
                if (newIndex >= 0)
                {
                    _unlockMoaiCount[newIndex] += count;
                }
                else
                {
                    _unlockMoai.Add("おねえモアイ");
                    _unlockMoaiCount.Add(count);
                }
            }
            
            // 存在しないデータは削除
            for (var i = _unlockMoai.Count - 1; i >= 0; i--)
            {
                var moaiName = _unlockMoai[i];
                if (U1WGlobal.I.MoaiInfos.All(x => x.MoaiName != moaiName))
                {
                    _unlockMoai.RemoveAt(i);
                    _unlockMoaiCount.RemoveAt(i);
                }
            }
        }
    }
}