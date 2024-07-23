using System.Collections.Generic;
using System.Linq;
using MergeClaw3D.Scripts.Configs.Stages.Data.Modules;
using MergeClaw3D.Scripts.Extensions;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Items
{
    [CreateAssetMenu(fileName = "ItemsConfig", menuName = "Items/ItemsConfig")]
    public class ItemsConfig : ScriptableObject
    {
        [SerializeField] private List<ItemConfigData> _itemDatas;

        public List<ItemConfigData> GetCorrespondingItemsData(ItemsDataProviderModule dataProviderModule)
        {
            var copy = _itemDatas.ToList();

            var finalResults = new List<ItemConfigData>();
            foreach (var itemDataSet in dataProviderModule.ItemsDataSets)
            {
                if (itemDataSet.IsRandomVariants)
                {
                    finalResults.AddRange(GetRandomItemsData(copy, itemDataSet.GroupsCount));
                }
                else
                {
                    finalResults.AddRange(GetPredefinedItems(copy, itemDataSet));
                }
            }

            return finalResults;
        }

        private List<ItemConfigData> GetRandomItemsData(List<ItemConfigData> source, int groupsCount)
        {
            source = source.Shuffle();
            return source.GetRange(0, groupsCount);
        }

        private List<ItemConfigData> GetPredefinedItems(List<ItemConfigData> source, ItemsDataProviderModule.ItemDataSet itemDataSet)
        {
            var finalResult = new List<ItemConfigData>();
            
            for (int i = 0; i < itemDataSet.GroupsCount; i++)
            {
                finalResult.Add(source.FirstOrDefault(x=>x.Id==itemDataSet.VariantIndex));
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
