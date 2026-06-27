using UnityEngine;

namespace AshenCore.Core
{
    public class ACDebugCameraObject : AshenObject
    {

        void Awake()
        {
                
            if (Log == null) return;
                
                Log.SetDebugCamera(gameObject);
        }

    }
}