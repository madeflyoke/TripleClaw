using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using MergeClaw3D.Scripts.Currency.Enums;
using MergeClaw3D.Scripts.Services.Interfaces;
using MergeClaw3D.Scripts.Services.Progress;
using Sirenix.OdinInspector;
using Zenject;

namespace MergeClaw3D.Scripts.Services
{
    public class CurrencyService : IService
    {
        public event Action<CurrencyType, long> CurrencyChanged;

        private readonly List<CurrencyType> _supportedCurrencies = new()
        {
            CurrencyType.COIN
        };

        private Dictionary<CurrencyType, long> _currencyValueMap;
        private ProgressService _progressService;

        [Inject]
        public void Construct(ServicesHolder servicesHolder)
        {
            _progressService = servicesHolder.GetService<ProgressService>();
        }
        
        public UniTask Initialize(CancellationTokenSource cts)
        {
            InitializeCurrency();
            return UniTask.CompletedTask;
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
        
        public void Dispose()
        {
            _progressService?.CurrencyProgressHandler.SaveCurrency(_currencyValueMap);
        }
    }
}
