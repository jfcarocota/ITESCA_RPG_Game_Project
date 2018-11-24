using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLaser : MonoBehaviour {

    [SerializeField]
    Transform playerTransform;

    LineRenderer laser;
    RaycastHit hit;

    // Use this for initialization
    void Start () {
        laser = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        Quaternion rotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, .4f);

        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, transform.position + transform.forward * 15);

        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            if (hit.collider) {
               //print(hit.collider.gameObject.tag);
            }
        }
	}
}
