using System;
using MergeClaw3D.Scripts.Currency.Enums;
using MergeClaw3D.Scripts.Inventory;
using MergeClaw3D.Scripts.Inventory.Enum;
using UnityEngine.Rendering;

namespace MergeClaw3D.Scripts.Configs.Artifacts
{
    [Serializable]
    public class ArtifactData
    {
        public ArtifactType Type;
        public IArtifact ArtifactPrefab;
        public SerializedDictionary<CurrencyType, long> BasePricesMap;
    }
}
