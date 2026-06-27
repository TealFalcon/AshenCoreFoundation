using VContainer;
using UnityEngine;


namespace AshenCore.Core
{


    public class AshenService : MonoBehaviour
    {
        protected ILog Log;
        protected AshenCoreServices Services;

        [Inject]
        public void Construct(AshenCoreServices _services)
        {
            Services = _services;
            Log = _services.Debug;
        }

    }

    public interface IAshenModule
    {
        void Register(AshenCore Core);
    }

    public class AshenObject : MonoBehaviour
    {
        protected ILog Log;
        protected AshenCoreServices Services;

        [Inject]
        public void Construct(AshenCoreServices _services)
        {
            Services = _services;
            Log = _services.Debug;
        }

    }

}