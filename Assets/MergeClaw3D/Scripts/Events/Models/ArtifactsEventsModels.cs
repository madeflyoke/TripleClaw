using MergeClaw3D.Scripts.Inventory.Enum;
using UnityEngine;

namespace MergeClaw3D.Scripts.Events.Models
{
    public static class ArtifactsEventsModels
    {
        public class ArtifactApplied
        {
            public readonly ArtifactType Type;

            public ArtifactApplied(ArtifactType type)
            {
                Type = type;
            }
        }
    }
}
