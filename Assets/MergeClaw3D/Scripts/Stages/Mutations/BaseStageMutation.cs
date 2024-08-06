using System;
using MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Mutations;
using MergeClaw3D.Scripts.Events.Models;
using MergeClaw3D.Scripts.Inventory.Enum;
using MergeClaw3D.Scripts.Inventory.UI;
using MergeClaw3D.Scripts.Services;
using MergeClaw3D.Scripts.Services.Progress.Inventory;
using UniRx;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Stages.Mutations
{
    public abstract class BaseStageMutation : MonoBehaviour
    {
        [field: SerializeField] public ArtifactType ResolveArtifactType { get; protected set; }
        protected InventoryService InventoryService;
        protected InventoryUI InventoryUI;
        
        [Inject]
        public void Construct(ServicesHolder servicesHolder)
        {
            InventoryService = servicesHolder.GetService<InventoryService>();
        }

        public virtual void Initialize(IBaseStageMutationDataModule dataModule)
        {
            InventoryUI = FindObjectOfType<InventoryUI>();
            InventoryUI.ArtifactShowed += RevertMutation;
            
            ApplyMutation();
            if (InventoryService.ContainsArtifact(ResolveArtifactType))
            {
                MessageBroker.Default.Publish(new ArtifactsEventsModels.ArtifactApplied(ResolveArtifactType));
            }   
        }

        protected abstract void ApplyMutation();
        protected abstract void RevertMutation();

        protected virtual void OnDisable()
        {
            InventoryUI.ArtifactShowed -= RevertMutation;
        }
    }
}
