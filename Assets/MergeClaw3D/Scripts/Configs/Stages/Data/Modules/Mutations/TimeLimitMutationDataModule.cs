using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Mutations
{
    public class TimeLimitMutationDataModule : TimeLimitDataModule, IBaseStageMutationDataModule
    {
        [field: SerializeField] public int ResolveArtifactId { get; private set; }
        
        #if UNITY_EDITOR
        public void ManualValidate()
        {
        }
        #endif
    }
}
