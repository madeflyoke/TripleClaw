using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MergeClaw3D
{
    public class InputService : MonoBehaviour
    {
        public static InputService S; //TODO Hahan't
        
        public event Action PointerInputDown;
        public event Action PointerInputUp;
        
        private IDisposable _pointerDownTracker;
        private IDisposable _pointerUpTracker;
            
        private void Awake()
        {
            S = this;
            InitializePointerInput();
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
            }).AddTo(this);
            _pointerUpTracker = Observable.EveryUpdate().Where(_ => IsPointerUp()).Subscribe(_ =>
            {
                PointerInputUp?.Invoke();
            }).AddTo(this);
        }
    }
}
