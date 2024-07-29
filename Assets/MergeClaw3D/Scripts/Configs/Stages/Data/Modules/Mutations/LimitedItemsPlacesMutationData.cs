using MergeClaw3D.Scripts.Items.Utility;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Mutations
{
    public class LimitedItemsPlacesMutationData : IBaseStageMutationDataModule
    {
        [SerializeField, Range(1,ItemConstants.ITEMS_PLACES_COUNT-1)] public int DisabledPlacesCount;

        public void ManualValidate()
        {
        }
    }
}
