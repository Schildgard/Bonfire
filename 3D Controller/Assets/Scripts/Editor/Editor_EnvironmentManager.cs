using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(EnvironmentManager))]
public class Editor_EnvironmentManager : Editor
{

    private EnvironmentManager manager;


    public override void OnInspectorGUI()
    {


        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();

          //  if (check.changed)
          //  {
          //      manager.UpdateEnvironment();
          //  }
        }

        if (GUILayout.Button("Generate Environment"))
        {
            manager.Initialize();
        }
        if (GUILayout.Button("Keep Environmental Prefabs"))
        {
            manager.ClearPrefabList();
        }

        if (GUILayout.Button("Remove Environmental Prefabs"))
        {
            manager.RemoveEnvironmentPrefabs();
        }


    }


    private void OnEnable()
    {
        manager = (EnvironmentManager)target;
    }
}
