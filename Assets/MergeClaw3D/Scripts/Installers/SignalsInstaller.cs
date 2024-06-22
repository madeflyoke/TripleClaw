using MergeClaw3D.Scripts.Signals;
using Zenject;

namespace MergeClaw3D.Scripts.Installers
{
    public class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<GameStartedSignal>();
            Container.DeclareSignal<LevelCompletedSignal>();
            Container.DeclareSignal<StartGameCallSignal>();
            Container.DeclareSignal<ResetSignal>();
        }
    }
}
