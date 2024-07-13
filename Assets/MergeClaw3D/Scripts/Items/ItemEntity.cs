using Cysharp.Threading.Tasks;
using DG.Tweening;
using MergeClaw3D.Scripts.Configs.Items;
using MergeClaw3D.Scripts.Items.Components;
using MergeClaw3D.Scripts.Items.Data;
using MergeClaw3D.Tools.Outline.HighlightPlus.Runtime.Scripts;
using System;
using UniRx;
using UnityEngine;

namespace MergeClaw3D.Scripts.Items
{
    public class ItemEntity : MonoBehaviour
    {
        public int Id { get; private set; }
        
        [SerializeField] private ItemView _itemView;
        [SerializeField] private SelectionHandler _selectionHandler;
        
        [SerializeField] private Rigidbody _rigidbody;

        public Rigidbody Rigidbody => _rigidbody;
        public Tween ShowTween => _itemView.ScaleTween;
        public Subject<ItemEntity> Selected { get; private set; } = new();

        private void OnDestroy()
        {
            _selectionHandler.Selected -= OnItemSelected;
        }

        public void Initialize(ItemConfigData configData, ItemSpecificationData specificationData)
        {
            Id = configData.Id;
            
            _itemView.Initialize(configData.Mesh, specificationData.ItemSize);
            _selectionHandler.Initialize();
            _selectionHandler.Selected += OnItemSelected;

            ActivateVelocityLimiter();
        }

        public void Show(float duration)
        {
            _itemView.SetActive(true);
            _itemView.ScaleObject(_itemView.DefaultScale, duration);
        }

        public async UniTask ShowAsync(float duration)
        {
            _itemView.SetActive(true);

            await _itemView.ScaleObjectAsync(_itemView.DefaultScale, duration);
        }
        
        public void Hide(float duration)
        {
            _itemView.ScaleObject(0f, duration);
            _itemView.SetActive(false);
        }
        
        public async UniTask HideAsync(float duration)
        {
            await _itemView.ScaleObjectAsync(0f, duration);

            _itemView.SetActive(false);
        }

        private async void ActivateVelocityLimiter()
        {
            var velocityLimiter = new ItemVelocityLimiter(_rigidbody);

            await UniTask.WaitUntil(() => velocityLimiter.LimiterCompleted);

            velocityLimiter = null;
        }

        private void OnItemSelected()
        {
            Selected?.OnNext(this);
        }
    }
}
