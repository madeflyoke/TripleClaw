using MergeClaw3D.Scripts.Configs.Stages.Data;
using MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Mutations;
using MergeClaw3D.Scripts.Events;
using MergeClaw3D.Scripts.Events.Models;
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
            _mutationsHandler.Initialize(stageData);
            _itemsSpawner.Spawn(stageData as ItemsStageData);
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