using System;
using System.Collections.Generic;
using System.Linq;
using MergeClaw3D.Scripts.NPC;
using MergeClaw3D.Scripts.NPC.Interfaces;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.NPC
{
    [CreateAssetMenu(menuName = "NPC/AnimationSpeed", fileName = "AnimationSpeedConfig")]
    
    public class AnimationSpeedConfig : SerializedScriptableObject
    {
        [OdinSerialize] private List<AnimationSpeedData> _animationSpeedDatas;
        
#if UNITY_EDITOR
        [OdinSerialize, ValueDropdown(nameof(StatesDropDown)), DisplayAsString, Space(10),BoxGroup("Hint")]
        private string EDITOR_StatesNamesPreview;
        private List<string> StatesDropDown => AnimationConstants.GetAllStatesNames();
#endif
        
        public Dictionary<string, float> GetStatesSpeed(IAnimatedEntity receiver)
        {
           return _animationSpeedDatas.FirstOrDefault(x => x.EntityType.GetType()==receiver.GetType())?.StateByValue;
        }
        
        [Serializable]
        public class AnimationSpeedData
        {
            [OdinSerialize] public IAnimatedEntity EntityType;
            [OdinSerialize] public Dictionary<string, float> StateByValue;
        }
    }
}
