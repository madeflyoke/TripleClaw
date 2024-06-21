using MergeClaw3D.Scripts.Items.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.Items.View
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;
        private MeshCollider _currentCollider;
        
        public void SetMesh(Mesh mesh)
        {
            _meshFilter.sharedMesh = mesh;
            if (_currentCollider!=null) //TODO Mesh collider manipulation there
            {
                DestroyImmediate(_currentCollider);
            }

            _currentCollider = gameObject.AddComponent<MeshCollider>(); 
            _currentCollider.convex = true;
        }
        
        [Button]
        public ItemView SetCorrespondingSize(ItemSize itemSize)
        {
            var finalScale = Vector3.one; //default one
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

            transform.localScale = finalScale;
            return this;
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
