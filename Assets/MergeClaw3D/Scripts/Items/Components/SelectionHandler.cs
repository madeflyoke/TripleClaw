using MergeClaw3D.Scripts.Services;
using System;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Tools.Outline.HighlightPlus.Runtime.Scripts
{
    public class SelectionHandler : MonoBehaviour
    {
        public event Action Selected; 
        
        [Inject] private ServicesHolder _servicesHolder;
        
        [SerializeField] private HighlightEffect _highlightEffect;
       
        private bool _selectionActive;
        private bool _externalSelectionMode = true;

        public void Initialize()
        {
            var inputService = _servicesHolder.GetService<InputService>();

            inputService.PointerInputDown += AllowSelection;
            inputService.PointerInputUp += DenySelection;
        }

        public void ChangeMode(bool selectable)
        {
            _externalSelectionMode = selectable;
        }
        
        private void AllowSelection()
        {
            if (_externalSelectionMode == false)
            {
                return;
            }

            _selectionActive = true;
        }

        private void DenySelection()
        {
            if (_externalSelectionMode == false)
            {
                return;
            }

            _selectionActive = false;
            if (_highlightEffect.highlighted)
            {
                Selected?.Invoke();
            }
            _highlightEffect.SetHighlighted(false);
        }
        
        private void OnMouseEnter()
        {
            if (_selectionActive)
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
            if (_selectionActive)
            {
                _highlightEffect.SetHighlighted(false);
            }
        }
    }
}
