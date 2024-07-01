using UniRx;
using UnityEngine;

namespace MergeClaw3D.Scripts.Items.Components
{
    public class ItemVelocityLimiter
    {
        private const float VELOCITY_UP_LIMIT = 0.6f;
        private const float VELOCITY_DOWN_LIMIT = -0.6f;

        private Rigidbody _rigidbody;
        private Vector3 _velocity;

        public ItemVelocityLimiter(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
            _velocity = Vector3.zero;

            Observable.EveryFixedUpdate()
                .Subscribe(_ => FixedUpdate())
                .AddTo(_rigidbody);
        }

        private void FixedUpdate()
        {
            _velocity = _rigidbody.velocity;
            _velocity.x = Mathf.Clamp(_velocity.x, VELOCITY_DOWN_LIMIT, VELOCITY_UP_LIMIT);
            _velocity.y = _velocity.y > VELOCITY_UP_LIMIT ? VELOCITY_UP_LIMIT : _velocity.y;
            _velocity.z = Mathf.Clamp(_velocity.z, VELOCITY_DOWN_LIMIT, VELOCITY_UP_LIMIT);
            _rigidbody.velocity = _velocity;
        }
    }
}