using Cysharp.Threading.Tasks;
using MergeClaw3D.Scripts.Configs.Items;
using MergeClaw3D.Scripts.Items.Animation;
using MergeClaw3D.Scripts.Items.Components;
using MergeClaw3D.Scripts.Items.Data;
using MergeClaw3D.Tools.Outline.HighlightPlus.Runtime.Scripts;
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

        private ItemAnimator _animator;
        private bool _alreadySelected;
        private ItemVelocityLimiter _itemVelocityLimiter;

        public ItemView View => _itemView;
        public ItemAnimator Animator => _animator;
        public Rigidbody Rigidbody => _rigidbody;
        public Subject<ItemEntity> Selected { get; private set; } = new();

        private void OnDestroy()
        {
            _selectionHandler.Selected -= OnItemSelected;
        }

        public void Initialize(ItemConfigData configData, ItemSpecificationData specificationData)
        {
            Id = configData.Id;

            _animator = new(this);

            _itemView.Initialize(configData.Mesh, specificationData.ItemSize);
            _selectionHandler.Initialize();
            _selectionHandler.Selected += OnItemSelected;

            SetPhysicsMode(true);
            SetSelectableMode(false);

            ActivateVelocityLimiter();
        }

        public ItemEntity SetSelectableMode(bool selectable)
        {
            _selectionHandler.ChangeMode(selectable);

            return this;
        }

        private ItemEntity SetPhysicsMode(bool enable)
        {
            _rigidbody.isKinematic = !enable;
            _rigidbody.gameObject.layer = enable ? ItemConstants.DEFAULT_ITEM_LAYER : ItemConstants.IGNORED_ITEM_LAYER;

            return this;
        }

        public void SetInteractable(bool isInteractable)
        {
            SetSelectableMode(isInteractable);
            SetPhysicsMode(isInteractable);
            if (isInteractable==false)
            {
                if (_itemVelocityLimiter!=null)
                {
                    _itemVelocityLimiter.Dispose();
                    _itemVelocityLimiter = null;
                }
            }
        }
        
        private async void ActivateVelocityLimiter()
        {
            _itemVelocityLimiter = new ItemVelocityLimiter(_rigidbody);

            await UniTask.WaitUntil(() => _itemVelocityLimiter.LimiterCompleted);
        }

        private void OnItemSelected()
        {
            if (_alreadySelected)
            {
                return;
            }

            _alreadySelected = true;

            Selected?.OnNext(this);
        }
    }
}
