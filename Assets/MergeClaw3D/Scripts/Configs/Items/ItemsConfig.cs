using System;
using System.Collections.Generic;
using System.IO;
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

        public List<ItemConfigData> GetAllItemsData()
        {
            return _itemDatas;
        }
        
        public ItemConfigData GetRandomItemData()
        {
            return _itemDatas[Random.Range(0, _itemDatas.Count)];
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
