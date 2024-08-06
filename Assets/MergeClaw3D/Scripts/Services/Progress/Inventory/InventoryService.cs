using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using MergeClaw3D.Scripts.Inventory.Enum;
using MergeClaw3D.Scripts.Services.Interfaces;
using UnityEngine;

namespace MergeClaw3D.Scripts.Services.Progress.Inventory
{
    public class InventoryService : IService
    {
        public event Action<ArtifactType> ArtifactAdded;
        public event Action<ArtifactType> ArtifactRemoved;
        
        private HashSet<ArtifactType> _artifacts;
        private InventoryProgressHandler _inventoryProgressHandler;
        
        public UniTask Initialize(CancellationTokenSource cts)
        {
            _inventoryProgressHandler = new InventoryProgressHandler();
            InitializeInventory();
            return UniTask.CompletedTask;
        }

        private void InitializeInventory()
        {
            _artifacts = new HashSet<ArtifactType>();
            
            foreach (ArtifactType item in Enum.GetValues(typeof(ArtifactType)))
            {
                if (_inventoryProgressHandler.IsArtifactExist(item))
                {
                    _artifacts.Add(item);
                }
            }  
        }
        
        public void AddArtifact(ArtifactType artifact)
        {
            _artifacts.Add(artifact);
            _inventoryProgressHandler.SaveArtifactState(artifact,true);
            ArtifactAdded?.Invoke(artifact);
            
#if UNITY_EDITOR
            Debug.LogWarning("Artifact added: "+artifact);
#endif
        }

        public void RemoveArtifact(ArtifactType artifact)
        {
            if (_artifacts.Contains(artifact))
            {
                _artifacts.Remove(artifact);
                _inventoryProgressHandler.SaveArtifactState(artifact,false);
                ArtifactRemoved?.Invoke(artifact);
                
#if UNITY_EDITOR
                Debug.LogWarning("Artifact removed: "+artifact);
#endif
            }
        }

        public bool ContainsArtifact(ArtifactType artifact)
        {
            return _artifacts.Contains(artifact);
        }

        public HashSet<ArtifactType> GetExistsArtifacts()
        {
            return _artifacts;
        }
        
        public void Dispose()
        {
            _inventoryProgressHandler?.SaveArtifacts(_artifacts);
        }
    }
}
