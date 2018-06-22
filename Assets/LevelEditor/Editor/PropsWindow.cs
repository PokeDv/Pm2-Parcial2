using UnityEngine;
using UnityEditor;
using System.Linq;

public class PropsWindow : EditorWindow {

    ListPropDefinition listProps;
    PropType type; 
    int maxWidth;

    [MenuItem("LevelEditor/PropsList")]
    public static void ShowWindow()
    {

        EditorWindow.GetWindow(typeof(PropsWindow));
    }

    private void OnEnable()
    {
        listProps = AssetDatabase.LoadAssetAtPath<ListPropDefinition>("Assets/Scriptables/ListProps.asset");
    }

    void OnGUI()
    {
        //listProps = (ListPropDefinition)EditorGUILayout.ObjectField(listProps,typeof(ListPropDefinition),false);
        if (listProps == null) return;
        type = (PropType)EditorGUILayout.EnumPopup("Primitive to create:", type);
        var list = (type != PropType.ALL)?listProps.listProps.Where (pp => pp.type == type) : listProps.listProps;
        if (list.Any()) {
            int i = 0;
            int rowNum;
            foreach (var prop in list) {
                EditorGUILayout.BeginHorizontal ();
                var preview = AssetPreview.GetAssetPreview (prop.prefab);
                if (GUILayout.Button (preview, GUILayout.Width (50), GUILayout.Height (50))) {
                    var g = GameObject.Instantiate(prop.prefab);
                    Undo.RegisterCreatedObjectUndo(g, "room");
                    Selection.activeObject = g;
                }
            }
        }
    }


}
