using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;


namespace AshenCore.Core
{

    public enum ACEventType
    {
        System = 0,
        Global = 1,
        Local = 2
    }

    public class ACEventService : MonoBehaviour
    {
        private List<ACEvent> _events = new List<ACEvent>();
        private CancellationTokenSource _cts;

        //DRIVERS LIST
        [SerializeField] private ACDriver AudioDriver;
        [SerializeField] private ACDriver SpawnerDriver;
        // ... Add more drivers



        void Awake()
        {
            Initialize();
        }

  
        public void Initialize()
        {
            _cts = new CancellationTokenSource();

            DispatchLoop(_cts.Token).Forget();
            CleanupLoop(_cts.Token).Forget();
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }


        private async UniTaskVoid DispatchLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                while (_events.Count > 0)
                {
                    for (int i = _events.Count - 1; i >= 0; i--)
                    {
                        switch (_events[i].driverType)
                        {
                            case ACDriverType.Audio:
                                AudioDriver?.ManageEvent(_events[i]);
                                break;
                            case ACDriverType.Spawner:
                                SpawnerDriver?.ManageEvent(_events[i]);
                                break;
                            // ... Add more drivers
                        }
                    }
                }
                
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }

        private async UniTaskVoid CleanupLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                while (_events.Count > 0)
                {
                    for (int i = _events.Count - 1; i >= 0; i--)
                    {
                        if (_events[i].ToDestroy())
                        {
                            _events.RemoveAt(i);
                        }
                    }
                }
                
              
                // Limpieza cada 5 segundos
                await UniTask.Delay(TimeSpan.FromSeconds(5), cancellationToken: token);
            }
        }

        public void AddEvent(ACEventType eventType, int eventID) {
            //TODO Look for Event System Preset
        }

        public void AddEvent(ACEventType eventType,ACDriverType driverType, int eventID) {
            //TODO Look for Event Global Preset
        }

        public void AddEvent(ACEventType eventType, ACDriverType driverType, int eventID, ACEventPayloadContainer data)
        {
            ACEvent acEvent = new ACEvent(eventType, driverType, eventID, data);
            AddEvent(acEvent);

        }

        public void AddEvent(ACEvent acEvent)
        {
            _events.Add(acEvent);
        }


    }


    [System.Serializable]
    public class ACEvent
    {
        public ACEventType eventType;
        public ACDriverType driverType;
        public int eventID;
        public ACEventPayloadContainer payloadContainer = new ACEventPayloadContainer();
        private bool toDestroy = false;

        public ACEvent() { }

        public ACEvent(ACEventType eventType, ACDriverType driverType, int eventID, ACEventPayloadContainer payloadContainer)
        {
            this.eventType = eventType;
            this.driverType = driverType;
            this.eventID = eventID;
            this.payloadContainer = payloadContainer;
        }

        public void Destroy()
        {
            toDestroy = true;
        }

        public bool ToDestroy()
        {
            return toDestroy;
        }
    }



}
