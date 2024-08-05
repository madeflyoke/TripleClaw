using MergeClaw3D.Scripts.Signals;
using MergeClaw3D.Scripts.UI.Screens.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MergeClaw3D.Scripts.UI.Screens.Gameplay
{
    public abstract class GameplayScreen : MonoBehaviour, IScreen
    {
        protected SignalBus SignalBus;
        
        [SerializeField] private Button _pauseButton;
        //      [SerializeField] private SettingsPopup _settingsPopup;
  
        [Inject]
        public virtual void Construct(SignalBus signalBus)
        {
            SignalBus = signalBus;
            SignalBus.Subscribe<StageStartedSignal>(OnStageStarted);
        }
        
        protected virtual void OnStageStarted(StageStartedSignal signal)
        {
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _pauseButton.onClick.AddListener(ShowPausePopup);
        }
        public void Hide()
        {
            _pauseButton.onClick.RemoveListener(ShowPausePopup);
            gameObject.SetActive(false);
        }
        
        private void ShowPausePopup()
        {
   //         _settingsPopup.Show();
        }
    }
}
