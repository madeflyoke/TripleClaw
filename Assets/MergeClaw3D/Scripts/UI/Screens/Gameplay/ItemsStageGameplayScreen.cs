using System;
using MergeClaw3D.Scripts.Configs.Stages.Data;
using MergeClaw3D.Scripts.Configs.Stages.Data.Modules;
using MergeClaw3D.Scripts.Events.Models;
using MergeClaw3D.Scripts.Signals;
using MergeClaw3D.Scripts.Stages;
using UniRx;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.UI.Screens.Gameplay
{
    public class ItemsStageGameplayScreen : GameplayScreen
    {
     //   [SerializeField] private LevelCompletePopup _levelCompletePopup;
        [SerializeField] private CustomTimer _customTimer;
        private TimeLimitDataModule _timeLimitDataModule;

        public override void Construct(SignalBus signalBus)
        {
            base.Construct(signalBus);
            SignalBus.Subscribe<StageCompletedSignal>(OnStageCompleted);
        }
        
        protected override void OnStageStarted(StageStartedSignal signal)
        {
            base.OnStageStarted(signal);
            
            MessageBroker.Default.Receive<ItemsEventsModels.ItemsSpawned>()
                .Subscribe(_=>OnItemsSpawned()).AddTo(this);
            //       _levelCompletePopup.Hide();

            _timeLimitDataModule = signal.StageData.GetModule<TimeLimitDataModule>();
            _customTimer.Initialize(TimeSpan.FromSeconds(_timeLimitDataModule.SecondsTimeLimit));
        }

        private void OnItemsSpawned()
        {
            _customTimer.StartTimer();
        }

        private void OnStageCompleted(StageCompletedSignal signal)
        {
            _customTimer.StopTimer();
            ShowLevelCompletePopup(signal);
        }
        
        private void ShowLevelCompletePopup(StageCompletedSignal signal)
        {
    //        _levelCompletePopup.Show(signal);
        }
    }
}
