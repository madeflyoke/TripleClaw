using MergeClaw3D.Scripts.Items.Enums;
using UnityEngine;

namespace MergeClaw3D.Scripts.Items.Components.View
{
    public class ItemViewComponent : MonoBehaviour
    {
        [SerializeField] private ItemView _itemView;
        [SerializeField] private Rigidbody _rigidbody;

        public Rigidbody Rigidbody => _rigidbody;

        public void Initialize(Mesh mesh, ItemSize itemSize)
        {
            _itemView.SetupMesh(mesh);
            _itemView.SetCorrespondingSize(itemSize);
        }
    }
}
