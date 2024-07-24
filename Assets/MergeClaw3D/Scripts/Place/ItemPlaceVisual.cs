using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.Place
{
    public class ItemPlaceVisual : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _mainVfx;
        [SerializeField] private SpriteRenderer _glowSprite;
        
        [Title("MergeEffect")]
        [SerializeField] private ParticleSystem _matchedVfx;
        [SerializeField] private ParticleSystem _mergedVfx;

        [Title("StateVisual")] 
        [SerializeField] private Color _disabledMainVfxColor;
        [SerializeField, ReadOnly] private Color _enabledMainVfxColor;
        [SerializeField, ReadOnly] private float _defaultMainVfxSpeed;


        public void PlayMatchedVfx()
        {
            _matchedVfx.Play();
        }

        public void PlayMergedVfx()
        {
            _mergedVfx.Play();
        }
        
        public void SetVisualState(bool isEnabled)
        {
            var mainModule = _mainVfx.main;
            var startColor = mainModule.startColor;
            startColor.color = isEnabled?  _enabledMainVfxColor :_disabledMainVfxColor;
            mainModule.startColor = startColor;
            
            _glowSprite.enabled = isEnabled;

            if (isEnabled)
            {
                _mainVfx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                _mainVfx.Play();
            }
            else
            {
                _mainVfx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                _mainVfx.Play();
                _mainVfx.Pause();
            }
        }
        
#if UNITY_EDITOR

        [Button]
        private void SetMainVfxData()
        {
            if (_mainVfx!=null)
            {
                _enabledMainVfxColor = _mainVfx.main.startColor.color;
                _defaultMainVfxSpeed = _mainVfx.main.simulationSpeed;
            }
        }

#endif
    }
}
