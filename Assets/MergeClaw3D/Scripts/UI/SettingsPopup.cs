using MergeClaw3D.Scripts.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MergeClaw3D.Scripts.UI
{
    public class SettingsPopup : MonoBehaviour
    {
        [Inject] private ServicesHolder _servicesHolder;
        
        [SerializeField] private Button _closeButton;
        [SerializeField] private PopupAnimator _animator;
        
        public void Show()
        {
            _closeButton.onClick.AddListener(Hide);
            _servicesHolder.GetService<PauseService>().SetPause(true);
            
            _animator.PlayShowAnimation();
        }
        
        public void Hide()
        {
            _closeButton.onClick.RemoveListener(Hide);
            
            _animator.PlayHideAnimation(() =>
            {
                _servicesHolder.GetService<PauseService>().SetPause(false);
            });
        }
    }
}
