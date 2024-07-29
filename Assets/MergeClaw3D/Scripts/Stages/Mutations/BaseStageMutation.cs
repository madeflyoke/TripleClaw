using MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Mutations;
using MergeClaw3D.Scripts.Inventory.Enum;
using UnityEngine;

namespace MergeClaw3D.Scripts.Stages.Mutations
{
    public abstract class BaseStageMutation : MonoBehaviour
    {
        public abstract void Initialize(IBaseStageMutationDataModule dataModule);
        
        [SerializeField] protected ArtifactType ResolveArtifactType;
    }
}
