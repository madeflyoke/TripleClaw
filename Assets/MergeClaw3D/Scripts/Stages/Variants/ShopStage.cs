using MergeClaw3D.Scripts.Configs.Stages.Data;
using MergeClaw3D.Scripts.Currency;
using MergeClaw3D.Scripts.Currency.StageHandlers;
using MergeClaw3D.Scripts.Events.Models;
using MergeClaw3D.Scripts.Shop;
using MergeClaw3D.Scripts.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Stages.Variants
{
    public class ShopStage : MonoBehaviour, IStage
    {
        public StageData StageData { get; private set; }
        
        [SerializeField] private ArtifactsShopController _shopController; 
        
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void Initialize(StageData stageData)
        {
            StageData = stageData;
            var shopStageData = stageData as ShopStageData;
            _shopController.Initialize();
            
            InitCallbacks();
        }

        private void InitCallbacks()
        {
            MessageBroker.Default.Receive<ItemsEventsModels.AllItemsMerged>()
                .Subscribe(_=>_signalBus.Fire<StageCompletedSignal>())
                .AddTo(this);
            
            MessageBroker.Default.Receive<ItemsEventsModels.AllPlacesOccupied>()
                .Subscribe(_=>
                {
                    _signalBus.Fire<StageFailedSignal>();
                    Debug.LogWarning("fail");
                })
                .AddTo(this);
        }
    }
}