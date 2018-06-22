using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Block))]
public class BlockEditor : Editor {

    int size = 25;
    public void OnSceneGUI ()
    {
        Handles.BeginGUI ();

        var pos = Camera.current.WorldToScreenPoint(((Block)target).transform.position);
        pos.y = Camera.current.pixelHeight - pos.y + size*1.5f;

        if (GUI.Button (new Rect (pos, new Vector2 (size, size)), "x"))
            DestroyImmediate (((Block)target).gameObject);
        
        Handles.EndGUI ();
        SceneView.RepaintAll ();
    }


}

