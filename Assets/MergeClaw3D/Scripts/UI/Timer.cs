using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace MergeClaw3D.Scripts.UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;
        private TimeSpan _duration;
        private IDisposable _disposable;

        public void Initialize(TimeSpan duration)
        {
            _duration = duration;
            RefreshView();
        }

        public void StartTimer()
        {
            _disposable = Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ => UpdateTimer())
                .AddTo(this);
        }

        public void StopTimer()
        {
            _disposable?.Dispose();
        }

        private void UpdateTimer()
        {
            _duration -= TimeSpan.FromSeconds(1);
            RefreshView();
            
            if (_duration.TotalSeconds<=0)
            {
                OnFinish();
                StopTimer();
            }
        }

        private void OnFinish()
        {
            Debug.LogWarning("Finish");
        }

        private void RefreshView()
        {
            _timerText.text = _duration.ToString(@"mm\:ss");
        }
    }
}