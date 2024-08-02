using MergeClaw3D.Scripts.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MergeClaw3D.Scripts.Shop
{
    public class ArtifactShopSpot : MonoBehaviour
    {
        
        [SerializeField] private Button _buyButton;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private Transform _artifactParent;
        
        public void Initialize(long price, MutationArtifact artifactPrefab)
        {
            _price.text = price.ToString();
            Instantiate(artifactPrefab, _artifactParent);
        }
    }
}
