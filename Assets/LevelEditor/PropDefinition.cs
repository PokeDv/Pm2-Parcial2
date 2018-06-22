using UnityEngine;


[CreateAssetMenu(fileName = "Prop", menuName = "LevelProp")]
public class PropDefinition : ScriptableObject {
    public GameObject prefab;
    public PropType type;
}

