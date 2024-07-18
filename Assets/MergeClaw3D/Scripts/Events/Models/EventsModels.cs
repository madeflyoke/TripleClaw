using MergeClaw3D.Scripts.Place;
using System.Collections.Generic;

namespace MergeClaw3D.Scripts.Events
{
    public class ItemsSpawned
    {
        public static ItemsSpawned Create() => new();
    }

    public class AllPlacesOccupied
    {
        public static AllPlacesOccupied Create() => new();
    }

    public class ItemsMerged
    {
        public static ItemsMerged Create() => new();
    }

    public class AllItemsMerged
    {
        public static AllItemsMerged Create() => new();
    }
}