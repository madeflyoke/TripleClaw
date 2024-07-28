using System.Collections.Generic;
using MergeClaw3D.Scripts.Configs.Stages.Data;
using MergeClaw3D.Scripts.Currency.Enums;
using MergeClaw3D.Scripts.Events.Models;
using MergeClaw3D.Scripts.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Currency
{
    public class CurrencyHandler : MonoBehaviour
    {
        private CurrencyService _currencyService;
        private Dictionary<CurrencyType, int> _currenciesPerMerge;

        public void Initialize(ItemsStageData stageData)
        {
            _currenciesPerMerge = stageData.CurrencyPerMerge;
            MessageBroker.Default.Receive<ItemsEventsModels.ItemsMerged>()
                .Subscribe(_ => OnItemsMerged()).AddTo(this);
        }

        [Inject]
        public void Construct(ServicesHolder servicesHolder)
        {
            _currencyService = servicesHolder.GetService<CurrencyService>();
        }

        private void OnItemsMerged()
        {
            foreach (var kvp in _currenciesPerMerge)
            {
                _currencyService.AddCurrency(kvp.Key, kvp.Value, true);
            }
        }
    }
}
