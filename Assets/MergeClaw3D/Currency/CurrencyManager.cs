using System;
using System.Collections.Generic;
using MergeClaw3D.MergeClaw3D.Currency.Enums;
using MergeClaw3D.MergeClaw3D.Progress;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

namespace MergeClaw3D.MergeClaw3D.Currency
{
    public class CurrencyManager : MonoBehaviour
    {
        public event Action<CurrencyType, long> CurrencyChanged;

        [SerializeField] private SerializedDictionary<CurrencyType, CurrencyView> _currencyViewMap;
        private Dictionary<CurrencyType, long> _currencyValueMap;

        private void Awake()
        {
            InitializeCurrency();
        }

        [Button]
        public void AddCurrency(CurrencyType type, long value, bool withSave)
        {
            var currentValue = _currencyValueMap[type];
            var finalValue = Math.Clamp(currentValue + value, 0, long.MaxValue);

            _currencyValueMap[type] = finalValue;
            CurrencyChanged?.Invoke(type, finalValue);

            if (withSave)
            {
                ProgressService.S.CurrencyProgressHandler.SaveCurrency(type, finalValue);
            }
            
            _currencyViewMap[type].SetValueText(finalValue);
        }
        
        private void InitializeCurrency()
        {
            _currencyValueMap = new Dictionary<CurrencyType, long>();
            
            foreach (var kvp in _currencyViewMap)
            {
                var extractedValue =ProgressService.S.CurrencyProgressHandler.ExtractCurrency(kvp.Key);
                _currencyValueMap.Add(kvp.Key, extractedValue);
                kvp.Value.SetValueText(extractedValue);
            }  
        }

        private void OnDestroy()
        {
            ProgressService.S.CurrencyProgressHandler.SaveCurrency(_currencyValueMap);
        }
    }
}
