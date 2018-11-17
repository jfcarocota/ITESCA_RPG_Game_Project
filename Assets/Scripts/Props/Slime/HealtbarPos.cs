﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtbarPos : MonoBehaviour {
    
    Transform target;

    // Use this for initialization
    void Start () {
        target = GameObject.Find("Main Camera").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(target.position);
    }
}
