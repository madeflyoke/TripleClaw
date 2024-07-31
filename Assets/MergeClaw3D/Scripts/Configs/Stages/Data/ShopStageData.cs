using System.Collections.Generic;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Stages.Data
{
    public class ShopStageData : StageData
    {
        public override string SceneName => "ShopStage";
        
        public override T GetModule<T>()
        {
            return default;
        }

        public override IEnumerable<T> GetModules<T>()
        {
            return default;
        }
    }
}
