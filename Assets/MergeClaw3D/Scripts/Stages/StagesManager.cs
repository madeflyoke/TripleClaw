using System;
using Cysharp.Threading.Tasks;
using MergeClaw3D.Scripts.Configs.Stages;
using MergeClaw3D.Scripts.Events;
using MergeClaw3D.Scripts.Stages.Variants;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MergeClaw3D.Scripts.Stages
{
    public class StagesManager : MonoBehaviour
    {
        private IStage _currentStage;
        
        private SignalBus _signalBus;
        private StagesConfig _stagesConfig;

        [Inject]
        public void Construct(SignalBus signalBus, StagesConfig stagesConfig)
        {
            _signalBus = signalBus;
            _stagesConfig = stagesConfig;
            MessageBroker.Default.Publish(AllItemsMerged.Create());

        }
        
        [Button]
        public void Initialize()
        {
            MessageBroker.Default.Receive<AllItemsMerged>()
                .Subscribe(x=>SetNextStage())
                .AddTo(this);
            
            SetNextStage();
        }

        public async void SetNextStage()
        {
            var stageData = _stagesConfig.GetStageData(_currentStage==null?0:_currentStage.StageData.Id+1); //TODO maybe save? like last unfinished level restart
            _currentStage = null;
            await SceneManager.LoadSceneAsync(stageData.SceneName).ToUniTask();
            _currentStage = GameObject.FindGameObjectWithTag("Stage").GetComponent<IStage>();
            _currentStage.Initialize(stageData);
        }
    }
}
