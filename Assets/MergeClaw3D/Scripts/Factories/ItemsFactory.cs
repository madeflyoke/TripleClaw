using System;
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
        [SerializeField] private ItemEntity _itemBasePrefab;

        public ItemEntity Create(GameObjectSpawnData spawnData, ItemConfigData itemConfigData, ItemSpecificationData specificationData)
        {
            ItemEntity newItem = LeanPool.Spawn(_itemBasePrefab, spawnData.Parent);
            newItem.Initialize(itemConfigData, specificationData);

            #if UNITY_EDITOR
            newItem.gameObject.name = "ItemEntity_" + itemConfigData.Mesh.name;
            #endif
            
            var tr = newItem.transform;
            
            tr.localScale = spawnData.Scale;
            tr.position = spawnData.Position;
            tr.gameObject.SetActive(true);
            return newItem;
        }
        
        
#if UNITY_EDITOR
        
        [SerializeField] private ItemsConfig EDITOR_itemsConfig;

        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                TryClearPreviewPrefabs();
            }
        }

        private void TryClearPreviewPrefabs()
        {
            if (transform.childCount > 0)
            {
                for (int i = transform.childCount - 1; i >= 0; i--)
                {
                    if (Application.isPlaying)
                    {
                        DestroyImmediate(transform.GetChild(i).gameObject);
                    }
                    else
                    {
                        DestroyImmediate(transform.GetChild(i).gameObject);
                    }
                }
            }
        }

        private void SpawnPrefabsPreview()
        {


            var items = EDITOR_itemsConfig.GetAllItemsData();

            var rowCount = Mathf.CeilToInt(Mathf.Sqrt(items.Count));

            var index = 0;
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    if (index == items.Count)
                    {
                        return;
                    }

                    var itemConfigData = items[index];
                    var pos = transform.position + new Vector3(i + (i * 3f), 0, j + (j * 3f));

                    var newItemEntity = Instantiate(EDITOR_itemBasePrefab, pos, Quaternion.identity, transform);
                    if (itemConfigData.Mesh == null)
                    {
                        Debug.LogError("Item config data mesh is null, Id: " + itemConfigData.Id);
                        continue;
                    }

                    newItemEntity.gameObject.name = "ItemEntity_" + itemConfigData.Mesh.name;
                    newItemEntity.Initialize(itemConfigData, new ItemSpecificationData(ItemSize.LARGE));

                    _itemsCachedPrefabsMap.Add(itemConfigData.Id, newItemEntity);
                    index++;
                }
            }
        }

    }
        
#endif
    }
}