using MergeClaw3D.Scripts.Currency;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Installers
{
    public class GameplayManagersInstaller : MonoInstaller
    {
        [SerializeField] private CurrencyManager _currencyManager;

        public override void InstallBindings()
        {
            Container.BindInstance(_currencyManager).AsSingle().NonLazy();
        }
    }
}
