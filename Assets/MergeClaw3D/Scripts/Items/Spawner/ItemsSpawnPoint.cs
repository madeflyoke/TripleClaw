using UnityEngine;

namespace MergeClaw3D.Scripts.Items.Spawner
{
    public class ItemsSpawnPoint : MonoBehaviour
    {
        [SerializeField] private float squareSide;

        public float SquareSide => squareSide;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, new(squareSide, 0f, squareSide));
        }
    }
}