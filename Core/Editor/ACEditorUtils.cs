using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace AshenCore.Core
{

    public static class ACEditorUtils
    {

        public static void BeginColorBox(Color color)
        {
            var rect = EditorGUILayout.BeginVertical();
            EditorGUI.DrawRect(rect, color);
            GUILayout.Space(4);
        }

        public static void EndColorBox()
        {
            GUILayout.Space(4);
            EditorGUILayout.EndVertical();
        }

        public static void DrawImage(Texture2D image, float width, float height)
        {
            //Paint Image
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField(new GUIContent(image), GUILayout.Width(width), GUILayout.Height(height));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        public static T DrawEnum<T>(string label, T value) where T : System.Enum
        {
            return (T)EditorGUILayout.EnumPopup(label, value);
        }

        public static T DrawCollection<T>(string label, string[] options, List<T> collection, T selected)
        {
            if (collection == null || collection.Count == 0)
                {
                    EditorGUILayout.LabelField(label, "Empty");
                    return default;
                }

            int index = collection.IndexOf(selected);
            if (index < 0) index = 0;

            int newIndex = EditorGUILayout.Popup(label, index, options);

             return collection[newIndex]; 
        }

        public static void Header(string title)
        {
            GUILayout.Space(10);

            GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
            style.fontSize = 14;

            EditorGUILayout.LabelField(title, style);

            Rect rect = GUILayoutUtility.GetRect(1, 2);
            rect.height = 2;

            EditorGUI.DrawRect(rect, new Color(0.3f, 0.3f, 0.3f));
        }

        public static int DrawInt(string label, int value)
        {
            return EditorGUILayout.IntField(label, value);
        }

    }
    

}
