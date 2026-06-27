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
        public IAudioService Audio => TryGet<IAudioService>();  //Done
        public ACEventService Events => TryGet<ACEventService>(); //Done
        public ACSceneManager Scenes => TryGet<ACSceneManager>(); //Done
        public IACFeedbackService Feedback => TryGet<IACFeedbackService>(); //Done
        public ISpawnerService Spawner => TryGet<ISpawnerService>(); //Done
        public IACInputSystem Input => TryGet<IACInputSystem>();
        public IACPersistenceSystem Persistence => TryGet<IACPersistenceSystem>();  //Done
        public IResourcesService Resources => TryGet<IResourcesService>();
        public IUIService UI => TryGet<IUIService>(); //Done

        private T TryGet<T>() where T : class
        {
            _resolver.TryResolve<T>(out var service);
            return service;
        }
    }


    public class AshenCore : LifetimeScope
    {
        private IContainerBuilder _builder;
        
        [Header("Core Systems")]
        [SerializeField] private ACDebugSystem _ACLogSystem;
        [SerializeField] private ACEventService _eventService;
        [SerializeField] private ACSceneManager _sceneManager;

        private AshenCoreServices _ashenCoreServices;

        private List<IAshenModule> modules = new List<IAshenModule>();

        private List<string> registeredServices = new List<string>();

        protected override void Configure(IContainerBuilder builder)
        {

            Debug.Log("[ASHEN CORE] Registering Ashen Core Services...");

            _builder = builder;

            // *************************************************************
            //                         SYSTEMS
            // *************************************************************

            if (_ACLogSystem != null)
                builder.RegisterInstance(_ACLogSystem).As<ILog>();
            else
                builder.Register<UnityLogSystem>(Lifetime.Singleton).As<ILog>();

            OServiceRegistration<ACEventService>(_eventService, "Event Receiver");
            OServiceRegistration<ACSceneManager>(_sceneManager, "Scene Manager");

            // *************************************************************
            //                        CORE MODULES
            // *************************************************************

            modules = AshenCoreFind.FindInterfaces<IAshenModule>(false);
            
            foreach (var module in modules)
            {
                module.Register(this);
            }

            //Registering a wrapper with all Services
            builder.Register(resolver =>
            {
                return new AshenCoreServices(resolver);
            }, Lifetime.Singleton);


            // *************************************************************
            //                          SHARDS
            // *************************************************************



            builder.RegisterInstance(registeredServices);
            builder.RegisterEntryPoint<Bootstrap>();
        }

        
        public void OServiceRegistration<T>(T service, string serviceName) where T : class
        {

            if (service != null)
            {
                _builder.RegisterComponent(service);
                Debug.Log("[ASHEN CORE] " + serviceName + " service registered");
                registeredServices.Add(serviceName);
            }
            else
            {
                Debug.LogWarning(" [ASHEN CORE] No " + serviceName + " service found");
            }

        }

        public void ServiceRegistration<T>(string serviceName) where T : class
        {

            _builder.Register<T>(Lifetime.Singleton);
            registeredServices.Add(serviceName);
            Debug.Log("[ASHEN CORE] " + serviceName + " service registered");

        }

        public void InterfaceRegistration<T>(string serviceName) where T : class
        {

            _builder.RegisterComponentInHierarchy<T>().As<T>();
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
            Services.UI?.Initialize();
        }
    }
}