using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MergeClaw3D.Scripts.Place
{
    public class ItemPlacesHolder : MonoBehaviour
    {
        [SerializeField] private List<ItemPlace> _itemPlaces;

        public IReadOnlyCollection<ItemPlace> Places => _itemPlaces;
        public int PlacesCount => _itemPlaces.Count;
        public int FreePlaceCount => _itemPlaces.Count(i => i.State == PlaceState.Free);
        public int OccupiedPlaceCount => _itemPlaces.Count(i => i.State == PlaceState.Occupied);
        
        public ItemPlace GetRightFreePlace()
        {
            var placeIndex = _itemPlaces.FindLastIndex(i => i.State == PlaceState.Occupied);

            placeIndex += 1;

            if (placeIndex >= _itemPlaces.Count)
            {
                return null;
            }

            return _itemPlaces[placeIndex];
        }

        public ItemPlace GetLeftFreePlace()
        {
            return _itemPlaces.First(i => i.State == PlaceState.Free);
        }

        public int GetLeftFreePlaceIndex()
        {
            return _itemPlaces.FindIndex(i => i.State == PlaceState.Free);
        }

        public int GetRightOccupiedPlaceIndex()
        {
            return _itemPlaces.FindLastIndex(i => i.State == PlaceState.Occupied);
        }

        public int GetLeftOccupiedPlaceIndex()
        {
            return _itemPlaces.FindIndex(i => i.State == PlaceState.Occupied);
        }

        public int GetPlaceIndex(ItemPlace place)
        {
            return _itemPlaces.IndexOf(place);
        }

        public bool IsAnyEmptySpaceBeforeOccupiedItem()
        {
            if (OccupiedPlaceCount == 0)
            {
                return false;
            }

            var emptyPlaceIndex = GetLeftFreePlaceIndex();

            for (int i = emptyPlaceIndex + 1; i < PlacesCount; i++)
            {
                if (_itemPlaces[i].State == PlaceState.Occupied)
                {
                    return true;
                }
            }

            return false;
        }

        public ItemPlace GetPlace(int index)
        {
            if (_itemPlaces.Count <= index)
            {
                throw new IndexOutOfRangeException("Index out of range in place list");
            }

            return _itemPlaces[index];
        }
    }
}