using System;
using System.Collections.Generic;
using System.Threading;
using MergeClaw3D.Scripts.Currency;
using MergeClaw3D.Scripts.Services.Interfaces;
using MergeClaw3D.Scripts.Services.Progress;
using MergeClaw3D.Scripts.Services.Progress.Currency;
using MergeClaw3D.Scripts.Services.Progress.Inventory;
using UnityEngine;
using Zenject;

namespace MergeClaw3D.Scripts.Services
{
    public class ServicesHolder : IDisposable
    {
        private Dictionary<Type,IService> _services;
        private CancellationTokenSource _cts;
        private bool _isInitialized;
        private readonly DiContainer _container;

        public ServicesHolder(DiContainer container)
        {
            _container = container;
        }
        
        public async void InitializeServices(Action onInitialized = null)
        {
            if (_isInitialized)
            {
                return;
            }
            
            TService AddService<TService>() where TService: IService
            {
                var instance = _container.Instantiate<TService>();
                _services.Add(typeof(TService), instance);
                return instance;
            }
            
            _services = new Dictionary<Type, IService>();
            
            //add all services below
          //  AddService<YandexService>();
            AddService<InputService>();
            AddService<PauseService>();
            AddService<CurrencyService>();
            AddService<InventoryService>();

            _cts = new CancellationTokenSource();

            foreach (var service in _services)
            {
                Debug.LogWarning($"Service {service.Value} started initialization...");
                await service.Value.Initialize(_cts);
                Debug.LogWarning($"Service {service.Value} initialized2");
            }
            
            onInitialized?.Invoke();
            _isInitialized = true;
        }
        
        public TService GetService<TService>() where TService: IService
        {
            return (TService) _services[typeof(TService)];
        }

        public void Dispose()
        {
            _cts?.Cancel();
        }
    }
}
