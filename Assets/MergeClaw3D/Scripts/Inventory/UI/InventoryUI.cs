using System;
using System.Collections.Generic;
using System.Linq;
using MergeClaw3D.Scripts.Configs.Artifacts;
using MergeClaw3D.Scripts.Inventory.Enum;
using MergeClaw3D.Scripts.Services;
using MergeClaw3D.Scripts.Services.Progress.Inventory;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Inventory.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private List<InventoryArtifactSlotUI> _artifactsSlots;
        private InventoryService _inventoryService;

        [Inject]
        public void Construct(ServicesHolder servicesHolder)
        {
            _inventoryService = servicesHolder.GetService<InventoryService>();
        }

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (_canvas.worldCamera==null)
            {
                Debug.LogWarning("Inventory canvas cam missing, finding new one...");
                _canvas.worldCamera = GameObject.FindWithTag("OrthoCam").GetComponent<Camera>();
            }

            _inventoryService.ArtifactAdded += OnArtifactAdded;
            _inventoryService.ArtifactRemoved += OnArtifactRemoved;
        }

        private void OnArtifactRemoved(ArtifactType type)
        {
            _artifactsSlots.FirstOrDefault(x => x.RelatedArtifactType == type)?.RemoveArtifact();
        }

        private void OnArtifactAdded(ArtifactType type)
        {
            _artifactsSlots.FirstOrDefault(x=>x.IsEmpty)?.SetArtifact(type);
        }
    }
}
