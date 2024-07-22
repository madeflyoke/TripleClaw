using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Stages.Data
{
    [Serializable]
    public abstract class StageData
    {
        public int Id;
        public abstract string SceneName { get; }
        
#if UNITY_EDITOR

        public virtual void ManualValidate()
        {
        }
#endif
    }
}
