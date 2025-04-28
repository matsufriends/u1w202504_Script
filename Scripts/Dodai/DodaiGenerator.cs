using MornLib.Extensions;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace u1w202504
{
    public sealed class DodaiGenerator : MonoBehaviour
    {
        [SerializeField] private Dodai _dodaiPrefab;
        [Inject] private IObjectResolver _resolver;
        public Dodai LatestDodai { get; private set; }

        public void Generate(bool isTitle)
        {
            var spawnPos = U1WGlobal.I.DodaiSpawnPos;
            LatestDodai = _resolver.Instantiate(_dodaiPrefab, spawnPos, Quaternion.identity);
            var info = isTitle ? U1WGlobal.I.DodaiTitleInfo : U1WGlobal.I.DodaiInfos.RandomValue(x => x.GenerateWeight);
            LatestDodai.SetUp(info);
        }
    }
}