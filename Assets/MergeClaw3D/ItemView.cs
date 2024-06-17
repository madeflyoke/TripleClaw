using UnityEngine;

namespace MergeClaw3D.MergeClaw3D
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Transform _itemViewHolder;

#if UNITY_EDITOR

        [SerializeField] private ItemSize EDITOR_itemSize;

        private enum ItemSize
        {
            SMALL = 0,
            MEDIUM = 1,
            LARGE =2
        }
        
        private void OnValidate()
        {
            var scale = Vector3.one;
            switch (EDITOR_itemSize)
            {
                case ItemSize.SMALL:
                    scale *= ItemConstants.SMALL_ITEM_SCALE;
                    break;
                case ItemSize.MEDIUM:
                    scale *= ItemConstants.MEDIUM_ITEM_SCALE;
                    break;
                case ItemSize.LARGE:
                    scale *= ItemConstants.LARGE_ITEM_SCALE;
                    break;
            }

            _itemViewHolder.localScale = scale;
        }

#endif
    }
}
