using System;
using MergeClaw3D.Scripts.Configs.Stages.Data;
using MergeClaw3D.Scripts.Configs.Stages.Data.Modules;
using MergeClaw3D.Scripts.Events.Models;
using MergeClaw3D.Scripts.Signals;
using UniRx;
using UnityEngine;

namespace MergeClaw3D.Scripts.UI.Screens.Gameplay
{
    public class ItemsStageGameplayScreen : GameplayScreen
    {
     //   [SerializeField] private LevelCompletePopup _levelCompletePopup;
        [SerializeField] private Timer _timer;
        private TimeLimitDataModule _timeLimitDataModule;
        
        protected override void Initialize()
        {
            base.Initialize();
            SignalBus.Subscribe<StageCompletedSignal>(OnStageCompleted);
            MessageBroker.Default.Receive<ItemsEventsModels.ItemsSpawned>()
                .Subscribe(_=>OnItemsSpawned()).AddTo(this);
            //       _levelCompletePopup.Hide();
        }

        protected override void OnStageStarted(StageStartedSignal signal)
        {
            base.OnStageStarted(signal);
            _timeLimitDataModule = signal.StageData.GetModule<TimeLimitDataModule>();
            _timer.Initialize(TimeSpan.FromSeconds(_timeLimitDataModule.SecondsTimeLimit));
        }

        private void OnItemsSpawned()
        {
            _timer.StartTimer();
        }

        private void OnStageCompleted(StageCompletedSignal signal)
        {
            _timer.StopTimer();
            ShowLevelCompletePopup(signal);
        }
        
        private void ShowLevelCompletePopup(StageCompletedSignal signal)
        {
    //        _levelCompletePopup.Show(signal);
        }
    }
}
