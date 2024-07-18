using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Items
{
    [CreateAssetMenu(fileName = "MergeConfig", menuName = "Merge/MergeConfig")]
    public class MergeConfig : ScriptableObject
    {
        [Title("Merge Settings")]
        [SerializeField] private int _itemNeedToMerge;

        [Title("Animations Settings")]
        [SerializeField] private float _occupationDuration;
        [SerializeField] private float _spaceCorrectionDuration;
        [SerializeField] private float _mergeDuration;

        public int ItemNeedToMerge => _itemNeedToMerge;

        public float OccupationDuration => _occupationDuration;
        public float SpaceCorrectionDuration => _spaceCorrectionDuration;
        public float MergeDuration => _mergeDuration;
    }
}