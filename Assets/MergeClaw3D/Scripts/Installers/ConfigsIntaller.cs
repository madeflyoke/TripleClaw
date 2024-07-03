using MergeClaw3D.Scripts.Configs.Items;
using MergeClaw3D.Scripts.Configs.Stages;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Installers
{
    public class ConfigsInstaller : MonoInstaller
    {
        [SerializeField] private ItemsConfig _itemsConfig;
        [SerializeField] private ItemsSpawnerConfig _itemsSpawnerConfig;
        [SerializeField] private StagesConfig _stagesConfig;

        public override void InstallBindings()
        {
            Container.Bind<ItemsConfig>().FromInstance(_itemsConfig);
            Container.Bind<ItemsSpawnerConfig>().FromInstance(_itemsSpawnerConfig);
            Container.Bind<StagesConfig>().FromInstance(_stagesConfig);
        }
    }
}