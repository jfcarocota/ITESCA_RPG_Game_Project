using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLaser : MonoBehaviour {

    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    GameObject laserHitParticle;
    
    LineRenderer laser;
    RaycastHit hit;

    bool canHitAgain;

    // Use this for initialization
    void Start () {
        laser = GetComponent<LineRenderer>();
        canHitAgain = true;
    }
	
	// Update is called once per frame
	void Update () {
        Quaternion rotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, .4f);

        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, transform.position + transform.forward * 15);

        if (Physics.Raycast(transform.position, transform.forward * 15, out hit)) {
            if (hit.collider) {
                laserHitParticle.transform.position = hit.point;
                if (canHitAgain && hit.collider.gameObject.tag == "Player") {
                    hit.collider.gameObject.GetComponent<DamageMage>().RefreshHealth(-5);
                    canHitAgain = false;
                    StartCoroutine(HitAgain());
                }
            }
        }

	}

    /*private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * 15);
    }*/

    IEnumerator HitAgain() {
        yield return new WaitForSeconds(1f);
        canHitAgain = true;
    }

    private void OnEnable() {
        canHitAgain = true;
    }
}
