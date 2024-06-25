using System.Threading;
using Cysharp.Threading.Tasks;
using Lean.Pool;
using MergeClaw3D.Scripts.Services.Interfaces;
using Zenject;

namespace MergeClaw3D.Scripts.Services
{
    public class UtilityHelperService : IService
    {
        [Inject] private DiContainer _container;
        
        public UniTask Initialize(CancellationTokenSource cts)
        {
            SetupUtilitiesDependencies();
            return UniTask.CompletedTask;
        }

        private void SetupUtilitiesDependencies()
        {
            LeanPool.SetupDiContainer(_container);
        }
        
        public void Dispose()
        {
            
        }
    }
}
