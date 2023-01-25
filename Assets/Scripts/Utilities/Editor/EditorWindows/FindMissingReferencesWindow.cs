using UnityEditor;
using UnityEngine;

namespace Utilities.Editor.EditorWindows
{
    public class FindMissingReferencesWindow : EditorWindow
    {
        [MenuItem("Tools/Find Missing references in scene")]
        public static void FindMissingReferences()
        {
            GameObject[] objects = FindObjectsOfType<GameObject>();

            foreach (GameObject go in objects)
            {
                Component[] components = go.GetComponents<Component>();

                foreach (Component c in components)
                {
                    var so = new SerializedObject(c);
                    SerializedProperty sp = so.GetIterator();

                    while (sp.NextVisible(true))
                        if (sp.propertyType == SerializedPropertyType.ObjectReference)
                            if (sp.objectReferenceValue == null && sp.objectReferenceInstanceIDValue != 0)
                                ShowError(FullObjectPath(go), sp.name);
                }
            }
        }

        private static void ShowError(string objectName, string propertyName)
        {
            Debug.LogError("Missing reference found in: " + objectName + ", Property : " + propertyName);
        }

        private static string FullObjectPath(GameObject go)
        {
            return go.transform.parent == null
                ? go.name
                : FullObjectPath(go.transform.parent.gameObject) + "/" + go.name;
        }
    }
}