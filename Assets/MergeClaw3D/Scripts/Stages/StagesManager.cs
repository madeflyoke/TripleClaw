using System;
using MergeClaw3D.Scripts.Configs.Stages;
using MergeClaw3D.Scripts.Signals;
using MergeClaw3D.Scripts.Spawner;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Stages
{
    public class StagesManager : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;

        [SerializeField] private StagesConfig _stagesConfig;
        
        private Stage _currentStage;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            var stageData =  _stagesConfig.GetStageData(0);
            _currentStage = new Stage(stageData);
            _signalBus.Fire(new StageStartedSignal(stageData));
        }
    }
}
