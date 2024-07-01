using System.Collections.Generic;
using MergeClaw3D.Scripts.Configs.Stages;
using MergeClaw3D.Scripts.Items;
using MergeClaw3D.Scripts.Spawner;
using UnityEngine;

namespace MergeClaw3D.Scripts.Stages
{
    public class Stage
    {
        private readonly StageData _stageData;
        private readonly ItemsSpawner _itemsSpawner;
        
        private List<ItemEntity> _activeItems;//TODO Here?
        
        public Stage(StageData stageData)
        {
            _stageData = stageData;
        }

        public void Launch()
        {
           //_activeItems = //spawner spawn

           foreach (var item in _activeItems)
           {
               item.ItemSelected += OnItemSelected;
           }
        }

        private void OnItemSelected(ItemEntity item)
        {
            
        }
    }
}