using MornLib.Extensions;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace u1w202504
{
    public sealed class MoaiGenerator : MonoBehaviour
    {
        [SerializeField] private Moai _moaiPrefab;
        [Inject] private IObjectResolver _resolver;
        [Inject] private SaveManager _saveManager;
        public Moai LatestMoai { get; private set; }

        public void Generate(bool isTitle)
        {
            Calculate(isTitle, out var spawnPos, out var spawnRot, out var info);
            LatestMoai = _resolver.Instantiate(_moaiPrefab, spawnPos, spawnRot);
            LatestMoai.SetUp(info);
        }

        private void Calculate(bool isTitle, out Vector2 spawnPos, out Quaternion spawnRot, out MoaiInfo info)
        {
            if (isTitle)
            {
                spawnPos = U1WGlobal.I.MoaiSpawnPos;
                spawnRot = Quaternion.identity;
                info = U1WGlobal.I.MoaiTitleInfo;
            }
            else
            {
                var centerPos = U1WGlobal.I.MoaiSpawnPos;
                var length = U1WGlobal.I.MoaiSpawnPosDif.GetRandomValue();
                var dif = Random.insideUnitCircle * length;
                spawnPos = centerPos + dif;
                spawnRot = Quaternion.Euler(0f, 0f, U1WGlobal.I.MoaiSpawnDegree.GetRandomValue());
                info = U1WGlobal.I.MoaiInfos.RandomValue(x =>
                {
                    var count = _saveManager.Current.GetMoaiCount(x.MoaiName);
                    var scale = count > 0 ? 1 : U1WGlobal.I.MoaiNoHaveRateScale; 
                    return x.GenerateWeight * scale;
                });
            }
        }

        public void Respawn()
        {
            if (LatestMoai == null)
            {
                U1WGlobal.LogError("Moai is null");
                return;
            }

            Calculate(true, out var spawnPos, out var spawnRot, out var info);
            LatestMoai.transform.position = spawnPos;
            LatestMoai.transform.rotation = spawnRot;
            LatestMoai.SetUp(info);
        }
    }
}