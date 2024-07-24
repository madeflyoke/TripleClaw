using System;
using System.Collections.Generic;
using System.Linq;
using MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Interfaces;
using MergeClaw3D.Scripts.Items.Enums;
using MergeClaw3D.Scripts.Items.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Stages.Data.Modules
{
    [Serializable]
    public class ItemsDataProviderModule : IStageDataModule
    {
        public List<ItemDataSet> ItemsDataSets = new List<ItemDataSet>();
        public List<ItemSizeRatio> ItemSizeRatios = new List<ItemSizeRatio>();
        
        [ReadOnly, ShowInInspector] public int TotalItemsCount
        {
            get
            {
                if (ItemsDataSets!=null)
                {
                    return ItemsDataSets.Sum(x => x.GroupsCount) * ItemConstants.ITEMS_GROUP_COUNT;
                }

                return 0;
            }
        }

#if UNITY_EDITOR
        
        public void ManualValidate()
        {
           
        }
        
        [ReadOnly, ShowInInspector, GUIColor(nameof(EDITOR_ItemsRatioPropertyColor))]
        private int EDITOR_TotalItemsRatio
        {
            get
            {
                if (ItemSizeRatios!=null)
                {
                    return ItemSizeRatios.Sum(x => x.RatioPartPercent);
                }
                return 0;
            }
        }
        
        private Color EDITOR_ItemsRatioPropertyColor
        {
            get
            {
                if (ItemSizeRatios!=null)
                {
                    if (ItemSizeRatios.Sum(x => x.RatioPartPercent)!=100)
                    {
                        return Color.red;
                    }
                    return Color.green;
                }
                return Color.gray;
            }
        }
#endif
        
        [Serializable]
        public class ItemDataSet
        {
            public bool IsRandomVariants;
            [HideIf(nameof(IsRandomVariants))] public int VariantIndex;
            public int GroupsCount=1;
        }
        
        [Serializable]
        public struct ItemSizeRatio
        {
            public ItemSize ItemSize;
            public int RatioPartPercent;
        }
      
    }
}
