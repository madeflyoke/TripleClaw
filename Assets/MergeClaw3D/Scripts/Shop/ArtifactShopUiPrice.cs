using System.Collections.Generic;
using MergeClaw3D.Scripts.Currency.Enums;
using MergeClaw3D.Scripts.Services;
using MergeClaw3D.Scripts.Services.Progress.Currency;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MergeClaw3D.Scripts.Shop
{
    public class ArtifactShopUiPrice : MonoBehaviour
    {
        public bool IsEnoughCurrency => _currencyService.GetCurrency(_relatedPriceKvp.Key) >= _relatedPriceKvp.Value;

        public bool Enabled { get; private set; }
        
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Image _icon;
        [SerializeField] private Color _disabledPriceColor;
        private Color _defaultPriceColor;
        private CurrencyService _currencyService;
        private KeyValuePair<CurrencyType, long> _relatedPriceKvp;

        [Inject]
        public void Construct(ServicesHolder servicesHolder)
        {
            _currencyService = servicesHolder.GetService<CurrencyService>();
        }
        
        private void Awake()
        {
            _defaultPriceColor = _priceText.color;
        }

        public void Initialize(KeyValuePair<CurrencyType, long>  priceKvp, Sprite icon)
        {
            _relatedPriceKvp = priceKvp;
            _priceText.text = priceKvp.Value.ToString();
            _icon.sprite = icon;
            UpdateTextState(default, default);
        }

        public void Enable()
        {
            _currencyService.CurrencyChanged += UpdateTextState;
            gameObject.SetActive(true);
            Enabled = true;
        }
        
        public void Disable()
        {
            _currencyService.CurrencyChanged -= UpdateTextState;
            gameObject.SetActive(false);
            Enabled = false;
        }

        private void UpdateTextState(CurrencyType _, long __)
        {
            _priceText.color = IsEnoughCurrency ? _defaultPriceColor : _disabledPriceColor;
        }

        private void OnDisable()
        {
            _currencyService.CurrencyChanged -= UpdateTextState;
        }
    }
}
