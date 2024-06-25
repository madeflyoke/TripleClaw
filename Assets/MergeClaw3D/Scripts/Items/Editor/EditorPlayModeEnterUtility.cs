#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace MergeClaw3D.Scripts.Items.Editor
{
    [InitializeOnLoad]
    public static class EditorPlayModeEnterUtility 
    {
        static EditorPlayModeEnterUtility()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange stateChange)
        {
            if (stateChange == PlayModeStateChange.ExitingEditMode)
            {
                TryClearItemsPreview();
            }
        }

        private static void TryClearItemsPreview()
        {
            GameObject objectToDestroy = GameObject.Find("PreviewItemsContainer");

            if (objectToDestroy != null)
            {
                Object.DestroyImmediate(objectToDestroy);
            }
        }
    }
}

#endif
