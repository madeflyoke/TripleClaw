using MergeClaw3D.Scripts.Items.Enums;
using MergeClaw3D.Scripts.Items.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.Items.Components
{
    public class ItemView : MonoBehaviour
    {
        public float DefaultScale { get; private set; }

        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private PhysicMaterial _physicMaterial;

        private MeshCollider _currentCollider; //need?
        
        
        public void Initialize(Mesh mesh, ItemSize itemSize)
        {
            SetupMesh(mesh);
            SetCorrespondingSize(itemSize);
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
        
        private void SetupMesh(Mesh mesh)
        {
            _meshFilter.sharedMesh = mesh;
            if (_currentCollider == null)
            {
                _currentCollider = gameObject.AddComponent<MeshCollider>();
                _currentCollider.material = _physicMaterial;
                _currentCollider.convex = true;
            }
        }

        private void SetCorrespondingSize(ItemSize itemSize)
        {
            var finalScale = 1f; //default one
            switch (itemSize)
            {
                case ItemSize.SMALL:
                    finalScale *= ItemConstants.SMALL_ITEM_SCALE;
                    break;
                case ItemSize.MEDIUM:
                    finalScale *= ItemConstants.MEDIUM_ITEM_SCALE;
                    break;
                case ItemSize.LARGE:
                    finalScale *= ItemConstants.LARGE_ITEM_SCALE;
                    break;
            }

            transform.localScale = Vector3.one * finalScale;
            DefaultScale = finalScale;
        }

#if UNITY_EDITOR

        [SerializeField] private ItemSize EDITOR_itemSize;

        private void OnValidate()
        {
            SetCorrespondingSize(EDITOR_itemSize);
        }

#endif
    }
}
