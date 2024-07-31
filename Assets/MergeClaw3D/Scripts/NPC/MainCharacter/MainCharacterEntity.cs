using MergeClaw3D.Scripts.NPC.Interfaces;
using MergeClaw3D.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.NPC.MainCharacter
{
    public class MainCharacterEntity : MonoBehaviour, INpcEntity, IAnimatedEntity
    {
        [Inject] private SignalBus _signalBus; 
        
        [SerializeField] private AnimationComponent _animationComponent;
        [SerializeField] private PointsMovementComponent _movementComponent;

        public void Awake()
        {
             InitializeComponents();
        }
        
        private void InitializeComponents()
        {
            _animationComponent.Initialize(this);
            _movementComponent.Initialize();
            _signalBus.Subscribe<StageStartedSignal>(OnStageStarted);
            _signalBus.Subscribe<StageCompletedSignal>(OnStageCompleted);
        }
        
        private void OnStageStarted()
        {
            _movementComponent.TryToMoveToNextAvailablePoint(()=>
            {
                _animationComponent.TryPlayAnimation(AnimationConstants.IDLE_STATE);
            }, true);
            _animationComponent.TryPlayAnimation(AnimationConstants.RUN_STATE);
        }
        
        private void OnStageCompleted()
        {
            _movementComponent.MoveSequenceToLeftPoints(()=>
            {
                _signalBus.Fire<NextStageCallSignal>();
            }, true);
            _animationComponent.TryPlayAnimation(AnimationConstants.RUN_STATE);
            
            // _movementComponent.MoveSequenceToLeftPoints(()=> //for difficult paths
            // {
            //     _animationComponent.TryPlayAnimation(AnimationConstants.IDLE_STATE);
            // }, true);
            // _animationComponent.TryPlayAnimation(AnimationConstants.RUN_STATE);
        }
    }
}
