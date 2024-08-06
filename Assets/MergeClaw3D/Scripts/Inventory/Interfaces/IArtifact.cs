using MergeClaw3D.Scripts.Inventory.Enum;

namespace MergeClaw3D.Scripts.Inventory.Interfaces
{
    public interface IArtifact
    {
        public ArtifactType ArtifactType { get;}
        
        public T GetArtifactComponent<T>() where T : IArtifactComponent;
    }
}
