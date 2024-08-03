using System;
using System.Collections.Generic;
using System.Linq;
using MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Interfaces;
using MergeClaw3D.Scripts.Currency;
using MergeClaw3D.Scripts.Currency.Enums;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Rendering;

namespace MergeClaw3D.Scripts.Configs.Stages.Data
{
    [Serializable]
    public class ItemsStageData : StageData
    {
        public override string SceneName => "ItemsStage";
        
        [OdinSerialize] public List<IStageDataModule> Modules;
        [SerializeField] public SerializedDictionary<CurrencyType, long> CurrencyPerMerge;

        public override T GetModule<T>()
        {
            return (T)Modules.FirstOrDefault(x => x.GetType() == typeof(T));
        }

        public override IEnumerable<T> GetModules<T>()
        {
            return Modules.OfType<T>();
        }
        
#if UNITY_EDITOR
        
        public override void ManualValidate()
        {
            base.ManualValidate();
            if (Modules!=null)
            {
                Modules.ForEach(x=>x.ManualValidate());
            }
        }
        
#endif
    }
}