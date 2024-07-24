using MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Mutations;
using MergeClaw3D.Scripts.Place;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Stages.Mutations
{
    public class LimitedItemsPlacesMutation : BaseStageMutation
    {
        [Inject] private ItemPlacesHolder _itemPlacesHolder;
        
        private LimitedItemsPlacesMutationData _mutationData;
        
        public override void Initialize(BaseStageMutationDataModule dataModule)
        {
            _mutationData = dataModule as LimitedItemsPlacesMutationData;
            _itemPlacesHolder.SetPlacesState(_mutationData.DisabledPlacesCount, false);
        }
    }
}
