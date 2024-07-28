using MergeClaw3D.Scripts.Configs.Stages.Data;
using MergeClaw3D.Scripts.Currency;
using MergeClaw3D.Scripts.Events.Models;
using MergeClaw3D.Scripts.Services;
using MergeClaw3D.Scripts.Signals;
using MergeClaw3D.Scripts.Spawner;
using MergeClaw3D.Scripts.Stages.Mutations;
using UniRx;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Stages.Variants
{
    public class ItemsStage : MonoBehaviour, IStage
    {
        public StageData StageData { get; private set; }

        [SerializeField] private MutationsHandler _mutationsHandler;
        [SerializeField] private CurrencyHandler _currencyHandler;
        
        private ItemsSpawner _itemsSpawner;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(ItemsSpawner itemsSpawner, SignalBus signalBus)
        {
            _itemsSpawner = itemsSpawner;
            _signalBus = signalBus;
        }
        
        public void Initialize(StageData stageData)
        {
            StageData = stageData;
            var itemsStageData = stageData as ItemsStageData;
            
            _mutationsHandler.Initialize(stageData);
            _currencyHandler.Initialize(itemsStageData);
            _itemsSpawner.Spawn(itemsStageData);
            
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