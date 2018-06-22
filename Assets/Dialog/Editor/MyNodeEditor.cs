using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
public class MyNodeEditor : EditorWindow
{

    private List<Node> _allNodes;

    private bool _isDragging;

    [MenuItem("Ejemplos/NodeEditor")]
    static void OpenMyNodeEditor()
    {
        MyNodeEditor m = (MyNodeEditor)GetWindow(typeof(MyNodeEditor));
        m.Show();
        m.Init();
    }

    Node selectedTarget;
    Node selectedInput;

    public void Init()
    {
        _allNodes = new List<Node>();
    }

    void CreateDialog ()
    {
        int i = 0;
        foreach (var item in _allNodes) {
            SentenceScriptable asset = ScriptableObject.CreateInstance<SentenceScriptable>();
            asset.time = item.time;
            asset.title = item.title;
            asset.msg= item.msg;
            AssetDatabase.CreateAsset(asset, "Assets/Sentence-"+(++i)+".asset");
            AssetDatabase.SaveAssets();
        }
    }

    void OnGUI()
    {
        _isDragging = EditorGUILayout.Toggle("Dragging?", _isDragging);
        maxSize = new Vector2(800, 600);
        minSize = new Vector2(800, 600);
        if (_allNodes == null)
            _allNodes = new List<Node>();
        EditorGUILayout.BeginHorizontal();
       
        if (GUILayout.Button("Add Node"))
            CreateNode();
        if (GUILayout.Button("Create Dialog"))
            CreateDialog();
        EditorGUILayout.EndHorizontal();
        BeginWindows();
        for (int i = _allNodes.Count - 1; i >= 0; i--)
        {
            _allNodes[i].window = GUI.Window(i, _allNodes[i].window, _allNodes[i].DrawNodeWindow, "(" + i + ")");
        }
        EndWindows();

        Handles.color = new Color(0, 0, .75f);
        foreach (var node in _allNodes)
        {
            if(node.tgt!=null)
            {
                Handles.DrawLine(node.window.position+node.tgtRect.center,node.tgt.window.position+node.tgt.resRect.center);
            }
        }

        if(selectedInput!=null)
        {
            Handles.DrawSolidDisc(selectedInput.window.position + selectedInput.tgtRect.center, Vector3.forward, 10);
        }
        if(selectedTarget!=null)
        {
            Handles.DrawSolidDisc(selectedTarget.window.position + selectedTarget.resRect.center, Vector3.forward, 10);
        }
    }

    void OnInputClick(Node n)
    {
        if (selectedTarget == null)
        {
            selectedInput = n;
        }
        else if (n == selectedInput)
        {
            selectedInput = null;
        }
        else
        {
            n.tgt = selectedTarget;
            selectedTarget = null;
            selectedInput = null;
        }
    }

    void OnResultClick(Node n)
    {
        if (selectedInput == null)
        {
            selectedTarget = n;
        }
        else if (n == selectedTarget)
        {
            selectedTarget = null;
        }
        else
        {
            selectedInput.tgt = n;
            selectedTarget = null;
            selectedInput = null;
        }
    }

    private void CreateLine(Rect a, Rect b)
    {
        Handles.DrawLine(a.position, b.position);
    }

    private void CreateNode()
    {
        int current = 0;
        while (_allNodes.Count < current && _allNodes[current] != null)
        {
            current++;
        }
        Node n = new Node(current);

        n.OnInputClick += OnInputClick;
        n.OnResultClick += OnResultClick;

        if (_allNodes.Count >= current)
            _allNodes.Add(n);
        else
            _allNodes[current] = n;

    }
}

