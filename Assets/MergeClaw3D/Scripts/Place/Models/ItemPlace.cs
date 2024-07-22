using System;
using DG.Tweening;
using MergeClaw3D.Scripts.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeClaw3D.Scripts.Place
{
    public class ItemPlace : MonoBehaviour
    {
        [Title("MergeEffect")]
        [SerializeField] private ParticleSystem _matchedVfx;
        [SerializeField] private ParticleSystem _mergedVfx;
        
        public ItemEntity Item { get; private set; }
        public PlaceState State { get; private set; }
        public Vector3 Position => transform.position;
        

        public void SetItem(ItemEntity itemEntity)
        {
            Item = itemEntity;

            State = PlaceState.Occupied;
        }

        public ItemEntity ExtractItem()
        {
            State = PlaceState.Free;

            var item = Item;
            Item = null;

            return item;
        }

        public void OnMatched()
        {
            _matchedVfx.Play();
        }
        
        public void OnMerged()
        {
            _mergedVfx.Play();
        }
    }
}