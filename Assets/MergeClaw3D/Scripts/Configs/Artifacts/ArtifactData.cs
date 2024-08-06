using System;
using MergeClaw3D.Scripts.Currency.Enums;
using MergeClaw3D.Scripts.Inventory;
using MergeClaw3D.Scripts.Inventory.Enum;
using MergeClaw3D.Scripts.Inventory.Interfaces;
using UnityEngine.Rendering;

namespace MergeClaw3D.Scripts.Configs.Artifacts
{
    [Serializable]
    public class ArtifactData
    {
        public ArtifactType Type=> ArtifactOriginal.ArtifactType;
        public IArtifact ArtifactOriginal;
        public SerializedDictionary<CurrencyType, long> BasePricesMap;
    }
}
