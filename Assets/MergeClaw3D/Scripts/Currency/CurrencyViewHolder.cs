using System;
using MergeClaw3D.Scripts.Currency.Enums;
using MergeClaw3D.Scripts.Services;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace MergeClaw3D.Scripts.Currency
{
    public class CurrencyViewHolder : MonoBehaviour
    {
        private CurrencyService _currencyService;
        
        [SerializeField] private SerializedDictionary<CurrencyType, CurrencyView> _currencyViewMap;

        [Inject]
        public void Construct(ServicesHolder servicesHolder)
        {
            _currencyService = servicesHolder.GetService<CurrencyService>();
        }
        
        private void OnEnable()
        {
            ExtractValues();
            _currencyService.CurrencyChanged += RefreshView;
        }

        private void OnDisable()
        {
            _currencyService.CurrencyChanged -= RefreshView;
        }

        private void RefreshView(CurrencyType currencyType, long value)
        {
            if (_currencyViewMap.ContainsKey(currencyType))
            {
                _currencyViewMap[currencyType].SetValueText(value);
            }
        }

        private void ExtractValues()
        {
            foreach (var kvp in _currencyViewMap)
            {
                kvp.Value.SetValueText(_currencyService.GetCurrency(kvp.Key));
            }
        }
    }
}
