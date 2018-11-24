using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBodyCollider : MonoBehaviour {

    Robot robotScript;

    private void Start() {
        robotScript = gameObject.GetComponentInParent<Robot>();
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Spell") {
            robotScript.RefreshHealth(-other.gameObject.GetComponent<Spell>().damageValue);
            robotScript.Knockback();
        }
        else if (other.tag == "Arrow") {
            robotScript.RefreshHealth(-40f);
            robotScript.Knockback();
        }
        else if (other.tag == "Damage") {
            robotScript.RefreshHealth(-40f);
            robotScript.Knockback();
        }
    }

}
