using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Items
{
    [CreateAssetMenu(fileName = "ItemsSpawnerConfig", menuName = "Items/ItemsSpawnerConfig")]
    public class ItemsSpawnerConfig : ScriptableObject
    {
        [LabelText("Object count for per spawn")]
        [MinValue(1)]
        [SerializeField] private int _packCount = 1;

        [PropertySpace]
        [MinValue(0)]
        [SerializeField] private float _itemScaleDuration;
        [MinValue(0)]
        [SerializeField] private float _delayBeforeEachPackSpawn;

        public int PackCount => _packCount;
        public float ItemScaleDuration => _itemScaleDuration;
        public float DelayBeforeEachPackSpawn => _delayBeforeEachPackSpawn;
    }
}