using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace MergeClaw3D.Scripts.Items.Animation
{
    public class ItemAnimator
    {
        private Tween _moveTween;
        private ItemEntity _item;

        public Tween MoveTween => _moveTween;
        public Tween ShowTween => _item.View.ScaleTween;

        public ItemAnimator(ItemEntity item)
        {
            _item = item;
        }

        public void Show(float duration)
        {
            _item.View.SetActive(true);
            _item.View.ScaleObject(_item.View.DefaultScale, duration);
        }

        public async UniTask ShowAsync(float duration)
        {
            _item.View.SetActive(true);

            await _item.View.ScaleObjectAsync(_item.View.DefaultScale, duration);
        }

        public async void MoveToPoint(Vector3 position, float duration)
        {
            await MoveToPointAsync(position, duration);
        }

        public async UniTask MoveToPointAsync(Vector3 position, float duration)
        {
            if (_moveTween.IsActive())
            {
                _moveTween.Kill();
            }

            _moveTween = _item.Rigidbody.DOMove(position, duration)
                .Play()
                .SetLink(_item.Rigidbody.gameObject);

            await _moveTween.AsyncWaitForCompletion();
        }
    }
}