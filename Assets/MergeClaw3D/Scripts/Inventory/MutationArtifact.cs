using System.Collections.Generic;
using System.Linq;
using MergeClaw3D.Scripts.Inventory.Enum;
using MergeClaw3D.Scripts.Inventory.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.Inventory
{
    public class MutationArtifact : SerializedMonoBehaviour, IArtifact
    {
        [field: SerializeField] public ArtifactType ArtifactType { get; private set; }
        [SerializeField] private List<IArtifactComponent> _components;
        
        public T GetArtifactComponent<T>() where T : IArtifactComponent
        {
            return (T)_components.FirstOrDefault(x => x.GetType() == typeof(T));
        }
    }
}
