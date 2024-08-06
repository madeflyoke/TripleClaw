using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using MergeClaw3D.Scripts.Configs.Artifacts;
using MergeClaw3D.Scripts.Events.Models;
using MergeClaw3D.Scripts.Inventory.Enum;
using MergeClaw3D.Scripts.Services;
using MergeClaw3D.Scripts.Services.Progress.Inventory;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Inventory.UI
{
    public class InventoryUI : MonoBehaviour
    {
        public event Action ArtifactShowed;
        
        [SerializeField] private Canvas _canvas;
        [SerializeField] private List<InventoryArtifactSlotUI> _artifactsSlots;
        [SerializeField] private ArtifactPreview _artifactPreview;
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

            var artifacts = _inventoryService.GetExistsArtifacts().ToList();
            for (int i = 0; i < artifacts.Count; i++)
            {
                _artifactsSlots[i].SetArtifact(artifacts[i]);
            }
            
            MessageBroker.Default.Receive<ArtifactsEventsModels.ArtifactApplied>().Subscribe(OnArtifactApplied)
                .AddTo(this);
        }
        
        private void OnArtifactApplied(ArtifactsEventsModels.ArtifactApplied eventData)
        {
            var relatedSlot = _artifactsSlots.FirstOrDefault(x => x.RelatedArtifact.ArtifactType == eventData.Type);
            if (relatedSlot != null)
            {
                _artifactPreview.PreviewArtifact(relatedSlot.RelatedArtifact, ()=>
                {
                    ArtifactShowed?.Invoke();
                    relatedSlot.RemoveArtifact();
                });
            }
        }

        private void OnArtifactRemoved(ArtifactType type)
        {
            _artifactsSlots.FirstOrDefault(x => x.RelatedArtifact.ArtifactType == type)?.RemoveArtifact();
        }

        private void OnArtifactAdded(ArtifactType type)
        {
            _artifactsSlots.FirstOrDefault(x=>x.IsEmpty)?.SetArtifact(type);
        }
    }
}
