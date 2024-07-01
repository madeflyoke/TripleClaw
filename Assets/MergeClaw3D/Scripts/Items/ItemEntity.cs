using System;
using Cysharp.Threading.Tasks;
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

        private const float _SHOW_SCALE = 1f;
        
        public void Initialize(ItemConfigData configData, ItemSpecificationData specificationData)
        {
            Id = configData.Id;
            _itemViewComponent.Initialize(configData.Mesh, specificationData.ItemSize);
        }

        public void Show(float duration)
        {
            _itemViewComponent.ScaleObject(_SHOW_SCALE, duration);
        }

        public async UniTask ShowAsync(float duration)
        {
            await _itemViewComponent.ScaleObjectAsync(_SHOW_SCALE, duration);
        }

        public void Hide(float duration)
        {
            _itemViewComponent.ScaleObject(0f, duration);
        }

        public async UniTask HideAsync(float duration)
        {
            await _itemViewComponent.ScaleObjectAsync(0f, duration);
        }
    }
}
