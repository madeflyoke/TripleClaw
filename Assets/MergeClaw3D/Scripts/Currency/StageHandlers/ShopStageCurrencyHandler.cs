using MergeClaw3D.Scripts.Configs.Stages.Data;
using MergeClaw3D.Scripts.Services;
using MergeClaw3D.Scripts.Services.Progress.Currency;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Currency.StageHandlers
{
    public class ShopStageCurrencyHandler : MonoBehaviour
    {
        private CurrencyService _currencyService;

        public void Initialize(ShopStageData stageData)
        { 
           
        }

        [Inject]
        public void Construct(ServicesHolder servicesHolder)
        {
            _currencyService = servicesHolder.GetService<CurrencyService>();
        }
    }
}
