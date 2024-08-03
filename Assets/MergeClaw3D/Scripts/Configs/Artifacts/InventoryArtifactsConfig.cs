using System.Collections.Generic;
using System.Linq;
using MergeClaw3D.Scripts.Inventory.Enum;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Artifacts
{
    [CreateAssetMenu(fileName="InventoryArtifactsConfig", menuName = "Stage/InventoryArtifactsConfig")]
    public class InventoryArtifactsConfig : SerializedScriptableObject
    {
        [OdinSerialize] private List<ArtifactData> _mutationsArtifactsData;

        public List<ArtifactData> GetAllMutationsArtifactsData()
        {
            return _mutationsArtifactsData;
        }
        
        public ArtifactData GetMutationArtifactData(ArtifactType type)
        {
            return _mutationsArtifactsData.FirstOrDefault(x => x.Type == type);
        }
    }
}
