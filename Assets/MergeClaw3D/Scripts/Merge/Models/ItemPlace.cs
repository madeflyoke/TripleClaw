using MergeClaw3D.Scripts.Items;
using UnityEngine;

namespace MergeClaw3D.Scripts.Merge
{
    public class ItemPlace
    {
        public readonly Transform transform;
        public readonly ItemPlace nextContainer;

        public ItemEntity Item { get; private set; }
        public bool Occupied { get; private set; }

        public ItemPlace(Transform containerTransform)
        {
            transform = containerTransform;
        }

        public void SetItem(ItemEntity itemEntity)
        {
            Occupied = true;

            Item = itemEntity;
        }

        public void FreePlace()
        {
            Occupied = false;

            Item = null;
        }
    }
}