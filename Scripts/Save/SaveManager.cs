using System;
using MornSound;
using UniRx;
using UnityEngine;

namespace u1w202504
{
    public sealed class SaveManager : MonoBehaviour, IMornSoundVolumeSaver
    {
        [SerializeField] private MornSoundVolumeType _masterVolume;
        [SerializeField] private MornSoundVolumeType _seVolume;
        [SerializeField] private MornSoundVolumeType _bgmVolume;
        private SaveData _current;
        public SaveData Current => _current ??= new SaveData();
        private const string SaveKey = "SaveData";

        public void Load()
        {
            _current = null;
            if (PlayerPrefs.HasKey(SaveKey))
            {
                var json = PlayerPrefs.GetString(SaveKey);
                var data = JsonUtility.FromJson<SaveData>(json);
                if (data != null)
                {
                    _current = data;
                }
            }

            if (_current == null)
            {
                _current = new SaveData();
            }
            _current.Validate();
            _current.OnSeChanged.Subscribe(_ => _soundVolumeSubject.OnNext(_seVolume)).AddTo(this);
            _current.OnBgmChanged.Subscribe(_ => _soundVolumeSubject.OnNext(_bgmVolume)).AddTo(this);
        }

        public void Save()
        {
            var json = JsonUtility.ToJson(_current);
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
            U1WGlobal.Log("セーブしました！ " + json);
        }
        
        public void DataReset()
        {
            var seVolume = Current.SeVolume;
            var bgmVolume = Current.BgmVolume;
            PlayerPrefs.DeleteKey(SaveKey);
            PlayerPrefs.Save();
            Load();
            _current.SeVolume = seVolume;
            _current.BgmVolume = bgmVolume;
            U1WGlobal.Log("データリセットしました！");
        }

        private readonly Subject<MornSoundVolumeType> _soundVolumeSubject = new();
        IObservable<MornSoundVolumeType> IMornSoundVolumeSaver.OnVolumeChanged => _soundVolumeSubject;

        float IMornSoundVolumeSaver.Load(MornSoundVolumeType key)
        {
            if (key == _masterVolume)
            {
                return 1.0f;
            }

            if (key == _seVolume)
            {
                return Current.SeVolume;
            }

            if (key == _bgmVolume)
            {
                return Current.BgmVolume;
            }

            throw new NotImplementedException();
        }

        void IMornSoundVolumeSaver.Save(MornSoundVolumeType key, float volumeRate)
        {
            U1WGlobal.LogError("未使用のはず");
        }
    }
}