using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelPart))]
public class LevelPartEditor : Editor {

    LevelPart myTarget;

    int width;
    int height;
    bool sizeError;

    bool showRooms;
    bool showPlatforms;

    void OnEnable(){
        myTarget = (LevelPart)target;
        Tools.hidden = true;
    }

    void OnDisable()
    {
        Tools.hidden = false;
    }

    public override void OnInspectorGUI(){
        base.OnInspectorGUI ();

        width = EditorGUILayout.IntField ("width", width);
        height = EditorGUILayout.IntField ("height", height);
        if (sizeError) {
            EditorGUILayout.HelpBox("Width Or Height with wrong values. \n min value '3'",MessageType.Error);
        }


        if (myTarget.rooms != null && myTarget.rooms.Count > 0) {
            EditorGUILayout.BeginVertical (EditorStyles.helpBox);
            showRooms = EditorGUILayout.Foldout (showRooms, "Rooms");
            if(showRooms){
                for (int i = 0; i < myTarget.rooms.Count; i++) {
                    var room = myTarget.rooms [i];
                    if (GUILayout.Button ("Remove Room " + i )) {
                        myTarget.rooms.RemoveAt (i);
                        DestroyImmediate(room.gameObject);
                    }
                }

            }
            EditorGUILayout.EndVertical ();

            EditorGUILayout.Space ();
        }
       


        if (GUILayout.Button ("Add Room")) {

            if (width < 3 || height < 3) {
                sizeError = true;
                return;
            } else if (width >= 3 && height >= 3) {
                sizeError = false;
            }

            var room = new GameObject ("Room - " + (myTarget.rooms.Count+1));
            room.transform.SetParent (myTarget.transform, true);
            myTarget.rooms.Add(room.transform);
           
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    var block = ((GameObject)(GameObject.Instantiate (Resources.Load ("Block")))).GetComponent<Transform> ();
                    block.SetParent (room.transform, false);
                    block.localPosition = Vector3.right * i + Vector3.forward * j;
                    if (IsWall (i, j, width, height)) {
                        var block2 = ((GameObject)(GameObject.Instantiate (Resources.Load ("Block")))).GetComponent<Transform> ();
                        block2.name = i +"-"+ j;
                        block2.SetParent (room.transform, false);
                        block2.localPosition = Vector3.right * i + Vector3.forward * j+Vector3.up;
                    }
                }
            }
        }

        GUILayout.Space (10);

        if (myTarget.platforms.Count > 0) {
            EditorGUILayout.BeginVertical (EditorStyles.helpBox);
            showPlatforms = EditorGUILayout.Foldout (showPlatforms, "Platforms");
            if(showPlatforms){
                for (int i = 0; i < myTarget.platforms.Count; i++) {
                    var platform = myTarget.platforms [i];
                    if (GUILayout.Button ("Remove Platform " + i )) {
                        myTarget.platforms.RemoveAt (i);
                        DestroyImmediate(platform.gameObject);
                    }
                }

            }
            EditorGUILayout.EndVertical ();

            EditorGUILayout.Space ();
        }


        if (GUILayout.Button ("Add Platform")) {
            var platform = ((GameObject)(GameObject.Instantiate (Resources.Load ("Platform")))).GetComponent<Platform> ();
            platform.transform.SetParent (myTarget.transform, true);
            myTarget.platforms.Add (platform);
        }

        GUILayout.Space (10);
       
        GUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();

        if (GUILayout.Button ("Clear All",GUILayout.Width(100))) {
            foreach (var item in myTarget.rooms) 
                DestroyImmediate (item.gameObject);
            foreach (var item in myTarget.platforms) 
                DestroyImmediate (item.gameObject);
            myTarget.rooms.Clear ();
            myTarget.platforms.Clear ();
        }

        GUILayout.EndHorizontal();
       
    }

    bool IsWall (int i, int j, int width, int height)
    {
        return i == width-1 || j == height-1 || i == width-1 || j == height-1 || i == 0 || j == 0;
    }

    public void OnSceneGUI ()
    {
        Handles.BeginGUI ();
        for (int i = 0; i < myTarget.rooms.Count; i++) {
            Handles.Label (myTarget.rooms[i].position, "Room-"+i);
            myTarget.rooms[i].position = Handles.PositionHandle(myTarget.rooms[i].position, Quaternion.identity);
        }
        Handles.EndGUI ();
        SceneView.RepaintAll ();
    }

}

