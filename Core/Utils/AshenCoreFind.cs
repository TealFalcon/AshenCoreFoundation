using UnityEngine;

namespace AshenCore.Core
{
    public static class AshenCoreFind
    {

#pragma warning disable CS0618
        public static T[] FindAll<T>(bool includeInactive) where T : Object
        {
#if UNITY_2022_2_OR_NEWER
            return Object.FindObjectsByType<T>(
                includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude,
                FindObjectsSortMode.None
            );
#else
            return Object.FindObjectsOfType<T>(includeInactive);
    #endif
        }
#pragma warning restore CS0618
    }
    
}