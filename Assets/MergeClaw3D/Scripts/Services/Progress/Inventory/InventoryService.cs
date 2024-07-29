using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using MergeClaw3D.Scripts.Inventory.Enum;
using MergeClaw3D.Scripts.Services.Interfaces;

namespace MergeClaw3D.Scripts.Services.Progress.Inventory
{
    public class InventoryService : IService
    {
        public event Action<ArtifactType> ArtifactAdded;
        public event Action<ArtifactType> ArtifactRemoved;
        
        private HashSet<ArtifactType> _items;
        private InventoryProgressHandler _inventoryProgressHandler;
        
        public UniTask Initialize(CancellationTokenSource cts)
        {
            _inventoryProgressHandler = new InventoryProgressHandler();
            InitializeInventory();
            return UniTask.CompletedTask;
        }

        private void InitializeInventory()
        {
            _items = new HashSet<ArtifactType>();
            
            foreach (ArtifactType item in Enum.GetValues(typeof(ArtifactType)))
            {
                if (_inventoryProgressHandler.IsArtifactExist(item))
                {
                    _items.Add(item);
                }
            }  
        }
        
        public void AddArtifact(ArtifactType artifact)
        {
            _items.Add(artifact);
            _inventoryProgressHandler.SaveArtifactState(artifact,true);
            ArtifactAdded?.Invoke(artifact);
        }

        public void RemoveArtifact(ArtifactType artifact)
        {
            if (_items.Contains(artifact))
            {
                _items.Remove(artifact);
                _inventoryProgressHandler.SaveArtifactState(artifact,false);
                ArtifactRemoved?.Invoke(artifact);
            }
        }

        public bool ContainsArtifact(ArtifactType artifact)
        {
            return _items.Contains(artifact);
        }

        public HashSet<ArtifactType> GetExistsArtifacts()
        {
            return _items;
        }
        
        public void Dispose()
        {
            _inventoryProgressHandler?.SaveArtifacts(_items);
        }
    }
}
