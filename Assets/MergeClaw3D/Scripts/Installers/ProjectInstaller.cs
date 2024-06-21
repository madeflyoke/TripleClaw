using MergeClaw3D.Scripts.Services;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private Bootstrapper _bootstrapper;
        
        public override void InstallBindings()
        {
            Container.Bind<Bootstrapper>().FromComponentInNewPrefab(_bootstrapper).AsSingle().NonLazy();
            Container.BindInstance(new ServicesHolder(Container)).AsSingle().NonLazy();
        }
    }
}
