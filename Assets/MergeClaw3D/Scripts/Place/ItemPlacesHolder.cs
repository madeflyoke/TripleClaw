using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.Place
{
    public class ItemPlacesHolder : MonoBehaviour
    {
        [SerializeField] private List<ItemPlace> _itemPlaces;
        private List<ItemPlace> EnabledPlaces => _itemPlaces.Where(x=>x.IsEnabled).ToList();
        
        public IReadOnlyCollection<ItemPlace> Places => EnabledPlaces;
        public int PlacesCount => EnabledPlaces.Count;
        public int FreePlaceCount => EnabledPlaces.Count(i => i.State == PlaceState.Free);
        public int OccupiedPlaceCount => EnabledPlaces.Count(i => i.State == PlaceState.Occupied);
        
        [Button]
        public void SetPlacesState(int range, bool isEnabled)
        {
            if (range>_itemPlaces.Count)
            {
                Debug.LogError("Input incorrect, range larger then actual places count!");
                return;
            }
            _itemPlaces.GetRange(_itemPlaces.Count - range, range).ForEach(x=>x.SetState(isEnabled));
        }
        
        public ItemPlace GetRightFreePlace()
        {
            var placeIndex = EnabledPlaces.FindLastIndex(i => i.State == PlaceState.Occupied);

            placeIndex += 1;

            if (placeIndex >= EnabledPlaces.Count)
            {
                return null;
            }

            return EnabledPlaces[placeIndex];
        }

        public ItemPlace GetLeftFreePlace()
        {
            return EnabledPlaces.First(i => i.State == PlaceState.Free);
        }

        public int GetLeftFreePlaceIndex()
        {
            return EnabledPlaces.FindIndex(i => i.State == PlaceState.Free);
        }

        public int GetRightOccupiedPlaceIndex()
        {
            return EnabledPlaces.FindLastIndex(i => i.State == PlaceState.Occupied);
        }

        public int GetLeftOccupiedPlaceIndex()
        {
            return EnabledPlaces.FindIndex(i => i.State == PlaceState.Occupied);
        }

        public int GetPlaceIndex(ItemPlace place)
        {
            return EnabledPlaces.IndexOf(place);
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
                if (EnabledPlaces[i].State == PlaceState.Occupied)
                {
                    return true;
                }
            }

            return false;
        }

        public ItemPlace GetPlace(int index)
        {
            if (EnabledPlaces.Count <= index)
            {
                throw new IndexOutOfRangeException("Index out of range in place list");
            }

            return EnabledPlaces[index];
        }
        
#if UNITY_EDITOR

        // [Button]
        // private void SpawnItemsPlaces(int count)
        // {
        //     for (int i = 0; i < count; i++)
        //     {
        //         float xOffset = (i - (count - 1) / 2f) * _distanceBetweenPlaces;
        //         Vector3 position = new Vector3(xOffset, _centerPoint.position.y, _centerPoint.position.z);
        //
        //         Instantiate(_itemPlacePrefab, position, Quaternion.identity, transform.GetChild(0));
        //     }
        // }

#endif
    }
}