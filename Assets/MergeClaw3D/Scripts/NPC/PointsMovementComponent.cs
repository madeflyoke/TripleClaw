using System;
using System.Collections.Generic;
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
        
        public void Initialize()
        {
            transform.position = _originalPoint.position;
        }

        public void MoveToNextAvailablePoint(Action onComplete=null)
        {
            _tween?.Kill();
            if (_currentPointIndex>=_destinationPoints.Count)
            {
                Debug.LogWarning("false");
                return;
            }

            var point = _destinationPoints[_currentPointIndex];

            _tween = transform.DOMove(point.position, _speed).SetSpeedBased(true)
                .OnComplete(() => onComplete?.Invoke()).SetEase(Ease.Linear);
            _currentPointIndex++;
        }

        private void OnDisable()
        {
            _tween?.Kill();
        }
    }
}
