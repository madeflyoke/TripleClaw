using System;
using MergeClaw3D.Scripts.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Stages
{
    [Serializable]
    public class StageData
    {
        public int Id;
        [SerializeField, Range(1, 16)] public int ItemsVariantsCount =1;
        public int ItemsGroupsCount =1;
        
#if UNITY_EDITOR

        [ReadOnly, ShowInInspector]
        private int EDITOR_TotalItemsCount => ItemsGroupsCount * ItemConstants.ITEMS_GROUP_COUNT;

#endif
    }
}
