using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace MergeClaw3D.Scripts.NPC.Shop
{
    public class LookerAtCursorDisablerOnClick : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _vfx;
        [SerializeField] private List<MonoBehaviour> _links;
        [SerializeField] private Transform _shakeablePart;
        [SerializeField] private Transform _basePart;
        private bool _state =true;
        private Tween _tween;
        
        private void OnMouseDown()
        {
            _vfx.Play();
            _state = !_state;
            foreach (var item in _links)
            {
                item.enabled = _state;
            }
            
            _tween?.Kill();
            var defaultPos = _shakeablePart.position;
            _tween = _shakeablePart.DOShakePosition(0.5f, Vector3.one * 0.05f).SetEase(Ease.Linear)
                .OnKill(() =>
                {
                    _shakeablePart.position = defaultPos;
                });
            _shakeablePart.forward = _basePart.forward;
        }
    }
}
