using MergeClaw3D.Scripts.Configs.Items;
using MergeClaw3D.Scripts.Events;
using MergeClaw3D.Scripts.Items;
using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MergeClaw3D.Scripts.Place
{
    public class ItemPlacer : IDisposable
    {
        private MergeConfig _mergeConfig;
        private ItemsContainer _itemsContainer;
        private ItemPlacesHolder _placesHolder;
        private CompositeDisposable _disposables = new();
        private Camera _mainCam;
        private Camera _overlayCam;
        
        public Subject<ItemPlace> PlaceOccupied { get; private set; } = new();

        public ItemPlacer(ItemPlacesHolder placesHolder, ItemsContainer itemsContainer, MergeConfig mergeConfig)
        {
            _placesHolder = placesHolder;
            _itemsContainer = itemsContainer;
            _mergeConfig = mergeConfig;

            Subscribe();
        }

        public void Initialize()
        {
            _mainCam = Camera.main;
            _overlayCam = _mainCam.GetUniversalAdditionalCameraData().cameraStack[0];
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

            if (_placesHolder.IsAnyEmptySpaceBeforeOccupiedItem())
            {
                RemoveSpaceBetweenPlaces();
            }

            CorrectPositionsByCamera(item);
            
            item.SetInteractable(false);

            WakeUpAll();
            var place = _placesHolder.GetRightFreePlace();

            PutItemOnPlace(item, place, true);
        }

        private void CorrectPositionsByCamera(ItemEntity item)
        {
            Vector3 perspectiveScreenPosition = _mainCam.WorldToScreenPoint(item.transform.position);
            Vector3 orthographicWorldPosition = _overlayCam.ScreenToWorldPoint(perspectiveScreenPosition);
            Vector3 positionDifference = orthographicWorldPosition - item.transform.position;
            item.transform.position += positionDifference;
        }

        private void OnItemsMerged(ItemsMerged message)
        {
            RemoveSpaceBetweenPlaces();
        }

        private void RemoveSpaceBetweenPlaces()
        {
            if (_placesHolder.OccupiedPlaceCount == 0
                || _placesHolder.IsAnyEmptySpaceBeforeOccupiedItem() == false)
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
                    false);
            }
        }
        
        private async void PutItemOnPlace(ItemEntity item, ItemPlace place, bool fromHeap)
        {
            place.SetItem(item);
            
            if (fromHeap)
            {
                await PlayPlacingFromHeapAnimation(item, place);
            }
            else
            {
                await PlayPlacingFromAnotherPlace(item, place);
            }
            
            if (place.State != PlaceState.Occupied || place.Item != item)
            {
                return;
            }

            PlaceOccupied?.OnNext(place);
        }
        
        private async UniTask PlayPlacingFromAnotherPlace(ItemEntity item, ItemPlace place)
        {
            await item.Animator.MoveToPoint(place.Position, _mergeConfig.SpaceCorrectionDuration).AsyncWaitForCompletion();
        }

        
        private async UniTask PlayPlacingFromHeapAnimation(ItemEntity item, ItemPlace place)
        {
            var seq = item.Animator.GetSequence();
            await seq.Append(item.Animator.MoveToPoint(place.Position, _mergeConfig.OccupationDuration))
                .Join(item.Animator.RotateObject(_mergeConfig.OccupatedRotation,  _mergeConfig.OccupationDuration))

                .Join(item.Animator.ScaleObject(_mergeConfig.PreviewScale,  _mergeConfig.OccupationDuration/2))
                .Join(item.Animator.ScaleObject(_mergeConfig.OccupatedScale,  _mergeConfig.OccupationDuration/2).SetDelay(_mergeConfig.OccupationDuration/2+0.1f))
                .SetEase(Ease.Linear)
                .AsyncWaitForCompletion();
        }
    }
}