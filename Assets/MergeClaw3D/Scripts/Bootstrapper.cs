using MergeClaw3D.Scripts.Services;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MergeClaw3D.Scripts
{
    public class Bootstrapper : MonoBehaviour
    {
        [Inject] private ServicesHolder _servicesHolder;
        
        private void Awake()
        {
            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;
            _servicesHolder.InitializeServices();
        }
        
        private void OnDestroy()
        {
            _servicesHolder?.Dispose();
        }
    }
}
