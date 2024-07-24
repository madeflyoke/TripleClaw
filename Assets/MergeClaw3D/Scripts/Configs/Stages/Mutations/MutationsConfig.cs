using System;
using System.Collections.Generic;
using System.Linq;
using MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Mutations;
using MergeClaw3D.Scripts.Stages.Mutations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Stages.Mutations
{
    [CreateAssetMenu(fileName = "MutationsConfig", menuName = "Stage/MutationsConfig")]
    public class MutationsConfig : SerializedScriptableObject
    {
        [OdinSerialize, ReadOnly] private Dictionary<Type, BaseStageMutation> MutationsPrefabsMap;

        [Button]
        public void AddNewElement(BaseStageMutationDataModule dataModule, BaseStageMutation prefab)
        {
            if (MutationsPrefabsMap==null)
            {
                MutationsPrefabsMap = new();
            }

            if (MutationsPrefabsMap.ContainsKey(dataModule.GetType()))
            {
                return;
            }
            MutationsPrefabsMap.Add(dataModule.GetType(), prefab);
        }

        public BaseStageMutation GetMutationPrefab(BaseStageMutationDataModule dataModule)
        {
            var type = dataModule.GetType();
            if (MutationsPrefabsMap.ContainsKey(type))
            {
                return MutationsPrefabsMap[type];
            }

            return null;
        }
    }
}
