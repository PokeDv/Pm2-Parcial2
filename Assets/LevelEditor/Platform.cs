using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Platform : MonoBehaviour {

    public List<Vector3> waypoint;
    public float speed = 1;

    int currentIndex = 0;



    public void Move(){
        if (!waypoint.Any())
            return;
            
        var deltaPos = waypoint [currentIndex] - transform.position;
        var magnitude = deltaPos.magnitude;
        transform.position += deltaPos / magnitude * speed / 100;
        if (magnitude < .1f)
            currentIndex = ++currentIndex % waypoint.Count;
    }
}