using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(PlaneManager))]
public class PlaneEditor : Editor
{
    PlaneManager planeManager;
    private Editor shapeEditor;
    public bool ShapeSettingsFouldOut;

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                planeManager.UpdatePlaneMesh();
            }
            foreach (var item in planeManager.ShapeSettings)
            {
                DrawSettingsEditor(item, planeManager.UpdatePlaneMesh, ref ShapeSettingsFouldOut, ref shapeEditor);

            }
        }
        if (GUILayout.Button("GeneratePlane"))
        {
            planeManager.CreatePlane();
        }
    }
    private void OnEnable()
    {
        planeManager = (PlaneManager)target;
    }

    private void DrawSettingsEditor(Object _settings, System.Action _onSettingsChanged, ref bool _fouldOut, ref Editor _editor)
    {
        if (_settings == null) { return; }

        _fouldOut = EditorGUILayout.InspectorTitlebar(_fouldOut, _settings);

        using (var check = new EditorGUI.ChangeCheckScope())
        {

            if (_fouldOut)
            {
                // CachedEditor checks if the reference is null, if so it creates a new one to that reference. if not, it overrides editor values
                CreateCachedEditor(_settings, null, ref _editor);
                _editor.OnInspectorGUI();
            }

            if (check.changed)
            {
                _onSettingsChanged.Invoke();
            }
        }




    }
}
