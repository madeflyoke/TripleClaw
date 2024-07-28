using System;
using MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Interfaces;

namespace MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Mutations
{
    public interface IBaseStageMutationDataModule : IStageDataModule
    {
        public int ResolveArtifactId { get; }

    }
}
