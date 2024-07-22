using MergeClaw3D.Scripts.Configs.Stages.Data;
using MergeClaw3D.Scripts.Spawner;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Stages.Variants
{
    public class ItemsStage : MonoBehaviour, IStage
    {
        public StageData StageData { get; private set; }
        private ItemsSpawner _itemsSpawner;
        
        [Inject]
        public void Construct(ItemsSpawner itemsSpawner)
        {
            _itemsSpawner = itemsSpawner;
        }
        
        public void Initialize(StageData stageData)
        {
            StageData = stageData;
            _itemsSpawner.Spawn(stageData as ItemsStageData);
        }
    }
}