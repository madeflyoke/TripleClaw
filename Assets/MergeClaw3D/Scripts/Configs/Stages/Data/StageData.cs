using System;
using MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Interfaces;

namespace MergeClaw3D.Scripts.Configs.Stages.Data
{
    [Serializable]
    public abstract class StageData
    {
        public int Id;
        public abstract string SceneName { get; }
        public abstract T GetModule<T>() where T : IStageDataModule;

#if UNITY_EDITOR

        public virtual void ManualValidate()
        {
        }
#endif
    }
}
