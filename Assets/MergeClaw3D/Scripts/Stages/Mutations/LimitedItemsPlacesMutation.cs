using MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Mutations;
using MergeClaw3D.Scripts.Place;
using Zenject;

namespace MergeClaw3D.Scripts.Stages.Mutations
{
    public class LimitedItemsPlacesMutation : BaseStageMutation
    {
        [Inject] private ItemPlacesHolder _itemPlacesHolder;
        
        private LimitedItemsPlacesMutationData _mutationData;
        
        public override void Initialize(IBaseStageMutationDataModule dataModule)
        {
            _mutationData = dataModule as LimitedItemsPlacesMutationData;
            base.Initialize(dataModule);
        }

        protected override void ApplyMutation()
        {
            _itemPlacesHolder.SetPlacesState(_mutationData.DisabledPlacesCount, false);
        }

        protected override void RevertMutation()
        {
            _itemPlacesHolder.SetPlacesState(_mutationData.DisabledPlacesCount, true);
        }
    }
}
