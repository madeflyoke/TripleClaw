using MergeClaw3D.Scripts.Inventory.Enum;
using UnityEngine;

namespace MergeClaw3D.Scripts.Inventory
{
    public class MutationArtifact : MonoBehaviour, IArtifact
    {
        public ArtifactType ArtifactType { get; private set; }
        
        public void Initialize(ArtifactType correspondingType)
        {
            ArtifactType = correspondingType;
        }
    }
}
