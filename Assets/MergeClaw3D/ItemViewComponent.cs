using System;
using UnityEngine;

namespace MergeClaw3D.MergeClaw3D
{
    public class ItemViewComponent : MonoBehaviour
    {
        private ItemView _itemView;
        
        public void Initialize(ItemView itemView)
        {
            _itemView = itemView;
        }
    }
}
