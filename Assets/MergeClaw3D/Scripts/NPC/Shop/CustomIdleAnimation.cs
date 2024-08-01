using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MergeClaw3D.Scripts.NPC.Shop
{
    public class CustomBreathAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _chestTr;
        [SerializeField] private float _height = 0.001f;
        [SerializeField] private float _speed = 2f;
        private CancellationTokenSource _cts;
        private bool _isReady;
        
        private async void Awake()
        {
            _cts = new CancellationTokenSource();
            await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(0, 1f)), cancellationToken:_cts.Token).SuppressCancellationThrow();
            _isReady = true;
        }

        private void Update()
        {
            if (_isReady)
            {
                _chestTr.position += (Vector3.up* Mathf.Sin(Time.time * _speed) * _height);
            }
        }

        private void OnDisable()
        {
            _cts?.Cancel();
        }
    }
}
