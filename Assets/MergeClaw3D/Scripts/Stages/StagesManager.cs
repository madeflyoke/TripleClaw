using MergeClaw3D.Scripts.Configs.Stages;
using MergeClaw3D.Scripts.Signals;
using MergeClaw3D.Scripts.Spawner;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Stages
{
    public class StagesManager : MonoBehaviour
    {
        private Stage _currentStage;
        private SignalBus _signalBus;
        private StagesConfig _stagesConfig;
        private ItemsSpawner _itemsSpawner;

        [Inject]
        public void Construct(SignalBus signalBus, ItemsSpawner itemsSpawner, StagesConfig stagesConfig)
        {
            _signalBus = signalBus;
            _itemsSpawner = itemsSpawner;
            _stagesConfig = stagesConfig;
        }

        private void Start()
        {
            StartStage(0);
        }

        // public void Initialize()
        // {
        //     var stageData =  _stagesConfig.GetStageData(0);
        //     _currentStage = new Stage(stageData);
        //     _signalBus.Fire(new StageStartedSignal(stageData));
        //     _itemsSpawner.Spawn(stageData);
        // }

        [Button]
        public void StartStage(int index)
        {
            var stageData =  _stagesConfig.GetStageData(index);
            _currentStage = new Stage(stageData);
            _signalBus.Fire(new StageStartedSignal(stageData));
            _itemsSpawner.Spawn(stageData);
        }
    }
}
