using MergeClaw3D.Scripts.Factories;
using MergeClaw3D.Scripts.Items;
using MergeClaw3D.Scripts.Items.Spawner;
using MergeClaw3D.Scripts.Place;
using MergeClaw3D.Scripts.Spawner;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Installers
{
    public class ItemsInstaller : MonoInstaller
    {
        [SerializeField] private ItemEntity _itemBasePrefab;
        [SerializeField] private ItemsSpawnPoint _itemsSpawnPoint;
        [SerializeField] private ItemPlacesHolder _itemsPlacesHolder;

        public override void InstallBindings()
        {
            Container.Bind<ItemsSpawnPoint>().FromInstance(_itemsSpawnPoint);
            Container.BindInterfacesAndSelfTo<ItemPlacesHolder>().FromInstance(_itemsPlacesHolder);

            Container.Bind<ItemsSpawner>().FromNew()
                .AsSingle()
                .NonLazy();
            Container.Bind<ItemsContainer>().FromNew()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<ItemPlacer>().FromNew()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<Merger>().FromNew()
                .AsSingle()
                .NonLazy();

            Container.Bind<ItemsFactory>().FromNew()
                .AsSingle()
                .WithArguments(_itemBasePrefab)
                .NonLazy();
        }
    }
}