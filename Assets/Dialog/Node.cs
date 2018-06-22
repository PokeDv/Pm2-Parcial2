using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;

public class Node
{
   
    public Rect window;
    public int id;

    public String title;
    public String msg;
    public float time;

    public Node tgt;

    public Rect tgtRect;
    public Rect resRect;

    public System.Action<Node> OnInputClick = (n) => { };
    public System.Action<Node> OnResultClick = (n) => { };

    Rect dragArea
    {
        get{
            var r = new Rect(0, 0, window.width, 15);
            return r;
        }
    }

    public Node(int id = -1, int posX = 50, int posY = 50, int width = 300, int height = 200)
    {
        this.id = id;
        window = new Rect(posX, posY, width, height);
    }

    bool dragging;

    public void DrawNodeWindow(int id)
    {
        dragging |= (dragArea.Contains(Event.current.mousePosition) && Event.current.type == EventType.mouseDown);
        dragging &= Event.current.type != EventType.mouseUp;
        if(dragging)
            GUI.DragWindow();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("", GUILayout.Width(20)))
            OnInputClick(this);
        
		tgtRect = GUILayoutUtility.GetLastRect();
   
        EditorGUILayout.EndHorizontal();

        title = EditorGUILayout.TextField("Title", title);
        msg = EditorGUILayout.TextField("Message", msg);
        time  = EditorGUILayout.FloatField("Time", time);

        EditorGUILayout.BeginHorizontal();
        GUIStyle style = new GUIStyle(EditorStyles.label);
        style.alignment = TextAnchor.MiddleRight;
        //EditorGUILayout.LabelField(Calculate().ToString(),style, GUILayout.Width(65));
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("", GUILayout.Width(20)))
            OnResultClick(this);
        
        resRect = GUILayoutUtility.GetLastRect();
        EditorGUILayout.EndHorizontal();

    }



}
