using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;


namespace AshenCore.Core
{

    [CreateAssetMenu(fileName = "ACInputProfile", menuName = "AshenCore/Input/Input Profile")]
    public class ACInputProfile : ScriptableObject
    {
        public string ProfileName;

        // Settings References
        public ACInputBindingCapability DeviceCapabilities;
        public ACInputActionDatabase ActionDatabase;
        public ACInputControlRegistry Registry;
        public List<ACInputBinding> Bindings;

        //Editor References
        public ACInputAction _action;
        public ACInputControlDefinition _definition;

        public void AddNewBinding()
        {

            foreach (ACInputBinding bind in Bindings)
            {
                if (bind.CONTROL_PATH == _definition.CONTROL_PATH)
                {
                    Debug.Log("DEFINTION ALREADY EXISTS");
                    return;
                }
            }
            
            ACInputBinding _binding = new ACInputBinding();
            _binding.ACTION_NAME = _action.NAME;
            _binding.CONTROL_PATH = _definition.CONTROL_PATH;


            Bindings.Add(_binding);
        }

    }


    [Serializable]
    public class ACInputBinding
    {
        public string ACTION_NAME;       // "Jump"
        public string CONTROL_PATH;      // "<Keyboard>/space"
    }

}