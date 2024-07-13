using MergeClaw3D.Scripts.Merge;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Items
{
    public class ItemPlacesHolder : MonoBehaviour, IInitializable
    {
        [SerializeField] private List<Transform> _places;

        private List<ItemPlace> _itemPlaces = new();

        public IReadOnlyCollection<ItemPlace> ItemPlaces => _itemPlaces;
        public int OccupiedPlaceCount => _itemPlaces.Where(i => i.Occupied).Count();
        public int FreePlaceCount => _itemPlaces.Where(i => i.Occupied == false).Count();

        public void Initialize()
        {
            for (int i = 0; i < _places.Count; i++)
            {
                var place = new ItemPlace(_places[i]);

                _itemPlaces.Add(place);
            }
        }

        public ItemPlace GetFreePlace()
        {
            return _itemPlaces.FirstOrDefault(i => i.Occupied == false);
        }
    }
}