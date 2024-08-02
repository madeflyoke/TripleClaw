using System.Collections.Generic;
using System.Linq;
using MergeClaw3D.Scripts.Inventory.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Artifacts
{
    [CreateAssetMenu(fileName="InventoryArtifactsConfig", menuName = "Stage/InventoryArtifactsConfig")]
    public class InventoryArtifactsConfig : SerializedScriptableObject
    {
        [SerializeField] private List<MutationArtifactData> _mutationsArtifactsData;

        public List<MutationArtifactData> GetAllMutationsArtifactsData()
        {
            return _mutationsArtifactsData;
        }
        
        public MutationArtifactData GetMutationArtifactData(ArtifactType type)
        {
            return _mutationsArtifactsData.FirstOrDefault(x => x.Type == type);
        }
    }
}
