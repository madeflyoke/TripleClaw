using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.NPC
{
    public class PointsMovementComponent : MonoBehaviour
    {
        [BoxGroup("Stats")]
        [SerializeField] private float _speed;
        
        [SerializeField] private Transform _originalPoint;
        [SerializeField] private List<Transform> _destinationPoints;
        private int _currentPointIndex;
        private Tween _tween;
        private CancellationTokenSource _cts;
        
        public void Initialize()
        {
            transform.position = _originalPoint.position;
        }

        public bool TryToMoveToNextAvailablePoint(Action onComplete=null, bool lookAt = false)
        {
            _tween?.Kill();
            if (_currentPointIndex>=_destinationPoints.Count)
            {
                Debug.LogWarning("false");
                _cts?.Cancel();
                return false;
            }

            var point = _destinationPoints[_currentPointIndex].position;

            if (lookAt)
            {
                transform.LookAt(new Vector3(point.x, transform.position.y, point.z));
            }
            
            _tween = transform.DOMove(point, _speed).SetSpeedBased(true)
                .OnComplete(() => onComplete?.Invoke()).SetEase(Ease.Linear);
            _currentPointIndex++;
            return true;
        }

        public async void MoveSequenceToLeftPoints(Action onComplete=null, bool lookAt = false)
        {
            _cts = new CancellationTokenSource();
            while (true)
            {
                if (TryToMoveToNextAvailablePoint(lookAt:lookAt))
                {
                    UniTask task = _tween.AsyncWaitForCompletion().AsUniTask();
                    task.ToCancellationToken(_cts.Token);
                
                    bool isCanceled = await task.SuppressCancellationThrow();
                    if (isCanceled)
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void OnDisable()
        {
            _tween?.Kill();
            _cts?.Cancel();
        }
    }
}
