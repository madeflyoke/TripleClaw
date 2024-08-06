using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MergeClaw3D.Scripts.Inventory.Interfaces;
using UniRx;
using UnityEngine;

namespace MergeClaw3D.Scripts.Inventory.UI
{
    public class ArtifactPreview : MonoBehaviour
    {
        [SerializeField] private Transform _showedArtifactFinalPoint;
        private Sequence _artifactShowSeq;
        private CancellationTokenSource _cts;
        
        public async void PreviewArtifact(IArtifact target, Action onComplete)
        {
            await UniTask.Delay(3000);
            MonoBehaviour artifactMonobeh = target as MonoBehaviour;
            var effectsComponent =target.GetArtifactComponent<ArtifactEffectsComponent>();
            effectsComponent.PlayGlowingVfx();
            
            _cts = new CancellationTokenSource();
            
            _artifactShowSeq?.Kill();
            _artifactShowSeq = DOTween.Sequence();
            _artifactShowSeq
                .Append(artifactMonobeh.transform.DOMove(_showedArtifactFinalPoint.position, 1.5f))
                .Join(artifactMonobeh.transform.DOScale(artifactMonobeh.transform.localScale*2f, 1f))
                .SetEase(Ease.OutCirc);
            
            var task = _artifactShowSeq.AsyncWaitForCompletion().AsUniTask();
            task.ToCancellationToken(_cts.Token);
            await task.SuppressCancellationThrow();
            
            await UniTask.Delay(TimeSpan.FromSeconds(2), cancellationToken: _cts.Token).SuppressCancellationThrow();
            onComplete?.Invoke();
            
            effectsComponent.StopAll();
            effectsComponent.PlayDisappearVfx(true);
        }
        
        private void OnDisable()
        {
            _cts?.Cancel();
            _artifactShowSeq?.Kill();
        }
    }
}
