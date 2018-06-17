using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public Vector3 MoveBy;
    Vector3 pointA;
    Vector3 pointB;
    public bool going_to_a;
	// Use this for initialization
	void Start () {
        this.pointA = transform.position;
        this.pointB = transform.position + MoveBy;
        going_to_a = false;
	}
    bool isArrived(Vector3 pos, Vector3 target){
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos,target)<0.02f;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 my_pos = this.transform.position;
        Vector3 target;
        if (going_to_a)
        {
            target = this.pointA;
        }
        else
        {
            target = this.pointB;
        }
        Vector3 destination = target - my_pos;
        destination.z = 0;
        if (isArrived(this.transform.position, destination)) {
            going_to_a = !going_to_a;
        }
	}
}
