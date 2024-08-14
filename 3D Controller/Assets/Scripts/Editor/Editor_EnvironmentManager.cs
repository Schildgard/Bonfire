using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(AreaCollection))]
public class Editor_EnvironmentManager : Editor
{

    private AreaCollection manager;


    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
        }

        if (GUILayout.Button("Generate Environment"))
        {
            manager.GenerateEnvironment();
        }
        if (GUILayout.Button("Keep Environmental Prefabs"))
        {
            manager.KeepPrefabsInScene();
        }

        if (GUILayout.Button("Remove Environmental Prefabs"))
        {
            manager.RemoveEnvironmentFromScene();
        }
    }


    private void OnEnable()
    {
        manager = (AreaCollection)target;
    }
}
