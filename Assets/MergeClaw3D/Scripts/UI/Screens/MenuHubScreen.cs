// using MergeClaw3D.Scripts.Signals;
// using MergeClaw3D.Scripts.UI.Screens.Interfaces;
// using UnityEngine;
// using UnityEngine.UI;
// using Zenject;
//
// namespace MergeClaw3D.Scripts.UI.Screens
// {
//     public class MenuHubScreen : MonoBehaviour, IScreen
//     {
//         [Inject] private SignalBus _signalBus;
//         
//         [SerializeField] private Button _startButton;
//
//         public void Start()
//         {
//             _startButton.gameObject.SetActive(true);
//             
//             _startButton.onClick.AddListener(()=> 
//             {
//                 _startButton.enabled = false;
//                 _startButton.onClick.RemoveAllListeners();
//                 _signalBus.Fire<StartGameCallSignal>();
//             });
//         }
//         
//         public void Show()
//         {
//             gameObject.SetActive(true);
//         }
//
//         public void Hide()
//         {
//             gameObject.SetActive(false);
//         }
//     }
// }
