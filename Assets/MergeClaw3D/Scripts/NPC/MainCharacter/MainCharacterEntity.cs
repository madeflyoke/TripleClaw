using System;
using System.Collections.Generic;
using MergeClaw3D.Scripts.NPC.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.NPC.MainCharacter
{
    public class MainCharacterEntity : MonoBehaviour, INpcEntity, IAnimatedEntity
    {
        [SerializeField] private AnimationComponent _animationComponent;
        [SerializeField] private PointsMovementComponent _movementComponent;

        public void Awake()
        {
             InitializeComponents();
        }

        [Button]
        private void StartMoving()
        {
            _movementComponent.TryToMoveToNextAvailablePoint(()=>
            {
                Debug.LogWarning("gameplay started");
                _animationComponent.TryPlayAnimation(AnimationConstants.IDLE_STATE);
            }, true);
            _animationComponent.TryPlayAnimation(AnimationConstants.RUN_STATE);
        }
        
        private void InitializeComponents()
        {
            _animationComponent.Initialize(this);
            _movementComponent.Initialize();
        }
        
        [Button]
        public void OnLevelCleaned()
        {
            _movementComponent.MoveSequenceToLeftPoints(()=>
            {
                Debug.LogWarning("next level");
                _animationComponent.TryPlayAnimation(AnimationConstants.IDLE_STATE);
            }, true);
            _animationComponent.TryPlayAnimation(AnimationConstants.RUN_STATE);
        }
    }
}
