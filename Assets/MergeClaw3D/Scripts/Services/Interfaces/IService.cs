using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace MergeClaw3D.Scripts.Services.Interfaces
{
    public interface IService : IDisposable
    {
        public UniTask Initialize(CancellationTokenSource cts);
    }
}
