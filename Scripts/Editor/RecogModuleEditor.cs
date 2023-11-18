using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RecogModule), true)]
public class RecogModuleEditor : Editor
{
    RecogModule module = null;

    private void OnEnable()
    {
        module = target as RecogModule;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameObject obj = module.gameObject;

        EditorGUI.BeginDisabledGroup(!EditorApplication.isPlaying || !EditorApplication.isPlaying && Recog.Instance != null && Recog.Instance.CurrentSelected == obj);
        
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Select"))
            module.Select();
        
        if(GUILayout.Button("Deselect"))
            Recog.Instance?.ClearActive(obj);

        EditorGUILayout.EndHorizontal();
        EditorGUI.EndDisabledGroup();
    }
}
