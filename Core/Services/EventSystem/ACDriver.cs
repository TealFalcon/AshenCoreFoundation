using UnityEngine;


namespace AshenCore.Core
{
    public interface IACDriver
    {
        public void ManageEvent(ACEvent acEvent);
    }

    public enum ACDriverType
    {
        Audio = 0,
        Spawner = 1
        //ADD More DriverTypes
    }
    
    
    public enum ACEventPayloadType
    {
        Base,
        Audio,
        Spawner
        //ADD more Payload Types
    }



    [System.Serializable]
    public class ACEventPayloadContainer
    {
        public ACEventPayloadType payloadType;
        public ACEventPayload payload;
        public ACEventAudioPayload audioPayload;
        public ACEventSpawnerPayload spawnerPayload;
        //ADD Payloads to Container
    }


    [System.Serializable]
    public class ACEventPayload
    {
        public float value;
        public int intValue;
        public string stringValue;
    }

    [System.Serializable]
    public class ACEventAudioPayload
    {
        public AudioClip audioClip;
        public float volume;
        public int channel;
        public Vector3 position;
    }

    [System.Serializable]
    public class ACEventSpawnerPayload
    {
        public GameObject prefab;
        public Vector3 position;
        public Quaternion rotation;
        public Transform parent;
        public bool activateOnSpawn;
        public int poolId;
    }

    //ADD Payloads


    public class ACDriver : MonoBehaviour
    {

        public ACDriverType driverType;


        public virtual void ManageEvent(ACEvent acEvent)
        {

        

        }


    }
}
