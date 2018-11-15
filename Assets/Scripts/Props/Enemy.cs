using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character3D {

    [Range(0, 99)]
    public int startDamage;

    [SerializeField]
    protected GameObject player;
    protected Vector3 playerPosition;
    protected float distanceToPlayer;
    protected bool tracked;
    [SerializeField]
    protected float trackDistance;

    override protected void Start() {
        base.Start();
        RefreshHealth((float)-startDamage);
    }

    protected override void OnCollisionEnter(Collision collision) {

    }

    protected override void OnTriggerEnter(Collider other) {
        if (other.tag == "Spell") {
            RefreshHealth(-other.gameObject.GetComponent<Spell>().damageValue);
        }
        if (other.tag == "Arrow") {
            RefreshHealth(-40f);
        }
        if (other.tag == "Damage") {
            RefreshHealth(-40f);
        }
    }

    protected override void Move() {
        StartCoroutine(DoCheck());
        if (tracked) {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
    }

    protected override void Rotate() {
        if (tracked) {
            transform.LookAt(new Vector3(playerPosition.x, transform.position.y, playerPosition.z));
        }
    }

    protected bool ProximityCheck() {
        playerPosition = player.transform.position;
        if (Vector3.Distance(transform.position, playerPosition) < trackDistance) {
            return true;
        }
        return false;
    }

    IEnumerator DoCheck() {
        for (;;) {
            tracked = ProximityCheck(); 
            yield return new WaitForSeconds(.5f);
        }
    }

}
