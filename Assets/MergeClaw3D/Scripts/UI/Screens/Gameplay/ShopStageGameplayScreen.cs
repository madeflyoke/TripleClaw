using MergeClaw3D.Scripts.Signals;

namespace MergeClaw3D.Scripts.UI.Screens.Gameplay
{
    public class ShopStageGameplayScreen : GameplayScreen
    {
        //   [SerializeField] private LevelCompletePopup _levelCompletePopup;
        
        protected override void Initialize()
        {
            base.Initialize();
            SignalBus.Subscribe<StageCompletedSignal>(OnStageCompleted);
            //       _levelCompletePopup.Hide();
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
