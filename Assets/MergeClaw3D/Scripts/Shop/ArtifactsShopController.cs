using System.Collections.Generic;
using MergeClaw3D.Scripts.Configs.Artifacts;
using MergeClaw3D.Scripts.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.Shop
{
    public class ArtifactsShopController : MonoBehaviour
    {
        [SerializeField] private List<ArtifactShopSpot> _artifactsSpots;
        [SerializeField] private InventoryArtifactsConfig _artifactsConfig;
        private List<ArtifactShopSpot> _currentSpots;
    
        
        [Button]
        public void Initialize()
        {
            var allArtifacts = _artifactsConfig.GetAllMutationsArtifactsData().Shuffle();

            for (int i = 0; i < allArtifacts.Count; i++)
            {
                var artifactData = allArtifacts[i];
                var spot = _artifactsSpots[i];
                spot.Initialize(artifactData);
            }
        }
    }
}
