using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using MergeClaw3D.Scripts.Currency.Enums;
using MergeClaw3D.Scripts.Services.Interfaces;

namespace MergeClaw3D.Scripts.Services.Progress.Currency
{
    public class CurrencyService : IService
    {
        public event Action<CurrencyType, long> CurrencyChanged;

        private readonly List<CurrencyType> _supportedCurrencies = new()
        {
            CurrencyType.COIN
        };

        private Dictionary<CurrencyType, long> _currencyValueMap;
        private CurrencyProgressHandler _currencyProgressHandler;
        
        public UniTask Initialize(CancellationTokenSource cts)
        {
            _currencyProgressHandler = new CurrencyProgressHandler();
            InitializeCurrency();
            return UniTask.CompletedTask;
        }

        private void InitializeCurrency()
        {
            _currencyValueMap = new Dictionary<CurrencyType, long>();
            
            foreach (var item in _supportedCurrencies)
            {
                var extractedValue =_currencyProgressHandler.ExtractCurrency(item);
                _currencyValueMap.Add(item, extractedValue);
            }  
        }
        
        public void AddCurrency(CurrencyType type, long value, bool withSave)
        {
            var currentValue = _currencyValueMap[type];
            var finalValue = Math.Clamp(currentValue + value, 0, long.MaxValue);

            _currencyValueMap[type] = finalValue;

            if (withSave)
            {
                _currencyProgressHandler.SaveCurrency(type, finalValue);
            }
            
            CurrencyChanged?.Invoke(type, finalValue);
        }

        public void RemoveCurrency(CurrencyType type, long value, bool withSave)
        {
            AddCurrency(type, -value, withSave);
        }
        
        public void AddCurrencies(Dictionary<CurrencyType, long> value, bool withSave)
        {
            foreach (var kvp in value)
            {
                AddCurrency(kvp.Key, kvp.Value, withSave);
            }
        }

        public void RemoveCurrencies(Dictionary<CurrencyType, long> value, bool withSave)
        {
            foreach (var kvp in value)
            {
                RemoveCurrency(kvp.Key, kvp.Value, withSave);
            }
        }

        public long GetCurrency(CurrencyType type)
        {
            return _currencyValueMap[type];
        }
        
        public void Dispose()
        {
            _currencyProgressHandler?.SaveCurrency(_currencyValueMap);
        }
    }
}
