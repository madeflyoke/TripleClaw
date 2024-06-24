using System.Collections.Generic;
using Lean.Pool;
using MergeClaw3D.Scripts.Configs.Items;
using MergeClaw3D.Scripts.Items;
using MergeClaw3D.Scripts.Items.Data;
using MergeClaw3D.Scripts.Items.Enums;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace MergeClaw3D.Scripts.Factories
{
    public class ItemsFactory : SerializedMonoBehaviour
    {
        [SerializeField, ReadOnly] private Dictionary<int, ItemEntity> _itemsCachedPrefabsMap;
        
        private void Awake()
        {
           _itemsCachedPrefabsMap.Values.ForEach(x => x.gameObject.SetActive(false));
        }
        
        public ItemEntity Create(GameObjectSpawnData spawnData, ItemConfigData itemConfigData, ItemSpecificationData specificationData)
        {
            var targetPrefab = _itemsCachedPrefabsMap[itemConfigData.Id];
            
            ItemEntity itemEntity = LeanPool.Spawn(targetPrefab, spawnData.Parent, injected: true);

            var tr = itemEntity.transform;
            
            tr.localScale = spawnData.Scale;
            tr.position = spawnData.Position;
            tr.gameObject.SetActive(true);
            return itemEntity;
        }
        
        
#if UNITY_EDITOR

        [SerializeField] private ItemEntity EDITOR_itemBasePrefab;
        [SerializeField] private ItemsConfig EDITOR_itemsConfig;

        [Button]
        private void SpawnCachedPrefabs()
        {
            _itemsCachedPrefabsMap = new Dictionary<int, ItemEntity>();
            for (int i = transform.childCount-1; i >=0 ; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }

            foreach (var itemConfigData in EDITOR_itemsConfig.GetAllItemsData())
            {
                var newItemEntity = Instantiate(EDITOR_itemBasePrefab, transform);
                if (itemConfigData.Mesh==null)
                {
                    Debug.LogError("Item config data mesh is null, Id: "+ itemConfigData.Id);
                    continue;
                }
                newItemEntity.gameObject.name = "ItemEntity_" + itemConfigData.Mesh.name;
                newItemEntity.Initialize(itemConfigData, new ItemSpecificationData(ItemSize.LARGE));
                
                _itemsCachedPrefabsMap.Add(itemConfigData.Id, newItemEntity);
            }
            
        }
        
#endif
    }
}