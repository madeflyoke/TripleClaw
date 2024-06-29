using System;
using System.Collections.Generic;
using MergeClaw3D.Scripts.Configs.NPC;
using MergeClaw3D.Scripts.NPC.Interfaces;
using UnityEngine;

namespace MergeClaw3D.Scripts.NPC
{
    /// <summary>
    /// Use AnimationConstants for valid names
    /// </summary>
    public class AnimationComponent : MonoBehaviour
    {
        [SerializeField] private AnimationSpeedConfig _speedConfig;
        [SerializeField] private Animator _animator;
        private Dictionary<string, float> _defaultSpeeds;

        public void Initialize(IAnimatedEntity entity)
        {
            SetSpeedOnStates(_speedConfig.GetStatesSpeed(entity));
        }
        
        public void TryPlayAnimation(string stateName)
        {
            _animator.CrossFadeInFixedTime(stateName, 0.25f);
            SetSpeed(stateName);
        }
        
        private void SetSpeedOnStates(Dictionary<string, float> stateNameByValue)
        {
            _defaultSpeeds = stateNameByValue;
        }
        
        private void SetSpeed(string stateName)
        {
            _animator.SetFloat(AnimationConstants.SPEED_PARAMETER, GetCurrentSpeed(stateName));
        }

        private float GetCurrentSpeed(string stateName)
        {
            var finalValue = 1f;
            if (_defaultSpeeds.ContainsKey(stateName))
            {
                finalValue = _defaultSpeeds[stateName];
            }

            return finalValue;
        }
    }
}
