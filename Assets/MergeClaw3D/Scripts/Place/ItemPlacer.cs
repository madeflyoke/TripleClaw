using MergeClaw3D.Scripts.Configs.Items;
using MergeClaw3D.Scripts.Events;
using MergeClaw3D.Scripts.Items;
using System;
using UniRx;

namespace MergeClaw3D.Scripts.Place
{
    public class ItemPlacer : IDisposable
    {
        private MergeConfig _mergeConfig;
        private ItemsContainer _itemsContainer;
        private ItemPlacesHolder _placesHolder;
        private CompositeDisposable _disposables = new();

        public Subject<ItemPlace> PlaceOccupied { get; private set; } = new();

        public ItemPlacer(ItemPlacesHolder placesHolder, ItemsContainer itemsContainer, MergeConfig mergeConfig)
        {
            _placesHolder = placesHolder;
            _itemsContainer = itemsContainer;
            _mergeConfig = mergeConfig;

            Subscribe();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        private void Subscribe()
        {
            MessageBroker.Default.Receive<ItemsSpawned>()
                .Subscribe(OnItemsSpawned)
                .AddTo(_disposables);

            MessageBroker.Default.Receive<ItemsMerged>()
                .Subscribe(OnItemsMerged)
                .AddTo(_disposables);
        }

        private void OnItemsSpawned(ItemsSpawned message)
        {
            foreach (var item in _itemsContainer.Items)
            {
                item.Selected.Subscribe(OnItemSelected)
                    .AddTo(_disposables);
            }
        }

        private void WakeUpAll()
        {
            foreach (var item in _itemsContainer.Items)
            {
                item.Rigidbody.WakeUp();
            }
        }

        private void OnItemSelected(ItemEntity item)
        {
            if (_placesHolder.FreePlaceCount == 0)
            {
                MessageBroker.Default.Publish(AllPlacesOccupied.Create());
                return;
            }

            item.SetInteractable(false);

            WakeUpAll();
            var place = _placesHolder.GetRightFreePlace();

            PutItemOnPlace(item, place, _mergeConfig.OccupationDuration);
        }


        private void OnItemsMerged(ItemsMerged message)
        {
            RemoveSpaceBetweenPlaces();
        }

        private void RemoveSpaceBetweenPlaces()
        {
            if (_placesHolder.OccupiedPlaceCount == 0)
            {
                return;
            }

            var freePlacesSpaceAchieved = false;
            var occupiedPlaceIndexToCorrect = int.MaxValue;

            foreach (var place in _placesHolder.Places)
            {
                if (place.State == PlaceState.Occupied && freePlacesSpaceAchieved == false)
                {
                    continue;
                }
                else if (place.State == PlaceState.Occupied)
                {
                    occupiedPlaceIndexToCorrect = _placesHolder.GetPlaceIndex(place);
                    break;
                }

                freePlacesSpaceAchieved = true;
            }

            for (var i = occupiedPlaceIndexToCorrect; i < _placesHolder.PlacesCount; i++)
            {
                var place = _placesHolder.GetPlace(i);

                if (place.State == PlaceState.Free)
                {
                    return;
                }

                PutItemOnPlace(
                    place.ExtractItem(),
                    _placesHolder.GetLeftFreePlace(), 
                    _mergeConfig.SpaceCorrectionDuration);
            }
        }

        private async void PutItemOnPlace(ItemEntity item, ItemPlace place, float duration)
        {
            place.SetItem(item);

            //TODO ERROR above on fast clicks
            
            await item.Animator.MoveToPointAsync(place.Position, _mergeConfig.OccupationDuration);

            if (place.State != PlaceState.Occupied || place.Item != item)
            {
                return;
            }

            PlaceOccupied?.OnNext(place);
        }
    }
}