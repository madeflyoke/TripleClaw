using TMPro;
using UnityEngine;

namespace MergeClaw3D.Scripts.Currency
{
    public class CurrencyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _valueText;
        
        public void SetValueText(long value)
        {
            _valueText.text = value.ToString();
        }
    }
}
