using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

namespace AshenCore.Core
{

    public enum ACInputValueType
    {
        Button,
        Axis1D,
        Axis2D,
        Vector3,
        Delta,
        Trigger,
        Hold
    }

    public enum ACInputCategory
    {
        Gameplay = 0,
        UI = 1,
        Debug = 2,
        System = 3
    }

    [Flags]
    public enum ACInputBindingCapability
    {
        None      = 0,
        Keyboard  = 1 << 0,
        Mouse     = 1 << 1,
        Gamepad   = 1 << 2,
        Touch     = 1 << 3,
        VR        = 1 << 4
    }

    [Serializable]
    public class ACInputControlDefinition
    {
        public string NAME;
        public string CONTROL_PATH;
        public ACInputBindingCapability CAPABILITY;
    }

    [CreateAssetMenu(fileName = "ACInputActionDatabase", menuName = "AshenCore/Input/Input Action Database")]
    public class ACInputActionDatabase : ScriptableObject
    {
        public List<ACInputAction> inputActions;
        public ACInputCategory category;
    }

    [Serializable]
    public class ACInputAction
    {
        public string NAME;
        public ACInputValueType TYPE;
        public bool BLOCK_UI;
    }

}