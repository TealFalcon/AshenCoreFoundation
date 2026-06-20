using UnityEngine;
using VContainer;
using VContainer.Unity;
using System.Collections.Generic;

namespace AshenCore.Core
{

    public class AshenCoreServices
    {
        private readonly IObjectResolver _resolver;

        public AshenCoreServices(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public ILog Debug => TryGet<ILog>();
        public ACAudioService Audio => TryGet<ACAudioService>();
        public ACEventService Events => TryGet<ACEventService>();
        public ACSceneManager Scenes => TryGet<ACSceneManager>();
        public ACFadeService Fade => TryGet<ACFadeService>();
        public ACSpawnerService Spawner => TryGet<ACSpawnerService>();
        public ACInputSystem Input => TryGet<ACInputSystem>();
        public IACPersistenceSystem Persistence => TryGet<IACPersistenceSystem>();
        public ACResourcesService Resources => TryGet<ACResourcesService>();

        private T TryGet<T>() where T : class
        {
            _resolver.TryResolve<T>(out var service);
            return service;
        }
    }


    public class AshenCore : LifetimeScope
    {
        [Header("Log System")]
        [SerializeField] private ACDebugSystem _ACLogSystem;

        [Header("Services")]
        [SerializeField] private ACAudioService _audioService;
        [SerializeField] private ACEventService _eventService;
        [SerializeField] private ACSceneManager _sceneManager;
        [SerializeField] private ACFadeService _fadeService;
        [SerializeField] private ACSpawnerService _spawnerService;
        [SerializeField] private ACInputSystem _inputSystem;
        

        private AshenCoreServices _ashenCoreServices;

        private List<string> registeredServices = new List<string>();


        protected override void Configure(IContainerBuilder builder)
        {

            Debug.Log("[ASHEN CORE] Registering Ashen Core Services...");

            // *************************************************************
            //                         SYSTEMS
            // *************************************************************

            if (_ACLogSystem != null)
                builder.RegisterInstance(_ACLogSystem).As<ILog>();
            else
                builder.Register<UnityLogSystem>(Lifetime.Singleton).As<ILog>();

            InterfaceRegistration<IACPersistenceSystem>(builder, "Persistence System");

            OServiceRegistration<ACAudioService>(builder, _audioService, "Audio Service");
            OServiceRegistration<ACEventService>(builder, _eventService, "Event Receiver");
            OServiceRegistration<ACSceneManager>(builder, _sceneManager, "Scene Manager");
            OServiceRegistration<ACFadeService>(builder, _fadeService, "Fade Service");
            OServiceRegistration<ACSpawnerService>(builder, _spawnerService, "Spawner Service");
            OServiceRegistration<ACInputSystem>(builder, _inputSystem, "Input System");
            ServiceRegistration<ACResourcesService>(builder, "Resources Service");

            //Registering a wrapper with all Services
            builder.Register(resolver =>
            {
                return new AshenCoreServices(resolver);
            }, Lifetime.Singleton);


            // *************************************************************
            //                          MODULES
            // *************************************************************
            builder.RegisterInstance(registeredServices);
            builder.RegisterEntryPoint<Bootstrap>();
        }

        void OServiceRegistration<T>(IContainerBuilder builder, T service, string serviceName) where T : class
        {

            if (service != null)
            {
                builder.RegisterComponent(service);
                Debug.Log("[ASHEN CORE] " + serviceName + " service registered");
                registeredServices.Add(serviceName);
            }
            else
            {
                Debug.LogWarning(" [ASHEN CORE] No " + serviceName + " service found");
            }

        }

        void ServiceRegistration<T>(IContainerBuilder builder, string serviceName) where T : class
        {

            builder.Register<T>(Lifetime.Singleton);
            registeredServices.Add(serviceName);
            Debug.Log("[ASHEN CORE] " + serviceName + " service registered");

        }

        void InterfaceRegistration<T>(IContainerBuilder builder, string serviceName) where T : class
        {

            builder.RegisterComponentInHierarchy<T>().As<T>();
            registeredServices.Add(serviceName);
            Debug.Log("[ASHEN CORE] " + serviceName + " interface system registered");

        }
    }
    
    
    public class Bootstrap : IStartable
    {
        private readonly ILog Log;
        private readonly AshenCoreServices Services;

        public Bootstrap(List<string> registeredservices,IObjectResolver Container, AshenCoreServices services)
        {
            Log = services.Debug;
            Services = services;
            Log.SetRegisteredServices(registeredservices); 
            
            Log.Log("[ASHEN CORE] Registering Event Drivers", ConsoleMessageType.Info);
            if (services.Events != null)
            {
                var drivers = AshenCoreFind.FindAll<ACDriver>(false);
            
                foreach (var driver in drivers)
                {
                    Container.Inject(driver);
                    Log.Log("[ASHEN CORE] " + driver.name + " driver registered", ConsoleMessageType.Info);
                }
            }

            if (services.Spawner != null)
            {
                services.Spawner.SetResolver(Container);
            }

            if (Log.UseGUI())
            {
                Container.Inject(Log);
                Log.Log("Initalizing Console...", ConsoleMessageType.Info);
                Log.InitializeGUI();
            }

            
        }

        public void Start()
        {
            Log?.Log("¡[ASHEN CORE] Ashen Core Initialized!", ConsoleMessageType.Info);
            Services.Persistence?.Initialize();
        }
    }
}