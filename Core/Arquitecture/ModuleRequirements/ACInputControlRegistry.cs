using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.Controls;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace AshenCore.Core
{

    [CreateAssetMenu(fileName = "ACInputControlRegistry", menuName = "AshenCore/Input/Input Control Registry")]
    public class ACInputControlRegistry : ScriptableObject
    {
        public List<ACInputControlDefinition> Controls;
        public bool _isRecording;
        public ACInputBindingCapability AllowedCapabilities;
        

        public void onEnable()
        {
            _isRecording = false;
        }

        public async void RecordInput()
        {
            await StartCaptureRecording();
            _isRecording = true;
        }

        private void OnInputEvent(InputEventPtr eventPtr, InputDevice device)
        {
            if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
                return;

            var deviceCapability = GetCapability(device);

            if ((AllowedCapabilities & deviceCapability) == 0)
                return;


            foreach (var control in device.allControls)
            {
                if (!control.IsActuated())
                    continue;


                if (device is Mouse mouse)
                {
   
                    if (control is ButtonControl mousebutton)
                    {
                        Debug.Log("BUTTON CONTROL? " + mousebutton.path);
                        Register(mousebutton,device);
                    }

                }

                if(device is Keyboard keyboardControl)
                {
                    
                    if(control is KeyControl key)
                    {
                        Register(key,device);                 
                    }
           
                }

                if (control is ButtonControl button && button.wasPressedThisFrame)
                {
                    Register(button,device);
                }


            }
        }

        private void Register(KeyControl control,InputDevice device)
        {
            SaveDefinition(control.path,device);
            StopRecordingInput();
        }

        private void Register(ButtonControl control,InputDevice device)
        {
            SaveDefinition(control.path,device);
            StopRecordingInput();
        }

        private void SaveDefinition(string path,InputDevice device)
        {
            for (int i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i].CONTROL_PATH == path)
                    {
                        Debug.Log("CONTROL ALREADY EXISTS");
                        return;
                    }
                }

            ACInputControlDefinition definition = new ACInputControlDefinition();
            definition.CONTROL_PATH = path;
            definition.CAPABILITY = GetCapability(device);
            definition.NAME = path.Split('/').Last().ToUpper();

            Controls.Add(definition);

            Debug.Log($"CONTROL REGISTERED: {path}");

        }

        private ACInputBindingCapability GetCapability(InputDevice device)
        {
           if (device is Keyboard)
                return ACInputBindingCapability.Keyboard;

            if (device is Mouse)
                return ACInputBindingCapability.Mouse;

            if (device is Gamepad)
                return ACInputBindingCapability.Gamepad;

            if (device is Touchscreen)
                return ACInputBindingCapability.Touch;

            // XR / VR
            if (device.layout.Contains("XR") || device.layout.Contains("Tracked"))
                return ACInputBindingCapability.VR;

            return ACInputBindingCapability.None;
        }

        private bool IsValidControl(InputControl control)
        {
            if (control is ButtonControl)
                return true;

            if (control is AxisControl axis)
                return axis.noisy == false;

            return false;
        }       

        public void StopRecordingInput()
        {
            InputSystem.onEvent -= OnInputEvent;
            _isRecording = false;
        }

        
        private async UniTask StartCaptureRecording()
        {
            await UniTask.WaitForSeconds(0.2f);
            InputSystem.onEvent += OnInputEvent;
        }
    }

}