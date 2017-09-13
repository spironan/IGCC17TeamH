using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

    public float speed;
    public Vector3 target;
    Vector3 dir;
    bool reached = false;
	// Use this for initialization
	void Start () {
        Debug.Log("Transform position : " + transform.localPosition);
        dir = Vector3.Normalize(target - transform.localPosition);
	}
	
	// Update is called once per frame
	void Update () {
        if (!reached)
        {
            transform.localPosition += dir * speed;
            if (Vector3.Distance(transform.localPosition, target) <= 1.0f)
            {
                reached = true;
            }
        }
	}
}
