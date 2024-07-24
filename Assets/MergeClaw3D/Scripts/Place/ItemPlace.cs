using MergeClaw3D.Scripts.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.Place
{
    public class ItemPlace : MonoBehaviour
    {
        public bool IsEnabled { get; private set; } = true;
        
        [SerializeField] private ItemPlaceVisual _itemPlaceVisual;

        public ItemEntity Item { get; private set; }
        public PlaceState State { get; private set; }
        public Vector3 Position => transform.position;
        
        public void SetItem(ItemEntity itemEntity)
        {
            Item = itemEntity;

            State = PlaceState.Occupied;
        }

        public ItemEntity ExtractItem()
        {
            State = PlaceState.Free;

            var item = Item;
            Item = null;

            return item;
        }

        [Button]
        public void SetState(bool isEnabled)
        {
            IsEnabled = isEnabled;
            _itemPlaceVisual.SetVisualState(isEnabled);
        }
        
        public void OnMatched()
        {
            _itemPlaceVisual.PlayMatchedVfx();
        }
        
        public void OnMerged()
        {
            _itemPlaceVisual.PlayMergedVfx();
        }

    }
}