using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MergeClaw3D.Scripts.Services.Interfaces;
using UniRx;
using UnityEngine;

namespace MergeClaw3D.Scripts.Services
{
    public class InputService : IService
    {
        public event Action PointerInputDown;
        public event Action PointerInputUp;
        
        private IDisposable _pointerDownTracker;
        private IDisposable _pointerUpTracker;
        
        public UniTask Initialize(CancellationTokenSource cts)
        {
            InitializePointerInput();
            return UniTask.CompletedTask;
        }

        private void InitializePointerInput()
        {
            bool IsPointerDown()
            {
                if (Input.touchSupported)
                    return Input.touchCount > 0 && Input.GetTouch(0).phase==TouchPhase.Began;
                return Input.GetKeyDown(KeyCode.Mouse0);
            }
          
            bool IsPointerUp()
            {
                if (Input.touchSupported)
                    return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
                return Input.GetKeyUp(KeyCode.Mouse0);
            }
            
            _pointerDownTracker = Observable.EveryUpdate().Where(_ => IsPointerDown()).Subscribe(_ =>
            {
                PointerInputDown?.Invoke();
            });
            _pointerUpTracker = Observable.EveryUpdate().Where(_ => IsPointerUp()).Subscribe(_ =>
            {
                PointerInputUp?.Invoke();
            });
        }


        public void Dispose()
        {
            _pointerDownTracker?.Dispose();
            _pointerUpTracker?.Dispose();
        }
    }
}
