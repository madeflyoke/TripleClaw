using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;

namespace MergeClaw3D.Scripts.Items.Components
{
    public class ItemVelocityLimiter
    {
        private const float VELOCITY_UP_LIMIT = 1f;
        private const float VELOCITY_DOWN_LIMIT = -1f;

        private Rigidbody _rigidbody;
        private IDisposable disposable;

        private Vector3 _velocity;
        private float _timeStart;

        public bool LimiterCompleted { get; private set; }

        public ItemVelocityLimiter(Rigidbody rigidbody)
        {
            _timeStart = Time.realtimeSinceStartup;

            _rigidbody = rigidbody;
            _velocity = Vector3.zero;

            disposable = Observable.EveryFixedUpdate()
                .Subscribe(_ => FixedUpdate())
                .AddTo(_rigidbody);
        }

        private void FixedUpdate()
        {
            if (_rigidbody.velocity.magnitude < 0.01f)
            {
                LimiterCompleted = true;

                disposable.Dispose();
                return;
            }

            _velocity = _rigidbody.velocity;
            _velocity.x = Mathf.Clamp(_velocity.x, VELOCITY_DOWN_LIMIT, VELOCITY_UP_LIMIT);
            _velocity.y = _velocity.y > VELOCITY_UP_LIMIT ? VELOCITY_UP_LIMIT : _velocity.y;
            _velocity.z = Mathf.Clamp(_velocity.z, VELOCITY_DOWN_LIMIT, VELOCITY_UP_LIMIT);
            _rigidbody.velocity = _velocity;
        }
    }
}