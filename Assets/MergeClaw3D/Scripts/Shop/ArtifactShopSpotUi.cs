using System;
using System.Collections.Generic;
using System.Linq;
using MergeClaw3D.Scripts.Configs.Artifacts;
using MergeClaw3D.Scripts.Configs.Currency;
using MergeClaw3D.Scripts.Currency.Enums;
using MergeClaw3D.Scripts.Services;
using MergeClaw3D.Scripts.Services.Progress.Currency;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MergeClaw3D.Scripts.Shop
{
    public class ArtifactShopSpotUi : MonoBehaviour
    {
        public event Action ArtifactBought;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private CurrencyConfig _currencyConfig;
        [SerializeField] private Button _buyButton;
        [SerializeField] private List<ArtifactShopUiPrice> _prices;
        [SerializeField] private TMP_Text _soldText;
        private CurrencyService _currencyService;

        [Inject]
        public void Construct(ServicesHolder servicesHolder)
        {
            _currencyService = servicesHolder.GetService<CurrencyService>();
        }
        
        public void Initialize(ArtifactData artifactData)
        {
            _canvas.worldCamera = Camera.main;
            _buyButton.onClick.AddListener(OnBuyClick);

            _prices.ForEach(x=>x.Disable());
            _soldText.gameObject.SetActive(false);

            
            var pricesData = artifactData.BasePricesMap.ToList();
            for (int i = 0; i < pricesData.Count; i++)
            {
                var data = pricesData[i];
                _prices[i].Initialize(data, _currencyConfig.GetCurrencySprite(data.Key));
                _prices[i].Enable();
            }

            UpdateButtonState(default, default);
            _currencyService.CurrencyChanged += UpdateButtonState;
        }

        private void OnBuyClick()
        {
            Disable();
            _soldText.gameObject.SetActive(true);
            ArtifactBought?.Invoke();
        }

        private void UpdateButtonState(CurrencyType _, long __)
        {
            _buyButton.interactable = true;
            
            foreach (var price in _prices.Where(x=>x.Enabled))
            {
                if (price.IsEnoughCurrency==false)
                {
                    _buyButton.interactable = false;
                    return;
                }
            }
        }
        
        private void Disable()
        {
            _buyButton.onClick.RemoveAllListeners();
            _buyButton.interactable = false;
            _currencyService.CurrencyChanged -= UpdateButtonState;
            _prices.ForEach(x => x.Disable());
        }

        private void OnDisable()
        {
            Disable();
        }
        
        
#if UNITY_EDITOR

        [Button]
        private void SetPricesFromChildren()
        {
            _prices = _buyButton.GetComponentsInChildren<ArtifactShopUiPrice>(true).ToList();
        }

#endif
    }
}
