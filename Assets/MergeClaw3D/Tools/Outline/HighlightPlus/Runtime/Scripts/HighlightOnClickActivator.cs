using System;
using HighlightPlus;
using UniRx;
using UnityEngine;

namespace MergeClaw3D.MergeClaw3D
{
    public class HighlightOnClickActivator : MonoBehaviour
    {
        [SerializeField] private HighlightEffect _highlightEffect;
       
        private bool _isActive;

        private void Start()
        {
            InputService.S.PointerInputDown += Activate;
            InputService.S.PointerInputUp += Deactivate;
        }

        private void Activate()
        {
            _isActive = true;
        }

        private void Deactivate()
        {
            _isActive = false;
            _highlightEffect.SetHighlighted(false);
        }
        
        private void OnMouseEnter()
        {
            if (_isActive)
            {
                _highlightEffect.SetHighlighted(true);
            }
        }

        private void OnMouseDown()
        {
            _highlightEffect.SetHighlighted(true);
        }
        
        private void OnMouseExit()
        {
            if (_isActive)
            {
                _highlightEffect.SetHighlighted(false);
            }
        }
    }
}
