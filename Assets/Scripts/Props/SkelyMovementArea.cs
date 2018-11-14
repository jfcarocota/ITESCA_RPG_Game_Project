using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelyMovementArea : MonoBehaviour {

    [SerializeField]
    private SphereCollider attackArea;
    SkelyEnemy skelyScript;

    private void Start()
    {
        skelyScript = transform.GetComponentInParent<SkelyEnemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            skelyScript.followPlayer = true;
            skelyScript.playerPosition = other.transform.position;
            attackArea.enabled = true;
        }
    }
    

    protected void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            skelyScript.playerPosition = other.transform.position;
        }
    }
}
