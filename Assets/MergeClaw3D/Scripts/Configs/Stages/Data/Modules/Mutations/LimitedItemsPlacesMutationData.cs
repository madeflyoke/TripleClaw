using MergeClaw3D.Scripts.Items.Utility;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Mutations
{
    public class LimitedItemsPlacesMutationData : BaseStageMutationDataModule
    {
        public int DisabledPlacesCount;
        
        public override void ManualValidate()
        {
            DisabledPlacesCount = Mathf.Clamp(DisabledPlacesCount, 1, ItemConstants.ITEMS_PLACES_COUNT-1);
        }
    }
}
