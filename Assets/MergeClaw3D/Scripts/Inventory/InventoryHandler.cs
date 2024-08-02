using MergeClaw3D.Scripts.Configs.Artifacts;
using MergeClaw3D.Scripts.Configs.Stages.Mutations;
using MergeClaw3D.Scripts.Inventory.Enum;
using MergeClaw3D.Scripts.Services;
using MergeClaw3D.Scripts.Services.Progress.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Inventory
{
    public class InventoryHandler : MonoBehaviour
    {
        [SerializeField] private InventoryArtifactsConfig _config;
        private InventoryService _inventoryService;
        
        [Inject]
        public void Construct(ServicesHolder servicesHolder)
        {
            _inventoryService = servicesHolder.GetService<InventoryService>();
        }

        [Button]
        public void Spawn()
        {
            var items = _inventoryService.GetExistsArtifacts();
            foreach (var item in items)
            {
                Instantiate(_config.GetMutationArtifactData(item).ArtifactPrefab);
            }
        }

        [Button]
        public void AddItme(ArtifactType type)
        {
            _inventoryService.AddArtifact(type);
        }

        [Button]
        public void RemoveItme(ArtifactType type)
        {
            _inventoryService.RemoveArtifact(type);
        }

        [Button]
        public void Containts(ArtifactType tyoe)
        {
           Debug.LogWarning(_inventoryService.ContainsArtifact(tyoe));
        }
    }
}
