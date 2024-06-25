using MergeClaw3D.Scripts.Items.Enums;

namespace MergeClaw3D.Scripts.Items.Data
{
    public struct ItemSpecificationData
    {
        public readonly ItemSize ItemSize;

        public ItemSpecificationData(ItemSize itemSize)
        {
            ItemSize = itemSize;
        }
    }
}
