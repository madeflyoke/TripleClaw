using System;
using System.Collections.Generic;
using System.Linq;
using MergeClaw3D.Scripts.Items;
using MergeClaw3D.Scripts.Items.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Stages
{
    [Serializable]
    public class StageData
    {
        public int Id;
        public int ItemsGroupsCount =1;
        [SerializeField, Range(1, 16)] public int ItemsVariantsCount =1;
        [SerializeField] public List<ItemSizeRatio> ItemSizeRatios;
        [ReadOnly, ShowInInspector] public int TotalItemsCount => ItemsGroupsCount * ItemConstants.ITEMS_GROUP_COUNT;
        
        [Serializable]
        public struct ItemSizeRatio
        {
            public ItemSize ItemSize;
            public int RatioPartPercent;
        }
        
#if UNITY_EDITOR
        
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
    }
}
