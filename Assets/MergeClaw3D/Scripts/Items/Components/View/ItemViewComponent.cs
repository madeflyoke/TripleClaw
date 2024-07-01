using Cysharp.Threading.Tasks;
using DG.Tweening;
using MergeClaw3D.Scripts.Items.Enums;
using UnityEngine;

namespace MergeClaw3D.Scripts.Items.Components.View
{
    public class ItemViewComponent : MonoBehaviour
    {
        [SerializeField] private ItemView _itemView;
        [SerializeField] private Rigidbody _rigidbody;

        private Tween _scaleTween;
        private ItemVelocityLimiter velocityLimiter;

        public void Initialize(Mesh mesh, ItemSize itemSize)
        {
            _itemView.SetupMesh(mesh);
            _itemView.SetCorrespondingSize(itemSize);

            velocityLimiter = new(_rigidbody);
        }

        public async void ScaleObject(float scale, float duration)
        {
            await ScaleObjectAsync(scale, duration);
        }

        public async UniTask ScaleObjectAsync(float scale, float duration)
        {
            _scaleTween?.Kill();
            _scaleTween = transform.parent.DOScale(scale, duration).
                SetUpdate(UpdateType.Fixed);

            await _scaleTween.AsyncWaitForCompletion();
        }
    }
}
