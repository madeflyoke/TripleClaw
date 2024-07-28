using MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Interfaces;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Stages.Data.Modules
{
    public class TimeLimitDataModule : IStageDataModule
    {
        public long SecondsTimeLimit;
        
        public void ManualValidate()
        {
        }
    }
}
