using MergeClaw3D.Scripts.Items;
using System;
using UnityEngine;

namespace MergeClaw3D.Scripts.Place
{
    public class ItemPlace
    {
        public readonly Transform _transform;

        public ItemEntity Item { get; private set; }
        public PlaceState State { get; private set; }
        public Vector3 Position => _transform.position;

        public ItemPlace(Transform placeTransform)
        {
            _transform = placeTransform;
        }

        public void SetItem(ItemEntity itemEntity)
        {
            Item = itemEntity;

            State = PlaceState.Occpuied;
        }

        public ItemEntity ExtractItem()
        {
            State = PlaceState.Free;

            var item = Item;
            Item = null;

            return item;
        }
    }
}