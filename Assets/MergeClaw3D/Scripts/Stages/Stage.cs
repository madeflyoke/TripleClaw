using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MergeClaw3D.Scripts.Configs.Stages;
using MergeClaw3D.Scripts.Items;
using MergeClaw3D.Scripts.Spawner;
using UnityEngine;

namespace MergeClaw3D.Scripts.Stages
{
    public class Stage
    {
        private readonly StageData _stageData;
        
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
                
           }
        }

        private void OnItemSelected(ItemEntity item)
        {
            
        }
    }
}
