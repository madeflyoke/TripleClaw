using DG.Tweening;
using UnityEngine;

namespace MergeClaw3D.Scripts.Items.Animation
{
    public class ItemAnimator
    {
        private readonly ItemEntity _item;

        public Tween ShowTween => _scaleTween;
        
        private Tween _scaleTween;
        private Tween _rotationTween;
        private Tween _moveTween;

        private Sequence _sequence;
        
        public ItemAnimator(ItemEntity item)
        {
            _item = item;
        }

        public Tween Show(float duration)
        {
            _item.View.SetActive(true);
            return ScaleObject(_item.View.DefaultScale, duration);
        }
        
        public Tween MoveToPoint(Vector3 position, float duration)
        {
            if (_moveTween.IsActive())
            {
                _moveTween.Kill();
            }

            _moveTween = _item.Rigidbody.DOMove(position, duration).SetEase(Ease.Linear).SetUpdate(UpdateType.Fixed);
            
            return _moveTween;
        }
        
        public Tween RotateObject(Vector3 rot, float duration)
        {
            _rotationTween?.Kill();
            _rotationTween = _item.Rigidbody.DORotate(rot, duration).
                SetUpdate(UpdateType.Fixed).SetEase(Ease.Linear);

            return _rotationTween;
        }
        
        public Tween ScaleObject(float scale, float duration)
        {
            _scaleTween?.Kill();
            _scaleTween = _item.transform.DOScale(scale, duration).
                SetUpdate(UpdateType.Fixed).SetEase(Ease.Linear);

            return _scaleTween;
        }

        public Sequence GetSequence()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            return _sequence;
        }

        public void Dispose()
        {
            _rotationTween?.Kill();
            _scaleTween?.Kill();
            _moveTween?.Kill();
        }
    }
}