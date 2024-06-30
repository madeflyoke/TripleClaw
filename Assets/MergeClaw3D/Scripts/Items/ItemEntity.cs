using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MergeClaw3D.Scripts.Configs.Items;
using MergeClaw3D.Scripts.Items.Components.View;
using MergeClaw3D.Scripts.Items.Data;
using UnityEngine;

namespace MergeClaw3D.Scripts.Items
{
    public class ItemEntity : MonoBehaviour
    {
        public event Action<ItemEntity> ItemSelected; 
        
        public int Id { get; private set; }
        
        [SerializeField] private ItemViewComponent _itemViewComponent;

        private Tween _scaleTween;
        private bool _lockVelocity = false;

        private void Update()
        {
            

            VelocityUpdate();
        }

        public void Initialize(ItemConfigData configData, ItemSpecificationData specificationData)
        {
            Id = configData.Id;
            _itemViewComponent.Initialize(configData.Mesh, specificationData.ItemSize);
        }

        public async void ScaleObject(float scale, float duration)
        {
            await ScaleObjectAsync(scale, duration);
        }

        public async UniTask ScaleObjectAsync(float scale, float duration)
        {
            _scaleTween?.Kill();
            _scaleTween = transform.DOScale(scale, duration).
                SetUpdate(UpdateType.Fixed);

            await _scaleTween.AsyncWaitForCompletion();
        }

        public void LockVelocityOnZero(bool lockVelocity)
        {
            _lockVelocity = lockVelocity;
        }

        private void VelocityUpdate()
        {
            if (_lockVelocity == false)
            {
                return;
            }

            _itemViewComponent.Rigidbody.velocity = Vector3.zero;
        }
    }
}
