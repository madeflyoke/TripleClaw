using System;
using MergeClaw3D.Scripts.Configs.Stages.Data;
using MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Mutations;
using MergeClaw3D.Scripts.Configs.Stages.Mutations;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Stages.Mutations
{
    public class MutationsHandler : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        
        [SerializeField] private MutationsConfig _config;
        
        public void Initialize(StageData stageData)
        {
            foreach (var mutationDataModule in stageData.GetModules<IBaseStageMutationDataModule>())
            {
                BaseStageMutation prefab = _config.GetMutationPrefab(mutationDataModule);
                _diContainer.InstantiatePrefabForComponent<BaseStageMutation>(prefab, transform).Initialize(mutationDataModule);
            }
        }
    }
}
