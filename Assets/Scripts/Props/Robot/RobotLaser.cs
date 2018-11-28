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
        playerTransform = PartyManager.members[0].transform;
        StartCoroutine(CheckLeader());
    }
	
	// Update is called once per frame
	void Update () {
        Quaternion rotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, .5f);

        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, transform.position + transform.forward * 15);

        if (Physics.Raycast(transform.position, transform.forward * 15, out hit)) {
            if (hit.collider) {
                laserHitParticle.transform.position = hit.point;
                if (canHitAgain && hit.collider.gameObject.tag == "Player") {
                    hit.collider.gameObject.GetComponent<Character3D>().RefreshHealth(-5);
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
        //transform.LookAt(transform.rotation, rotation, .4f);
        laserHitParticle.transform.position = transform.position;
        canHitAgain = true;
    }

    IEnumerator CheckLeader() {
        do {
            playerTransform = PartyManager.members[0].transform;
            yield return new WaitForSeconds(.1f);
        } while (PartyManager.members.Length > 0);
    }
}
