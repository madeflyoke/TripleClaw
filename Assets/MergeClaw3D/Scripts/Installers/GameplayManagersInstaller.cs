using MergeClaw3D.Scripts.Stages;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Installers
{
    public class GameplayManagersInstaller : MonoInstaller
    {
        [SerializeField] private StagesManager _stagesManager;

        public override void InstallBindings()
        {
            Container.BindInstance(_stagesManager).AsSingle().NonLazy();
        }
    }
}
