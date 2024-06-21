using MergeClaw3D.Scripts.Services;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Tools.Outline.HighlightPlus.Runtime.Scripts
{
    public class HighlightOnClickActivator : MonoBehaviour
    {
        [Inject] private ServicesHolder _servicesHolder;
        
        [SerializeField] private HighlightEffect _highlightEffect;
       
        private bool _isActive;

        private void Start()
        {
            var inputService = _servicesHolder.GetService<InputService>();
            inputService.PointerInputDown += Activate;
            inputService.PointerInputUp += Deactivate;
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
