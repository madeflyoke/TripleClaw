using System;
using MergeClaw3D.Scripts.Configs.Stages.Data;

namespace MergeClaw3D.Scripts.Stages.Variants
{
    public interface IStage
    {
        public StageData StageData { get; }

        public void Initialize(StageData stageData);
    }
}
