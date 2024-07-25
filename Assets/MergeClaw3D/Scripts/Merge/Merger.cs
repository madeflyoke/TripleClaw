using Cysharp.Threading.Tasks;
using MergeClaw3D.Scripts.Configs.Items;
using MergeClaw3D.Scripts.Items;
using System;
using System.Collections.Generic;
using DG.Tweening;
using MergeClaw3D.Scripts.Events.Models;
using UniRx;
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
            _itemPlacer.Initialize();
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
            
            if (_itemPlacesHolder.FreePlaceCount == 0) //TODO WHERE?
            {
                MessageBroker.Default.Publish(new ItemsEventsModels.AllPlacesOccupied());
            }
        }

        private async void MergeItems(List<ItemPlace> mergeList)
        {
            var targetItemPlace = mergeList[mergeList.Count/2];
            
            foreach (var place in mergeList)
            {
                place.OnMatched();
                if (place==mergeList[^1])
                    await MoveItemToMergePlace(place.ExtractItem(), targetItemPlace);
                else
                    MoveItemToMergePlace(place.ExtractItem(), targetItemPlace);
            }

            targetItemPlace.OnMerged();
            
            MessageBroker.Default.Publish(ItemsEventsModels.ItemsMerged.Create());
            
            if (_itemsContainer.ItemCount == 0)
            {
                MessageBroker.Default.Publish(new ItemsEventsModels.AllItemsMerged());
            }
        }

        private async UniTask MoveItemToMergePlace(ItemEntity item, ItemPlace mergePlace)
        {
            await item.Animator.MoveToPoint(mergePlace.Position, _mergeConfig.MergeDuration).AsyncWaitForCompletion();
            _itemsContainer.DestroyItem(item);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}