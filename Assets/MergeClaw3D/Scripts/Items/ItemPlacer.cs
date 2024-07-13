using DG.Tweening;
using MergeClaw3D.Scripts.Events;
using MergeClaw3D.Scripts.Merge;
using System;
using UniRx;
using UnityEngine;

namespace MergeClaw3D.Scripts.Items
{
    public class ItemPlacer : IDisposable
    {
        private ItemsContainer _itemsContainer;
        private ItemPlacesHolder _placesHolder;
        private CompositeDisposable _disposables = new();

        public ItemPlacer(ItemPlacesHolder placesHolder, ItemsContainer itemsContainer)
        {
            _placesHolder = placesHolder;
            _itemsContainer = itemsContainer;

            Subscribe();
        }

        public void Dispose()
        {
            Unsubscribe();

            _disposables.Dispose();
        }

        private void Subscribe()
        {
            MessageBroker.Default.Receive<ItemsSpawned>()
                .Subscribe(OnItemsSpawned)
                .AddTo(_disposables);
        }

        private void Unsubscribe()
        {

        }

        private void OnItemsSpawned(ItemsSpawned message)
        {
            foreach (var item in _itemsContainer.Items)
            {
                item.Selected.Subscribe(OnItemSelected)
                    .AddTo(_disposables);
            }
        }

        private void OnItemSelected(ItemEntity itemEntity)
        {
            if (_placesHolder.FreePlaceCount == 0)
            {
                MessageBroker.Default.Publish(AllPlacesOccupied.Create());
                return;
            }

            var freePlace = _placesHolder.GetFreePlace();
            freePlace.SetItem(itemEntity);

            MoveToPlace(freePlace, 0.7f);
        }

        private void MoveToPlace(ItemPlace place, float duration)
        {
            var rigidbody = place.Item.Rigidbody;

            rigidbody.isKinematic = true;
            rigidbody.gameObject.layer = 8;

            DOTween.Sequence()
                .Join(rigidbody.DOMove(place.transform.position, duration))
                .Join(rigidbody.DORotate(Vector3.zero, duration))
                .Play()
                .SetLink(rigidbody.gameObject);
        }
    }
}