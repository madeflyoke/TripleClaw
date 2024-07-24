using MergeClaw3D.Scripts.Configs.Stages.Data;
using MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Mutations;
using MergeClaw3D.Scripts.Spawner;
using MergeClaw3D.Scripts.Stages.Mutations;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Stages.Variants
{
    public class ItemsStage : MonoBehaviour, IStage
    {
        [SerializeField] private MutationsHandler _mutationsHandler;
        
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
            _mutationsHandler.Initialize(stageData);
            _itemsSpawner.Spawn(stageData as ItemsStageData);
        }

       
    }
}