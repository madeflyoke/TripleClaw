using System;
using System.Collections.Generic;
using System.Linq;
using MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Interfaces;
using Sirenix.Serialization;

namespace MergeClaw3D.Scripts.Configs.Stages.Data
{
    [Serializable]
    public class ItemsStageData : StageData
    {
        private const string SCENE_NAME = "ItemsStage";
        public override string SceneName => SCENE_NAME;
        
        [OdinSerialize] public List<IStageDataModule> Modules;
        
        public override T GetModule<T>()
        {
            return (T)Modules.FirstOrDefault(x => x.GetType() == typeof(T));
        }
        
#if UNITY_EDITOR
        
        public override void ManualValidate()
        {
            base.ManualValidate();
            if (Modules!=null)
            {
                Modules.ForEach(x=>x.ManualValidate());
            }
        }
        
#endif
    }
}