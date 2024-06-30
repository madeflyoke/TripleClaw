// using System.Collections.Generic;
// using MergeClaw3D.Scripts.Signals;
// using MergeClaw3D.Scripts.UI.Screens.Interfaces;
// using UnityEngine;
// using Zenject;
//
// namespace MergeClaw3D.Scripts.UI.Screens
// {
//     public class ScreensController : MonoBehaviour
//     {
//         [Inject] private SignalBus _signalBus;
//         
//         [field: SerializeField] public MenuHubScreen MenuHubScreen { get; private set; }
//         [field: SerializeField] public GameplayScreen GameplayScreen { get; private set; }
//         private List<IScreen> _screens;
//
//         private void Awake()
//         {
//             _screens = new List<IScreen>
//             {
//                 MenuHubScreen,
//                 GameplayScreen
//             };
//             _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
//         }
//
//         private void Start()
//         {
//             ShowSingleScreen<MenuHubScreen>();
//         }
//
//         private void OnGameStarted()
//         {
//             ShowSingleScreen<GameplayScreen>();
//         }
//         
//         private void ShowSingleScreen<T>() where T: IScreen
//         {
//             foreach (var screen in _screens)
//             {
//                 if (screen.GetType()==typeof(T))
//                 {
//                     screen.Show();
//                 }
//                 else
//                 {
//                     screen.Hide();
//                 }
//             }
//         }
//     }
// }
