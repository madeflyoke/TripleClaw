using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Stages.Data
{
    public class PredefinedItemsStageData : ItemsStageData
    {
        public class ItemDataSet
        {
            public int Index;
            public int GroupsCount;
        }
        
        [SerializeField] public List<ItemDataSet> PredefinedItemsData;
        
        
#if UNITY_EDITOR

        public override void ManualValidate()
        {
            base.ManualValidate();
            if (PredefinedItemsData!=null)
            {
                ItemsGroupsCount = PredefinedItemsData.Sum(x=>x.GroupsCount);
                ItemsVariantsCount = PredefinedItemsData.Count;
                
                PredefinedItemsData = PredefinedItemsData
                    .GroupBy(item => item.Index)
                    .Where(group => group.Count() == 1)
                    .SelectMany(group => group)
                    .ToList();
            }
        }
#endif
    }
}
