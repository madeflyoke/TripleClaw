using MergeClaw3D.Scripts.Configs.Items;
using MergeClaw3D.Scripts.Items;
using MergeClaw3D.Scripts.Items.Data;
using MergeClaw3D.Scripts.Items.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Factories
{
    public class ItemsFactory : SerializedMonoBehaviour
    {
        [Inject] private DiContainer _diContainer;   
        
        [SerializeField] private ItemEntity _itemBasePrefab;

        public ItemEntity Create(GameObjectSpawnData spawnData, ItemConfigData itemConfigData, ItemSpecificationData specificationData)
        {
            ItemEntity newItem = _diContainer.InstantiatePrefabForComponent<ItemEntity>(_itemBasePrefab, spawnData.Parent);
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

        #region EDITOR

        #if UNITY_EDITOR
        
        [SerializeField] private ItemsConfig EDITOR_itemsConfig;
        [SerializeField, ReadOnly] private Transform EDITOR_previewContainer;
        

        private void TryClearPreviewPrefabs()
        {
            if (EDITOR_previewContainer!=null)
            {
                DestroyImmediate(EDITOR_previewContainer.gameObject);
            }
        }

        [Button]
        private void SpawnPrefabsPreview()
        {
            TryClearPreviewPrefabs();
            
            var items = EDITOR_itemsConfig.GetAllItemsData();

            var rowCount = Mathf.CeilToInt(Mathf.Sqrt(items.Count));
            EDITOR_previewContainer = new GameObject().transform;
            EDITOR_previewContainer.name = "Autodestruct_PreviewItemsContainer";
            
            var index = 0;
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    if (index == items.Count)
                        return;

                    var itemConfigData = items[index];
                    var pos = transform.position + new Vector3(i + (i * 3f), 0, j + (j * 3f));

                    var newItemEntity = Instantiate(_itemBasePrefab, pos, Quaternion.identity, transform);
                    newItemEntity.transform.SetParent(EDITOR_previewContainer);
                    if (itemConfigData.Mesh == null)
                    {
                        Debug.LogError("Item config data mesh is null, Id: " + itemConfigData.Id);
                        continue;
                    }

                    newItemEntity.gameObject.name = "ItemEntity_" + itemConfigData.Mesh.name;
                    newItemEntity.Initialize(itemConfigData, new ItemSpecificationData(ItemSize.LARGE));
                    
                    index++;
                }
                
                foreach (Transform tr in EDITOR_previewContainer)
                {
                    tr.tag = "EditorOnly";
                }
                EDITOR_previewContainer.tag = "EditorOnly";
            }
        }
        
#endif

        #endregion
        
    }
    
}