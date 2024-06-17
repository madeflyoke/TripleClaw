using MergeClaw3D.MergeClaw3D.Enums;
using UnityEngine;

namespace MergeClaw3D.MergeClaw3D
{
    public class ItemView : MonoBehaviour
    {
        public ItemView SetCorrespondingSize(ItemSize itemSize)
        {
            var finalScale = Vector3.one; //default one
            switch (itemSize)
            {
                case ItemSize.SMALL:
                    finalScale *= ItemConstants.SMALL_ITEM_SCALE;
                    break;
                case ItemSize.MEDIUM:
                    finalScale *= ItemConstants.MEDIUM_ITEM_SCALE;
                    break;
                case ItemSize.LARGE:
                    finalScale *= ItemConstants.LARGE_ITEM_SCALE;
                    break;
            }

            transform.localScale = finalScale;
            return this;
        }

#if UNITY_EDITOR

        [SerializeField] private ItemSize EDITOR_itemSize;
        
        private void OnValidate()
        {
           SetCorrespondingSize(EDITOR_itemSize);
        }

#endif
    }
}
