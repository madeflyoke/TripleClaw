using System.Collections.Generic;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Stages.Data
{
    public class ShopStageData : StageData
    {
        public override string SceneName { get; }
        public override T GetModule<T>()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<T> GetModules<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}
