using MornLib;
using MornSound;
using MornTransition;
using MornUGUI;
using UnityEngine;
using VContainer;

namespace u1w202504
{
    public sealed class RootLifetimeScope : MornLifetimeScope
    {
        [SerializeField] private CommonSeSource _seSource;
        [SerializeField] private MornTransitionCtrl _transition;
        [SerializeField] private SaveManager _saveManager;
        [SerializeField] private MornUGUICtrl _uguiCtrl;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.Register<RushService>(Lifetime.Singleton);
            builder.RegisterInstance(_seSource);
            builder.RegisterInstance(_transition);
            builder.RegisterInstance(_saveManager);
            builder.RegisterInstance(_uguiCtrl);
            var volumeCore = new MornSoundVolumeCore(_saveManager);
            builder.RegisterInstance(volumeCore);
            _saveManager.Load();
        }
    }
}