using System.Collections.Generic;
using System.Text;
using MergeClaw3D.Scripts.Inventory.Enum;
using UnityEngine;

namespace MergeClaw3D.Scripts.Services.Progress.Inventory
{
    public class InventoryProgressHandler
    {
        private readonly StringBuilder _stringBuilder;

        public InventoryProgressHandler()
        {
            _stringBuilder = new StringBuilder();
        }

        public void SaveArtifacts(HashSet<ArtifactType> types)
        {
            foreach (var item in types)
            {
                SaveArtifactState(item, true);
            }
        }
        
        public void SaveArtifactState(ArtifactType type, bool isExists)
        {
            PlayerPrefs.SetInt(GetArtifactKey(type), isExists?1:0);
        }

        public bool IsArtifactExist(ArtifactType type)
        {
            return PlayerPrefs.GetInt(GetArtifactKey(type)) == 1;
        }
        
        private string GetArtifactKey(ArtifactType type)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(ProgressSaveKeys.SAVED_ARTIFACT);
            _stringBuilder.Append((int)type);
            return _stringBuilder.ToString();
        }
    }
}