using MergeClaw3D.Scripts.Configs.Stages.Data.Modules.Mutations;
using UnityEngine;

namespace MergeClaw3D.Scripts.Stages.Mutations
{
    public abstract class BaseStageMutation : MonoBehaviour
    {
        public abstract void Initialize(BaseStageMutationDataModule dataModule);
    }
}
