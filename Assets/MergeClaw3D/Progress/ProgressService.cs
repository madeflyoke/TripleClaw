using MergeClaw3D.MergeClaw3D.Progress.Handlers;
using UnityEngine;

namespace MergeClaw3D.MergeClaw3D.Progress
{
    public class ProgressService : MonoBehaviour
    {
        public static ProgressService S;
        
        public CurrencyProgressHandler CurrencyProgressHandler { get; private set; }

        private void Awake()
        {
            S = this;
            CurrencyProgressHandler = new CurrencyProgressHandler();
        }
    }
}
