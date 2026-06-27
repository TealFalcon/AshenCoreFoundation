using UnityEngine;
using VContainer;



namespace AshenCore.Core
{
    public class ACAudioDriver : ACDriver, IACDriver
    {
        private IAudioService _audioService;

        [Inject]
        void Construct(AshenCoreServices services)
        {
            _audioService = services.Audio;
        }

        public override void ManageEvent(ACEvent acEvent)
        {
            acEvent.Destroy();
        }


        void Update()
        {

        }

    }
}