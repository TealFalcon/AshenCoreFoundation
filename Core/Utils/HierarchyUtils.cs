using UnityEngine;

namespace AshenCore.Core
{
    public static class HierarchyUtils
    {
        public static Transform GetOrCreate(Transform parent, string name)
        {
            Transform child = parent.Find(name);

            if (child != null)
                return child;

            GameObject go = new GameObject(name);
            go.transform.SetParent(parent);
            return go.transform;
        }

        public static Transform EnsurePath(string path)
        {
            string[] parts = path.Split('/');

            Transform current = null;

            // Intentamos encontrar el root en escena
            GameObject rootObj = GameObject.Find(parts[0]);

            if (rootObj == null)
                rootObj = new GameObject(parts[0]);

            current = rootObj.transform;

            for (int i = 1; i < parts.Length; i++)
            {
                current = GetOrCreate(current, parts[i]);
            }

            return current;
        }
    }
}