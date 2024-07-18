using Cysharp.Threading.Tasks;
using MergeClaw3D.Scripts.Configs.Items;
using MergeClaw3D.Scripts.Events;
using MergeClaw3D.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Place
{
    public class Merger : IInitializable, IDisposable
    {
        private ItemPlacer _itemPlacer;
        private MergeConfig _mergeConfig;
        private ItemsContainer _itemsContainer;
        private ItemPlacesHolder _itemPlacesHolder;
        private CompositeDisposable _disposables = new();

        public Merger(ItemPlacer itemPlacer, ItemPlacesHolder itemPlacesHolder,
            MergeConfig mergeConfig, ItemsContainer itemsContainer)
        {
            _itemPlacer = itemPlacer;
            _itemPlacesHolder = itemPlacesHolder;
            _mergeConfig = mergeConfig;
            _itemsContainer = itemsContainer;
        }

        public void Initialize()
        {
            _itemPlacer.PlaceOccupied.Subscribe(OnPlaceOccupied)
                .AddTo(_disposables);
        }

        private void OnPlaceOccupied(ItemPlace itemPlace)
        {
            var index = _itemPlacesHolder.GetPlaceIndex(itemPlace);

            if (index < _mergeConfig.ItemNeedToMerge - 1)
            {
                return;
            }

            var counter = 0;
            var mergeList = new List<ItemPlace>();

            foreach (var place in _itemPlacesHolder.Places)
            {
                if (place.State == PlaceState.Free)
                {
                    break;
                }

                if (counter > index)
                {
                    break;
                }

                counter++;

                if (mergeList.Count == 0)
                {
                    mergeList.Add(place);
                    continue;
                }

                if (place.Item.Id != mergeList[0].Item.Id)
                {
                    mergeList.Clear();
                }

                mergeList.Add(place);

                if (mergeList.Count == _mergeConfig.ItemNeedToMerge)
                {
                    MergeItems(mergeList);
                    return;
                }
            }
        }

        private async void MergeItems(List<ItemPlace> mergeList)
        {
            var mergePositionX = mergeList.Sum(p => p.Position.x) / mergeList.Count;
            var mergePosition = new Vector3(mergePositionX, mergeList[0].Position.y, mergeList[0].Position.z);

            foreach (var place in mergeList)
            {
                MoveItemToMergePosition(place.ExtractItem(), mergePosition);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(_mergeConfig.MergeDuration));

            MessageBroker.Default.Publish(ItemsMerged.Create());

            if (_itemsContainer.ItemCount == 0)
            {
                MessageBroker.Default.Publish(AllItemsMerged.Create());
            }
        }

        private async void MoveItemToMergePosition(ItemEntity item, Vector3 mergePosition)
        {
            await item.Animator.MoveToPointAsync(mergePosition, _mergeConfig.MergeDuration);

            _itemsContainer.DestroyItem(item);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}