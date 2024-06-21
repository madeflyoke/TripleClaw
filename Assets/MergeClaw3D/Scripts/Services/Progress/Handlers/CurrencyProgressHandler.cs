using System.Collections.Generic;
using System.Text;
using MergeClaw3D.Scripts.Currency.Enums;
using UnityEngine;

namespace MergeClaw3D.Scripts.Services.Progress.Handlers
{
    public class CurrencyProgressHandler
    {
        private readonly StringBuilder _stringBuilder;

        public CurrencyProgressHandler()
        {
            _stringBuilder = new StringBuilder();
        }
        
        public void SaveCurrency(Dictionary<CurrencyType, long> currencyMap)
        {
            foreach (var kvp in currencyMap)
            {
                SaveCurrency(kvp.Key, kvp.Value);
            }
        }
        
        public void SaveCurrency(CurrencyType type, long value)
        {
            PlayerPrefs.SetString(GetCurrencyKey(type), value.ToString());
        }

        public long ExtractCurrency(CurrencyType type)
        {
            var value = 0;
            int.TryParse(PlayerPrefs.GetString(GetCurrencyKey(type)), out value);
            return value;
        }
        
        private string GetCurrencyKey(CurrencyType type)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(ProgressSaveKeys.SAVED_CURRENCY_PREFIX);
            _stringBuilder.Append((int)type);
            return _stringBuilder.ToString();
        }
    }
}
