using System.Threading;
using Cysharp.Threading.Tasks;
using MergeClaw3D.Scripts.Services.Interfaces;
using MergeClaw3D.Scripts.Services.Progress.Handlers;

namespace MergeClaw3D.Scripts.Services.Progress
{
    public class ProgressService : IService
    {
        public CurrencyProgressHandler CurrencyProgressHandler { get; private set; }
        
        public UniTask Initialize(CancellationTokenSource cts)
        {
            CurrencyProgressHandler = new CurrencyProgressHandler();
            return UniTask.CompletedTask;
        }
        
        public void Dispose()
        {
            
        }
    }
}
