using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace u1w202504
{
    public sealed class UIMoaiBook : MonoBehaviour
    {
        [SerializeField] private UIMoaiBookCell _prefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private TMP_Text _foundText;
        [SerializeField] private TMP_Text _standText;
        [Inject] private IObjectResolver _resolver;
        [Inject] private SaveManager _saveManager;

        private void Awake()
        {
            var infos = U1WGlobal.I.MoaiInfos;
            foreach (var info in infos)
            {
                var moaiBook = _resolver.Instantiate(_prefab, _parent);
                moaiBook.SetUp(info);
            }
            
            
            var foundNum = _saveManager.Current.FoundNum;
            var maxFoundNum = U1WGlobal.I.MoaiInfos.Count;
            var rate = (float)foundNum / maxFoundNum;
            _foundText.text = $"みつけた数: {_saveManager.Current.FoundNum} ({rate:P0})";
            _standText.text = $"おこした数: {_saveManager.Current.StandNum}";
        }
    }
}