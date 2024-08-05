using Cysharp.Threading.Tasks;
using MergeClaw3D.Scripts.Configs.Stages;
using MergeClaw3D.Scripts.Signals;
using MergeClaw3D.Scripts.Stages.Variants;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MergeClaw3D.Scripts.Stages
{
    public class StagesManager : MonoBehaviour
    {
        public int CurrentStageGlobalIndex { get; private set; } = -1;
        
        private IStage _currentStage;
        
        private SignalBus _signalBus;
        private StagesConfig _stagesConfig;

        [Inject]
        public void Construct(SignalBus signalBus, StagesConfig stagesConfig)
        {
            _signalBus = signalBus;
            _stagesConfig = stagesConfig;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<NextStageCallSignal>(SetNextStage);
            SetNextStage();
        }
        
        private async void SetNextStage()
        {
            var stageData = _stagesConfig.GetStageData(_currentStage==null?0:_currentStage.StageData.Id+1); //TODO maybe save? like last unfinished level restart
            CurrentStageGlobalIndex++;
            _currentStage = null;
            await SceneManager.LoadSceneAsync(stageData.SceneName).ToUniTask();
            _currentStage = GameObject.FindGameObjectWithTag("Stage").GetComponent<IStage>();
            _currentStage.Initialize(stageData);
            
            _signalBus.Fire(new StageStartedSignal(stageData));
        }
    }
}
