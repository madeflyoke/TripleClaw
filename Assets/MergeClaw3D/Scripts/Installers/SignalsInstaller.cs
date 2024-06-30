using MergeClaw3D.Scripts.Signals;
using Zenject;

namespace MergeClaw3D.Scripts.Installers
{
    public class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<StageStartedSignal>();
            Container.DeclareSignal<StageCompletedSignal>();
            Container.DeclareSignal<StageFailedSignal>();
            
            Container.DeclareSignal<ResetSignal>();
        }
    }
}
