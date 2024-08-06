using MergeClaw3D.Scripts.Inventory.Interfaces;
using UnityEngine;

namespace MergeClaw3D.Scripts.Inventory
{
    public class ArtifactEffectsComponent : MonoBehaviour, IArtifactComponent
    {
        [SerializeField] private ParticleSystem _glowingVfx;
        [SerializeField] private ParticleSystem _disappearVfx;

        public void StopAll()
        {
            _glowingVfx.Stop();
            _disappearVfx.Stop();
        }
        
        public void PlayGlowingVfx()
        {
            _glowingVfx.Play();
        }

        public void PlayDisappearVfx(bool afterDestroy)
        {
            if (afterDestroy)
            {
                _disappearVfx.transform.parent = transform.root;
                var main = _disappearVfx.main;
                main.stopAction = ParticleSystemStopAction.Destroy;
            }
            _disappearVfx.Play();
        }
    }
}
