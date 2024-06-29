using System.Collections.Generic;
using UnityEngine;

namespace MergeClaw3D.Scripts.NPC
{
    public static class AnimationConstants
    {
        #region States
        
        public const string IDLE_STATE = "Idle";
        public const string RUN_STATE = "Run";

        public static List<string> GetAllStatesNames()
        {
            return new List<string>()
            {
                IDLE_STATE,
                RUN_STATE
            };
        }
        
        #endregion
     

        #region Parameters

        public const string SPEED_PARAMETER = "SpeedParameter";

        #endregion
    }
}
