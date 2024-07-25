namespace MergeClaw3D.Scripts.Events.Models
{
    public static class ItemsEventsModels
    {
        public class ItemsSpawned
        {
            public static ItemsSpawned Create() => new();
        }
    
        public class ItemsMerged
        {
            public static ItemsMerged Create() => new();
        }

        public class AllItemsMerged
        {
            public static AllItemsMerged Create() => new();
        }
    
        public class AllPlacesOccupied
        {
            public static AllPlacesOccupied Create() => new();
        }
    }
}