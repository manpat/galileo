using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(WaypointThing))]
public class InspectorSavePosition : Editor {
	public override void OnInspectorGUI(){
		WaypointThing wt = (WaypointThing) target;
		if(!wt) return;

		serializedObject.Update();
		Show(serializedObject.FindProperty("savedPositions"), wt);
		serializedObject.ApplyModifiedProperties();

		if(GUILayout.Button("Save Waypoint")){
			wt.savedPositions.Add(wt.transform.position);
		}
	}

	public void Show(SerializedProperty prop, WaypointThing wt){
		EditorGUILayout.PropertyField(prop);

		if(prop.isExpanded){
			EditorGUI.indentLevel += 1;
			for (int i = 0; i < wt.savedPositions.Count; i++) {
				Vector3 p = wt.savedPositions[i];
				EditorGUILayout.BeginHorizontal();

					EditorGUILayout.LabelField(p.ToString());
					if(GUILayout.Button("Go")){
						wt.transform.position = wt.savedPositions[i];
					}
					if(GUILayout.Button("X")){
						wt.savedPositions.RemoveAt(i);
					}

				EditorGUILayout.EndHorizontal();
			}
			EditorGUI.indentLevel -= 1;
		}
	}
}
