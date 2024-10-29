using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Locations))]
public class LocationsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Populate from Children"))
            ((Locations)target).PopulateFromChildObjects();
    }
}
