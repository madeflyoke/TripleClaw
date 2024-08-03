using MergeClaw3D.Scripts.Currency.Enums;
using UnityEngine;
using UnityEngine.Rendering;

namespace MergeClaw3D.Scripts.Configs.Currency
{
    [CreateAssetMenu(fileName = "CurrencyConfig", menuName = "Currency/CurrencyConfig")]
    public class CurrencyConfig : ScriptableObject
    {
        [SerializeField] private SerializedDictionary<CurrencyType, Sprite> _sprites;

        public Sprite GetCurrencySprite(CurrencyType type)
        {
            return _sprites[type];
        }
    }
}
