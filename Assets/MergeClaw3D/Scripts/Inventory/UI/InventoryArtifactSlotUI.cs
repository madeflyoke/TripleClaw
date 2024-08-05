using MergeClaw3D.Scripts.Configs.Artifacts;
using MergeClaw3D.Scripts.Inventory.Enum;
using UnityEngine;

namespace MergeClaw3D.Scripts.Inventory.UI
{
    public class InventoryArtifactSlotUI : MonoBehaviour
    {
        public bool IsEmpty { get; private set; } = true;
        public ArtifactType RelatedArtifactType => _relatedArtifact.ArtifactType;
        
        [SerializeField] private InventoryArtifactsConfig _artifactsConfig;
        [SerializeField] private Transform _artifactParent;
        private IArtifact _relatedArtifact;

        public void SetArtifact(ArtifactType type)
        {
            var artifactMonoBeh = Instantiate(_artifactsConfig.GetMutationArtifactData(type).ArtifactPrefab as MonoBehaviour, _artifactParent);
            _relatedArtifact = artifactMonoBeh as IArtifact;
            IsEmpty = false;
        }

        public void RemoveArtifact()
        {
            var artifactGo = (_relatedArtifact as MonoBehaviour)?.gameObject;
            Destroy(artifactGo);
            _relatedArtifact = null;
            IsEmpty = true;
        }
    }
}
