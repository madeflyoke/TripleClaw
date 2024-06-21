using System;
using System.Collections.Generic;
using System.Linq;
using MergeClaw3D.Scripts.Currency.Enums;
using MergeClaw3D.Scripts.Services;
using MergeClaw3D.Scripts.Services.Progress;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Currency
{
    public class CurrencyManager : MonoBehaviour
    {
        [Inject] private ServicesHolder _servicesHolder;
        
        public event Action<CurrencyType, long> CurrencyChanged;

        [SerializeField] private List<CurrencyType> _supportedCurrencies;

        private Dictionary<CurrencyType, long> _currencyValueMap;
        private ProgressService _progressService;

        private void Awake()
        {
            _progressService = _servicesHolder.GetService<ProgressService>();
            InitializeCurrency();
        }

        /// <summary>
        /// Negative value including
        /// </summary>
        [Button]
        public void AddCurrency(CurrencyType type, long value, bool withSave)
        {
            var currentValue = _currencyValueMap[type];
            var finalValue = Math.Clamp(currentValue + value, 0, long.MaxValue);

            _currencyValueMap[type] = finalValue;

            if (withSave)
            {
                _progressService.CurrencyProgressHandler.SaveCurrency(type, finalValue);
            }
            
            CurrencyChanged?.Invoke(type, finalValue);
        }

        public long GetCurrency(CurrencyType type)
        {
            return _currencyValueMap[type];
        }
        
        private void InitializeCurrency()
        {
            _currencyValueMap = new Dictionary<CurrencyType, long>();
            
            foreach (var item in _supportedCurrencies)
            {
                var extractedValue =_progressService.CurrencyProgressHandler.ExtractCurrency(item);
                _currencyValueMap.Add(item, extractedValue);
            }  
        }

        private void OnDestroy()
        {
            _progressService?.CurrencyProgressHandler.SaveCurrency(_currencyValueMap);
        }
        
#if UNITY_EDITOR

        private void OnValidate()
        {
            if (_supportedCurrencies.Distinct().Count() != _supportedCurrencies.Count)
            {
                Debug.LogError("Currency Manager: duplicate currency was found!");
            }
        }

#endif
    }
}
