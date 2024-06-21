using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.MergeClaw3D.Items.View
{
    public class ItemViewComponent : MonoBehaviour
    {
        [SerializeField] private ItemView _itemView;
        
        //test
        [SerializeField] private List<Mesh> _allViews;
        private int index;
        
        [Button]
        public void SetNextView() 
        {
            _itemView.SetMesh(_allViews[index%_allViews.Count]);
            index++;
        }
        //test
    }
}
