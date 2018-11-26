using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFloating : MonoBehaviour {

    [SerializeField]
    GameObject prefab;
    [SerializeField, Range(0f, 5f)]
    float rotationSpeed;
    [SerializeField, Range(0f, 10f)]
    float verticalSpeed;
    [SerializeField, Range(0f, 0.05f)]
    float verticalSinInterval;
    float verticalTime;
    
    // Use this for initialization
    void Start () {
        GameObject o = Instantiate(prefab, transform.position, transform.rotation);
        o.transform.parent = transform;
        verticalTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (!MenuController.isPaused) {
            //Object rotation
            transform.Rotate(0f, rotationSpeed, 0f);
            //Object vertical movement
            verticalTime += Time.deltaTime;
            transform.position += Vector3.up * Mathf.Sin(verticalSpeed * verticalTime) * verticalSinInterval;
        }
    }



}
