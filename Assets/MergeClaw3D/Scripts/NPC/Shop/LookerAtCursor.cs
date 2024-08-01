using UnityEngine;

namespace MergeClaw3D.Scripts.NPC.Shop
{
    public class LookerAtCursor : MonoBehaviour
    {
        [SerializeField] private Transform _targetTr;
        [SerializeField] private Transform _baseTr;
        [SerializeField] private float _angleThreshold =120f;
        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            Vector3 screenPos = _cam.WorldToScreenPoint(_targetTr.position);

            Vector3 direction = Input.mousePosition - screenPos;
            Vector3 targetDirection = new Vector3(direction.x, 0, direction.y);

            if (Vector3.Angle(_baseTr.forward, targetDirection)>=_angleThreshold)
            {
                _targetTr.rotation = Quaternion.Lerp(_targetTr.rotation, _baseTr.rotation, Time.deltaTime * 5f);
                return;
            }
            
            float angle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;

            var finalRot = Quaternion.Euler(0, angle, 0);
            _targetTr.rotation = Quaternion.Lerp(_targetTr.rotation, finalRot, Time.deltaTime * 5f);
        }
    }
}
