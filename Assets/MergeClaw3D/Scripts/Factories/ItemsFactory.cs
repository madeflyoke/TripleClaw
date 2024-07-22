using MergeClaw3D.Scripts.Configs.Items;
using MergeClaw3D.Scripts.Items;
using MergeClaw3D.Scripts.Items.Data;
using Zenject;

namespace MergeClaw3D.Scripts.Factories
{
    public class ItemsFactory
    {
        private readonly DiContainer _diContainer;   
        private readonly ItemEntity _itemBasePrefab;

        [Inject]
        public ItemsFactory(DiContainer diContainer, ItemEntity itemBasePrefab)
        {
            _diContainer = diContainer;
            _itemBasePrefab = itemBasePrefab;
        }

        public ItemEntity Create(GameObjectSpawnData spawnData, ItemConfigData itemConfigData, ItemSpecificationData specificationData)
        {
            var newItem = _diContainer.InstantiatePrefabForComponent<ItemEntity>(_itemBasePrefab, spawnData.Parent);
            newItem.Initialize(itemConfigData, specificationData);

            #if UNITY_EDITOR
            newItem.gameObject.name = "ItemEntity_" + itemConfigData.Mesh.name;
            #endif
            
            var tr = newItem.transform;
            
            tr.localScale = spawnData.Scale;
            tr.position = spawnData.Position;
            tr.rotation = spawnData.Rotation;
            tr.gameObject.SetActive(true);

            return newItem;
        }
    }
}