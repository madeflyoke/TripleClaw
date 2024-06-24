using System;
using UnityEngine;

namespace MergeClaw3D.Scripts.Configs.Items
{
    [Serializable]
    public class ItemConfigData
    {
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public Mesh Mesh { get; private set; }

        public ItemConfigData(int id, Mesh mesh)
        {
            Id = id;
            Mesh = mesh;
        }
    }
}
