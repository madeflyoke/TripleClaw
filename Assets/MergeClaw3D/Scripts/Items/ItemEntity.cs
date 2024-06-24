using MergeClaw3D.Scripts.Configs.Items;
using MergeClaw3D.Scripts.Items.Components.View;
using MergeClaw3D.Scripts.Items.Data;
using UnityEngine;

namespace MergeClaw3D.Scripts.Items
{
    public class ItemEntity : MonoBehaviour
    {
        public int Id { get; private set; }
        
        [SerializeField] private ItemViewComponent _itemViewComponent;
        
        public void Initialize(ItemConfigData configData, ItemSpecificationData specificationData)
        {
            Id = configData.Id;
            _itemViewComponent.Initialize(configData.Mesh, specificationData.ItemSize);
        }
    }
}
