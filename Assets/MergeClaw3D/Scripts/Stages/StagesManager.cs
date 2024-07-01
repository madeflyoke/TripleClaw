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
        [Inject] private SignalBus _signalBus;

        [SerializeField] private StagesConfig _stagesConfig;
        [SerializeField] private ItemsSpawner _itemsSpawner;
        
        private Stage _currentStage;
        
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
