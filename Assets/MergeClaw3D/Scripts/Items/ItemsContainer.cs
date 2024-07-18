using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MergeClaw3D.Scripts.Items
{
    public class ItemsContainer
    {
        private Dictionary<int, HashSet<ItemEntity>> _items = new();

        public int ItemCount => _items.Sum(l => l.Value.Count);
        public IReadOnlyCollection<ItemEntity> Items => _items.Values.SelectMany(i => i).ToList();

        public void AddItem(ItemEntity itemEntity)
        {
            if (_items.ContainsKey(itemEntity.Id) == false)
            {
                _items[itemEntity.Id] = new();
            }

            _items[itemEntity.Id].Add(itemEntity);
        }

        public void RemoveItem(ItemEntity itemEntity)
        {
            _items[itemEntity.Id].Remove(itemEntity);
        }

        public void DestroyItem(ItemEntity itemEntity)
        {
            RemoveItem(itemEntity);

            GameObject.Destroy(itemEntity.gameObject);
        }
    }
}