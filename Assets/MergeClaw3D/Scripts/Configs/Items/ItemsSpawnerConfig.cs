using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Items
{
    [CreateAssetMenu(fileName = "ItemsSpawnerConfig", menuName = "Items/ItemsSpawnerConfig")]
    public class ItemsSpawnerConfig : ScriptableObject
    {
        [PropertySpace]
        [MinValue(0)]
        [SerializeField] private float _itemScaleDuration;
        [MinValue(0)]
        [SerializeField] private float _delayBeforeEachPackSpawn;

        public float ItemScaleDuration => _itemScaleDuration;
        public float DelayBeforeEachPackSpawn => _delayBeforeEachPackSpawn;
    }
}