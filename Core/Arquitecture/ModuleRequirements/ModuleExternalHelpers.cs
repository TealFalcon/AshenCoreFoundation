using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace AshenCore.Core
{
    [Serializable]
    public class ACSpawnHandle
    {
        public int Id;
        public GameObject Instance;

        public ACSpawnHandle(int id, GameObject instance)
        {
            Id = id;
            Instance = instance;
        }
    }

    public struct AssetHandle
    {

        public ResourceType Type;
        public string Key;
        public AssetReference AssetReference;
        public UnityEngine.Object AssetObject;

    }

    public enum ResourceType
    {
        assetobject,
        reference,
        resourcekey
    }

    public class ACSpawnRequest
    {
        public int PoolId;
        public Vector3 Position;
        public Quaternion Rotation;
        public Transform parent;
        public bool ActivateOnSpawn;

        public ACSpawnRequest(int poolId, Vector3 position, Quaternion rotation, Transform parent, bool activateOnSpawn)
        {
            PoolId = poolId;
            Position = position;
            Rotation = rotation;
            this.parent = parent;
            ActivateOnSpawn = activateOnSpawn;
        }
    }

    [Serializable]
    public class GUIHelper
    {

        public GUIDefinition definition;
        public ACWindowState state = ACWindowState.Destroyed;
        public ACUIWindow windowComponent;

        public GUIHelper(GUIDefinition definition)
        {
            this.definition = definition;
        }

    }


    [Serializable]
    public class ACIconDefinition
    {
        public string Id;

        public Sprite image;

        public Vector2 DefaultSize = new(24, 24);

        public bool PreserveAspect = true;
    }

    [Serializable]
    public class ACPool
    {
        public int Id;
        public GameObject Prefab;
        public List<ACSpawnHandle> GameObjects;
    }

    public enum ACSpawnContainerType
    {
        System,
        World,
        Props,
        FX,
        UI,
        Projectiles
    }

    public enum ACUILayers
    {
        Background = 0,
        Screen = 1,
        Popup = 2,
        Overlay = 3
    }

    public struct ACInputEvent
    {
        public string Action;
        public ACInputPhase Phase;
        public object Value;
    }

    public enum ACInputPhase
    {
        Pressed,
        Held,
        Released
    }

        public enum ACWindowState
    {
        Opened = 0,
        Closed = 1,
        Destroyed = 2
    }

    public enum AudioChannel
    {
        SFX,
        Music
    }

    public abstract class ACUIWindow : AshenObject
    {
        public virtual void OnCreate() { }
        public virtual void OnOpen() { }
        public virtual void OnClose() { }
        public virtual void OnDestroyWindow() { }
        public virtual void SetTheme(UIThemeManager themeManager) { }

        public List<Button> PositiveButtons = new List<Button>();
        public List<Button> NegativeButtons = new List<Button>();
        public List<Button> OtherButtons = new List<Button>();
        public List<TMP_Text> Labels = new List<TMP_Text>();
        public List<TMP_Text> Descriptions = new List<TMP_Text>();
        public List<TMP_Text> Titles = new List<TMP_Text>();
        public List<Image> Windows = new List<Image>();
        public List<TMP_InputField> InputFields = new List<TMP_InputField>();

        public void ApplyTheme(UITheme Theme)
        {
            foreach (Image w in Windows) UIThemeApplicator.ApplyThemeWindow(w, Theme);
            foreach (Button b in PositiveButtons) UIThemeApplicator.ApplyThemeOkButton(b, Theme);
            foreach (Button b in NegativeButtons) UIThemeApplicator.ApplyThemeCancelButton(b, Theme);
            foreach (Button b in OtherButtons) UIThemeApplicator.ApplyThemeOtherButton(b, Theme);
            foreach (TMP_Text l in Labels) UIThemeApplicator.ApplyThemePrimaryText(l, Theme);
            foreach (TMP_Text l in Descriptions) UIThemeApplicator.ApplyThemeSecondaryText(l, Theme);
            foreach (TMP_Text l in Titles) UIThemeApplicator.ApplyThemeTitleText(l, Theme);
            foreach (TMP_InputField l in InputFields) UIThemeApplicator.ApplyThemeInputText(l, Theme);

        }


    }
}