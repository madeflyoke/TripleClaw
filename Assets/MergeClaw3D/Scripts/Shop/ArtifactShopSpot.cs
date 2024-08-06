using MergeClaw3D.Scripts.Configs.Artifacts;
using MergeClaw3D.Scripts.Currency.Enums;
using MergeClaw3D.Scripts.Services;
using MergeClaw3D.Scripts.Services.Progress.Currency;
using MergeClaw3D.Scripts.Services.Progress.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Shop
{
    public class ArtifactShopSpot : MonoBehaviour
    {
        [SerializeField] private Transform _artifactParent;
        [SerializeField] private ArtifactShopSpotUi _spotUi;
        private ArtifactData _relatedArtifactData;
        
        private CurrencyService _currencyService;
        private InventoryService _inventoryService;

        [Button]
        public void AddGold(long value)
        {
            _currencyService.AddCurrency(CurrencyType.COIN, value, true);
        }

        [Button]
        public void RemoveGold(long value)
        {
            _currencyService.RemoveCurrency(CurrencyType.COIN, value, true);
        }
        
        [Inject]
        public void Construct(ServicesHolder servicesHolder)
        {
            _inventoryService = servicesHolder.GetService<InventoryService>();
            _currencyService = servicesHolder.GetService<CurrencyService>();
        }
        
        public void Initialize(ArtifactData artifactData)
        {
            _relatedArtifactData = artifactData;
            _spotUi.Initialize(artifactData);
            _spotUi.ArtifactBought += OnArtifactBought;
            SpawnArtifact(artifactData);
        }
        
        private void SpawnArtifact(ArtifactData artifactData)
        {
            _relatedArtifactData = artifactData;
            Instantiate(artifactData.ArtifactOriginal as MonoBehaviour, _artifactParent);
        }
        
        private void OnArtifactBought()
        {
            _currencyService.RemoveCurrencies(_relatedArtifactData.BasePricesMap, true);
            _inventoryService.AddArtifact(_relatedArtifactData.Type);
        }
    }
}
