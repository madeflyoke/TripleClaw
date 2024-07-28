using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MergeClaw3D.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonAnimationPuncher : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private float _punchForce = -0.15f;
        private Tween _tween;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
            _tween?.Kill();
        }
        
        private void OnButtonClick()
        {
            _tween?.Kill();
            var defaultScale = _button.transform.localScale;
            _tween = _button.transform.DOPunchScale(Vector3.one * _punchForce, 0.2f, 2).OnKill(()=>
            {
                _button.transform.localScale = defaultScale;
            }).SetEase(Ease.Linear);
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            if (_button==null)
            {
                _button = GetComponent<Button>();
            }
        }

#endif

    }
}
