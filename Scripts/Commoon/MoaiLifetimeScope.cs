using MornLib;
using UnityEngine;
using VContainer;

namespace u1w202504
{
    public sealed class MoaiLifetimeScope : MornLifetimeScope
    {
        [SerializeField] private Hand _leftHand;
        [SerializeField] private Hand _rightHand;
        [SerializeField] private CameraCtrl _cameraCtrl;
        [SerializeField] private MoaiGenerator _moaiGenerator;
        [SerializeField] private DodaiGenerator _dodaiGenerator;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterInstance<ILeftHand>(_leftHand);
            builder.RegisterInstance<IRightHand>(_rightHand);
            builder.RegisterInstance(_cameraCtrl);
            builder.RegisterInstance(_moaiGenerator);
            builder.RegisterInstance(_dodaiGenerator);
            builder.Register<HandCtrl>(Lifetime.Singleton);
            builder.Register<InputCtrl>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}