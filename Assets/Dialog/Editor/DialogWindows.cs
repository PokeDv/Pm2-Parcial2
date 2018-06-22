using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DialogWindows : EditorWindow {
    
    [MenuItem("DialogEditor/Open")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(DialogWindows));
    }

    void OnGUI()
    {
        
    }

}
