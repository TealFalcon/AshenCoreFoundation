using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AshenCore.Core
{
    [CustomEditor(typeof(AshenCore))]
    public class AshenCoreEditor : Editor
    {
        protected Texture2D image;

        protected virtual void OnEnable()
        {
            image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/AshenCore/Core/Editor/topbar.png");
        }

        public override void OnInspectorGUI()
        {
            // Create A GUI image Style
            ACEditorUtils.DrawImage(image, 539, 96);

            DrawDefaultInspector();

            //AshenCore ashenCore = (AshenCore) target;
        }

    }

    [CustomEditor(typeof(ACEventService))]
    public class ACEventServiceEditor : AshenCoreEditor { }

    [CustomEditor(typeof(ACSceneManager))]
    public class ACSceneManagerEditor : AshenCoreEditor { }



    [CustomEditor(typeof(ACLocalEvent))]
    public class ACLocalEventEditor : AshenCoreEditor
    {
        private SerializedProperty eventProp;
        private SerializedProperty containerProp;
        private SerializedProperty audioPayloadProp;
        private SerializedProperty spawnerPayloadProp;
        private SerializedProperty basePayloadProp;

        protected override void OnEnable()
        {
            base.OnEnable();

            eventProp = serializedObject.FindProperty("_event");
            containerProp = eventProp.FindPropertyRelative("payloadContainer");
            audioPayloadProp = containerProp.FindPropertyRelative("audioPayload");
            spawnerPayloadProp = containerProp.FindPropertyRelative("spawnerPayload");
            basePayloadProp = containerProp.FindPropertyRelative("payload");

        }
        public override void OnInspectorGUI()
        {
            ACEditorUtils.DrawImage(image, 539, 96);

            ACLocalEvent localEvent = (ACLocalEvent)target;

            ACEditorUtils.Header("Select PayLoad");
            localEvent.callType = ACEditorUtils.DrawEnum("Call Event Type", localEvent.callType);

            switch (localEvent.callType)
            {
                case ACEventCallType.SystemCommand:
                    GUILayout.Label("System Command");
                    localEvent.eventID = ACEditorUtils.DrawInt("Event ID", localEvent.eventID);
                    break;
                case ACEventCallType.GlobalPreset:
                    GUILayout.Label("Global Preset");
                    localEvent.driverType = ACEditorUtils.DrawEnum("Driver Type", localEvent.driverType);
                    localEvent.eventID = ACEditorUtils.DrawInt("Event ID", localEvent.eventID);
                    break;
                case ACEventCallType.NewEvent:
                    GUILayout.Label("Local Event");

                    if (localEvent._event == null)
                        localEvent._event = new ACEvent();
                    if (localEvent._event.payloadContainer == null)
                        localEvent._event.payloadContainer = new ACEventPayloadContainer();

                    localEvent._event.payloadContainer.payloadType = ACEditorUtils.DrawEnum("Payload Container Type", localEvent._event.payloadContainer.payloadType);

                    switch (localEvent._event.payloadContainer.payloadType)
                    {
                        case ACEventPayloadType.Audio:
                            if (audioPayloadProp == null)
                                localEvent._event.payloadContainer.audioPayload = new ACEventAudioPayload();
                            EditorGUILayout.PropertyField(audioPayloadProp);
                            break;

                        case ACEventPayloadType.Spawner:
                            if (spawnerPayloadProp == null)
                                localEvent._event.payloadContainer.spawnerPayload = new ACEventSpawnerPayload();
                            EditorGUILayout.PropertyField(spawnerPayloadProp);
                            break;

                        default:
                            if (basePayloadProp == null)
                                localEvent._event.payloadContainer.payload = new ACEventPayload();
                            EditorGUILayout.PropertyField(basePayloadProp);
                            break;
                    }

                    break;
            }

            EditorUtility.SetDirty(localEvent);
        }



    }
    
/*         [CustomEditor(typeof(ACLocalSystem))]
        public class ACLocalSystemEditor : AshenCoreEditor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                var system = (ACLocalSystem)target;

                GUILayout.Space(10);
                GUILayout.Label("Global Providers", EditorStyles.boldLabel);

                if(system.GetSaveService() == null)
                {
                    GUILayout.Label("No Save Service found.");
                    return;
                }

                foreach (var p in system.GetSaveService().GlobalProviders)
                {
                    GUILayout.Label($"{p.Key} → {p.Value.GetType().Name}");
                }

                GUILayout.Space(10);
                GUILayout.Label("Runtime Providers", EditorStyles.boldLabel);

                foreach (var p in system.GetSaveService().RuntimeProviders)
                {
                    GUILayout.Label($"{p.Key} → {p.Value.GetType().Name}");
                }
            }
        } */

}
