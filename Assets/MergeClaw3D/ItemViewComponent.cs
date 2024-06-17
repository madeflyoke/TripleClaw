using System;
using System.Collections.Generic;
using MergeClaw3D.MergeClaw3D.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.MergeClaw3D
{
    public class ItemViewComponent : MonoBehaviour
    {
        //test
        [SerializeField] private List<ItemView> _allViews;
        private int index;
        private ItemView _currentItemView;
        
        [Button]
        public void SetNextView(ItemSize size) 
        {
            if (_currentItemView!=null)
            {
                Destroy(_currentItemView.gameObject);   
            }
            _currentItemView = Instantiate(_allViews[index % _allViews.Count], transform).SetCorrespondingSize(size);
            index++;
        }
        //test
    }
}
