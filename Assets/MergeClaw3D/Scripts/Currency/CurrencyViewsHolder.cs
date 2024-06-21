using System;
using MergeClaw3D.Scripts.Currency.Enums;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace MergeClaw3D.Scripts.Currency
{
    public class CurrencyViewsHolder : MonoBehaviour
    {
        [Inject] private CurrencyManager _currencyManager;
        
        [SerializeField] private SerializedDictionary<CurrencyType, CurrencyView> _currencyViewMap;

        private void OnEnable()
        {
            ExtractValues();
            _currencyManager.CurrencyChanged += RefreshView;
        }

        private void OnDisable()
        {
            _currencyManager.CurrencyChanged -= RefreshView;
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
                kvp.Value.SetValueText(_currencyManager.GetCurrency(kvp.Key));
            }
        }
    }
}
