using MergeClaw3D.Scripts.Services;
using MergeClaw3D.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.UI
{
    public class LevelCompletePopup : MonoBehaviour
    {
        [Inject] private ServicesHolder _servicesHolder;
        
        [SerializeField] private PopupAnimator _popupAnimator;

        public void Show(LevelCompletedSignal signal)
        {
            _servicesHolder.GetService<PauseService>().SetPause(true);
            _popupAnimator.PlayShowAnimation();
        }
        
        public void Hide()
        {
            _popupAnimator.PlayHideAnimation(() =>
            {
                _servicesHolder.GetService<PauseService>().SetPause(false);
            });
        }
    }
}
