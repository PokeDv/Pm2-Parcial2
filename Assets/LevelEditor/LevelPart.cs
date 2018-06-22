using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class LevelPart : MonoBehaviour {

    [HideInInspector]
    public List<Platform> platforms;
    [HideInInspector]
    public List<Transform> rooms;


    private GameObject CreateBlock(Vector3 pos){
        var go = (GameObject)GameObject.Instantiate (Resources.Load ("Block"));
        go.transform.position = pos;
        return go;
    }

    public void CreatePlatform(){
        GameObject.Instantiate (Resources.Load ("Platform"));
    }

    // Update is called once per frame
    void Update () {
        foreach (var platform in platforms) {
            platform.Move();
        }
    }

    public void SetBlock(GameObject go){
        go.transform.localScale = Vector3.one;
        var pos = go.transform.position;
        pos.y = .5f;
        go.transform.position = pos;
    }

    public void SetWalkable(GameObject go){
        go.transform.localScale = Vector3.one*.1f;
        var pos = go.transform.position;
        pos.y = -0.5f;
        go.transform.position = pos;
    }

   

}

public class RoomSettings{
    public Vector3 origin;
    public GameObject door;
}

public enum BlockType{
    BLOCK,
    WALKABLE
}