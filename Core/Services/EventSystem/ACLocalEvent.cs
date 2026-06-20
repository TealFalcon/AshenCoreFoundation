using UnityEngine;
using VContainer;
using System.Collections.Generic;


namespace AshenCore.Core
{

        public enum ACEventCallType
        {
            SystemCommand,
            GlobalPreset,
            NewEvent
        }

    public class ACLocalEvent : MonoBehaviour
    {


        private ACEventService _eventService;
        public ACEventCallType callType;
        public ACDriverType driverType;
        public ACEvent _event;
        public int eventID = 0;


        [Inject]
        public void Construct(AshenCoreServices services)
        {
            _eventService = services.Events;
        }

        public void CallEvent(int eventID)
        {
            switch (callType)
            {
                case ACEventCallType.SystemCommand:
                    _eventService.AddEvent(ACEventType.System, eventID);
                    break;
                case ACEventCallType.GlobalPreset:
                    _eventService.AddEvent(ACEventType.Global,driverType, eventID);
                    break;
                case ACEventCallType.NewEvent:
                    _eventService.AddEvent(_event);
                    break;
            }

        }


    }


}
