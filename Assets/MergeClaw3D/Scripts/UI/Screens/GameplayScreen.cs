using MergeClaw3D.Scripts.Signals;
using MergeClaw3D.Scripts.UI.Screens.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MergeClaw3D.Scripts.UI.Screens
{
    public class GameplayScreen : MonoBehaviour, IScreen
    {
        [Inject] private SignalBus _signalBus;
        
        [SerializeField] private Button _pauseButton;
        [SerializeField] private SettingsPopup _settingsPopup;
        [SerializeField] private LevelCompletePopup _levelCompletePopup;

        private void Awake()
        {
            _signalBus.Subscribe<StageCompletedSignal>(OnLevelCompleted);
        }

        private void Start()
        {
            _settingsPopup.Hide();
            _levelCompletePopup.Hide();
        }

        private void OnLevelCompleted(StageCompletedSignal signal)
        {
            ShowLevelCompletePopup(signal);
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
            _settingsPopup.Show();
        }

        private void ShowLevelCompletePopup(StageCompletedSignal signal)
        {
            _levelCompletePopup.Show(signal);
        }
    }
}
