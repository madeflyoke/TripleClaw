using MergeClaw3D.Scripts.Services;
using MergeClaw3D.Scripts.Stages;
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
        [Inject] private StagesManager _stagesManager;
        
        private void Start()
        {
            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;
            _servicesHolder.InitializeServices();
            _stagesManager.Initialize();
        }
        
        private void OnDestroy()
        {
            _servicesHolder?.Dispose();
        }
    }
}
