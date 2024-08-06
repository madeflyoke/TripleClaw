using MergeClaw3D.Scripts.Configs.Artifacts;
using MergeClaw3D.Scripts.Inventory.Enum;
using MergeClaw3D.Scripts.Inventory.Interfaces;
using UnityEngine;

namespace MergeClaw3D.Scripts.Inventory.UI
{
    public class InventoryArtifactSlotUI : MonoBehaviour
    {
        public bool IsEmpty { get; private set; } = true;
        public IArtifact RelatedArtifact { get; private set; }

        [SerializeField] private InventoryArtifactsConfig _artifactsConfig;
        [SerializeField] private Transform _artifactParent;

        public void SetArtifact(ArtifactType type)
        {
            var artifactMonoBeh = Instantiate(_artifactsConfig.GetMutationArtifactData(type).ArtifactOriginal as MonoBehaviour, _artifactParent);
            RelatedArtifact = artifactMonoBeh as IArtifact;
            IsEmpty = false;
        }

        public void RemoveArtifact()
        {
            var artifactGo = (RelatedArtifact as MonoBehaviour)?.gameObject;
            Destroy(artifactGo);
            RelatedArtifact = null;
            IsEmpty = true;
        }
    }
}
