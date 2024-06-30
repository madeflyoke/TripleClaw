using System;
using DG.Tweening;
using MergeClaw3D.Scripts.NPC.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.Level
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Collider _triggerCollider;
        [SerializeField] private Transform _doorPivot;
        [Space]
        [SerializeField] private float _openedYRot;
        [SerializeField] private float _duration;
        [SerializeField] private bool _oneTimed;
        private Tween _tween;
        private Vector3 _defaultRot;
        private Vector3 _openedRot;
        private bool _isOpened;

        private void Awake()
        {
            _defaultRot = _doorPivot.localRotation.eulerAngles;
            _openedRot = new Vector3(_defaultRot.x, _openedYRot, _defaultRot.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<INpcEntity>()!=null)
            {
                Open();
            }
        }

        [Button]
        private void Open()
        {
            if (_oneTimed && _isOpened)
                return;
            
            _tween?.Kill();
            _tween = _doorPivot.DOLocalRotate(_openedRot, _duration)
                .SetEase(Ease.OutBack);
            _isOpened = true;
        }

        [Button]
        private void Close()
        {
            if (_oneTimed)
              return;
            
            _tween?.Kill();
            _tween = _doorPivot.DOLocalRotate(_defaultRot, _duration)
                .SetEase(Ease.Linear);
        }

        private void OnDisable()
        {
            _tween?.Kill();
        }
    }
}
