using MergeClaw3D.Scripts.Signals;

namespace MergeClaw3D.Scripts.UI.Screens.Gameplay
{
    public class ShopStageGameplayScreen : GameplayScreen
    {
        //   [SerializeField] private LevelCompletePopup _levelCompletePopup;
        

        protected override void OnStageStarted(StageStartedSignal signal)
        {
            base.OnStageStarted(signal);
            //       _levelCompletePopup.Hide();
            SignalBus.Subscribe<StageCompletedSignal>(OnStageCompleted);
        }

        private void OnStageCompleted(StageCompletedSignal signal)
        {
            ShowLevelCompletePopup(signal);
        }
        
        private void ShowLevelCompletePopup(StageCompletedSignal signal)
        {
            //        _levelCompletePopup.Show(signal);
        }
    }
}
