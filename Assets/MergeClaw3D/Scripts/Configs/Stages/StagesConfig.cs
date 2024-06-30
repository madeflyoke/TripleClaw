using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Stages
{
    [CreateAssetMenu(menuName = "Stage/StagesConfig", fileName = "StagesConfig")]
    public class StagesConfig : SerializedScriptableObject
    {
        [OdinSerialize] private List<StageData> _stageDatas;

        public StageData GetStageData(int id)
        {
            return _stageDatas.FirstOrDefault(x => x.Id == id);
        }
        
        
        #if UNITY_EDITOR

        private void OnValidate()
        {
            for (var index = 0; index < _stageDatas.Count; index++)
            {
                var item = _stageDatas[index];
                if (item.Id!=index)
                {
                    item.Id = index;
                }

                if (item.ItemsVariantsCount>item.ItemsGroupsCount)
                {
                    item.ItemsVariantsCount = item.ItemsGroupsCount;
                }
            }
        }

#endif
    }
}
