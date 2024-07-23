using System.Collections.Generic;
using System.Linq;
using MergeClaw3D.Scripts.Configs.Stages.Data;
using MergeClaw3D.Scripts.Extensions;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MergeClaw3D.Scripts.Configs.Items
{
    [CreateAssetMenu(fileName = "ItemsConfig", menuName = "Items/ItemsConfig")]
    public class ItemsConfig : ScriptableObject
    {
        [SerializeField] private List<ItemConfigData> _itemDatas;

        public List<ItemConfigData> GetCorrespondingItemsData(ItemsStageData data)
        {
            switch (data)
            {
                case PredefinedItemsStageData stageData:
                    return GetPredefinedItems(stageData);
                default:
                    return GetRandomItemsData(data.ItemsVariantsCount);
            }
        }

        private List<ItemConfigData> GetRandomItemsData(int variantsCount)
        {
            var copy = _itemDatas.ToList().Shuffle();
            return copy.GetRange(0, variantsCount);
        }

        private List<ItemConfigData> GetPredefinedItems(PredefinedItemsStageData data)
        {
            var finalResult = new List<ItemConfigData>();
            var copy = _itemDatas.ToList();

            foreach (var itemDataSet in data.PredefinedItemsData)
            {
                for (int i = 0; i < itemDataSet.GroupsCount; i++)
                {
                    finalResult.Add(copy.FirstOrDefault(x=>x.Id==itemDataSet.Index));
                }
            }
            
            return finalResult;
        }
        
        
#if UNITY_EDITOR

        [SerializeField] private string EDITOR_itemsModelsPath;
        
        [Button]
        public void LoadAllMeshesFromPath()
        {
            _itemDatas = new List<ItemConfigData>();
            int index = 0;
            string[] guids = AssetDatabase.FindAssets("t:GameObject", new[] { EDITOR_itemsModelsPath });

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                LoadFBXMeshes(assetPath);
            }
            
            void LoadFBXMeshes(string fbxAssetPath)
            {
                GameObject fbxPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(fbxAssetPath);

                if (fbxPrefab != null)
                {
                    MeshFilter[] meshFilters = fbxPrefab.GetComponentsInChildren<MeshFilter>();

                    foreach (MeshFilter meshFilter in meshFilters)
                    {
                        Mesh mesh = meshFilter.sharedMesh;
                        _itemDatas.Add(new ItemConfigData(index, mesh));
                        index++;
                    }
                }
            }
        }
        
        private void OnValidate()
        {
            for (int i = 0; i < _itemDatas.Count; i++)
            {
                if (_itemDatas[i].Id != i)
                {
                    var data = new ItemConfigData(i, _itemDatas[i].Mesh);
                    _itemDatas[i] = data;
                }
            }
        }

#endif
    }
}
