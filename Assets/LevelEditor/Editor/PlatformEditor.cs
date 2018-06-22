using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
[CustomEditor(typeof(Platform))]
public class PlatformEditor : Editor {

    Platform myTarget;

    void OnEnable(){
        myTarget = (Platform)target;
    }

    public override void OnInspectorGUI(){
        base.OnInspectorGUI ();
        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button ("Add Waypoint")) {
            myTarget.waypoint.Add (myTarget.transform.position+myTarget.transform.forward);
        }
        if(myTarget.waypoint.Any())
            if (GUILayout.Button ("Remove Waypoint")) 
                myTarget.waypoint.RemoveAt (myTarget.waypoint.Count-1);
        
        EditorGUILayout.EndHorizontal();
    }

    public void OnSceneGUI ()
    {
        for (int i = 0; i < myTarget.waypoint.Count; i++)
        {
            var next = (i + 1) % myTarget.waypoint.Count;
            Handles.DrawDottedLine(myTarget.waypoint[i], myTarget.waypoint[next], 2);

        }

        Handles.BeginGUI ();
        for (int i = 0; i < myTarget.waypoint.Count; i++) {
            Handles.Label (myTarget.waypoint [i], "wp-"+i);
            myTarget.waypoint [i] = Handles.PositionHandle (myTarget.waypoint [i], Quaternion.identity);
           
        }

        Handles.EndGUI ();
        SceneView.RepaintAll ();
    }

}
