using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelyAtackArea : MonoBehaviour {

    SkelyEnemy skelyScript;

    private void Start()
    {
        skelyScript = transform.GetComponentInParent<SkelyEnemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            skelyScript.attackPlayer = true;
        }
    }
}
