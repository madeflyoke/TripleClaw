using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Place
{
    public class ItemPlacesHolder : MonoBehaviour, IInitializable
    {
        [SerializeField] private List<Transform> _places;

        private List<ItemPlace> _itemPlaces = new();

        public IReadOnlyCollection<ItemPlace> Places => _itemPlaces;
        public int PlacesCount => _itemPlaces.Count;
        public int FreePlaceCount => _itemPlaces.Count(i => i.State == PlaceState.Free);
        public int OccupiedPlaceCount => _itemPlaces.Count(i => i.State == PlaceState.Occupied);

        public void Initialize()
        {
            for (int i = 0; i < _places.Count; i++)
            {
                _itemPlaces.Add(new(_places[i]));
            }
        }

        public ItemPlace GetRightFreePlace()
        {
            var placeIndex = _itemPlaces.FindLastIndex(i => i.State == PlaceState.Occupied);

            placeIndex += 1;

            if (placeIndex >= _places.Count)
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