using System.Threading;
using Cysharp.Threading.Tasks;
using MergeClaw3D.Scripts.Services.Interfaces;
using UnityEngine;

namespace MergeClaw3D.Scripts.Services
{
    public class PauseService : IService
    {
        public UniTask Initialize(CancellationTokenSource cts)
        {
            return UniTask.CompletedTask;
        }
        
        public void SetPause(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
        }
        
        public void Dispose()
        {
            
        }
    }
}
