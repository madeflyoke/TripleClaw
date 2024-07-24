using System;
using MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Interfaces;

namespace MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Mutations
{
    [Serializable]
    public abstract class BaseStageMutationDataModule : IStageDataModule
    {
        public abstract void ManualValidate();
    }
}
