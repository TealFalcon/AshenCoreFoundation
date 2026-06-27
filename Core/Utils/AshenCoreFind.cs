using System.Collections.Generic;
using System.Linq;
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

        public static List<T> FindInterfaces<T>(bool includeInactive = true) where T : class
        {
            var behaviours = Object.FindObjectsByType<MonoBehaviour>(
                includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude,
                FindObjectsSortMode.None
            );

            return behaviours
                .OfType<T>()
                .ToList();
        }
        
#pragma warning restore CS0618
    }
    
    
    
}