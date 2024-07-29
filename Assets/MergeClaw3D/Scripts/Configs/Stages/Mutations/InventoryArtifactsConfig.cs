using MergeClaw3D.Scripts.Inventory;
using MergeClaw3D.Scripts.Inventory.Enum;
using UnityEngine;
using UnityEngine.Rendering;

namespace MergeClaw3D.Scripts.Configs.Stages.Mutations
{
    [CreateAssetMenu(fileName="InventoryArtifactsConfig", menuName = "Stage/InventoryArtifactsConfig")]
    public class InventoryArtifactsConfig : ScriptableObject
    {
        [SerializeField] private SerializedDictionary<ArtifactType, MutationArtifact> _artifactsPrefabsMap;

        public MutationArtifact GetArtifactPrefab(ArtifactType type)
        {
            if (_artifactsPrefabsMap.ContainsKey(type))
            {
                return _artifactsPrefabsMap[type];
            }

            return null;
        }
    }
}
