using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character3D {

    [Range(0, 99)]
    public int startDamage;

    [SerializeField]
    protected Transform playerTransform;
    protected float distanceToPlayer;
    protected bool tracked;
    [SerializeField]
    protected float trackDistance;

    [SerializeField, Range(0,10)]
    float knockbackForce;

    override protected void Start() {
        base.Start();
        RefreshHealth((float)-startDamage);
        StartCoroutine(DoCheck());
    }

    protected override void OnCollisionEnter(Collision collision) {

    }

    protected override void OnTriggerEnter(Collider other) {
        if (other.tag == "Spell") {
            RefreshHealth(-other.gameObject.GetComponent<Spell>().damageValue);
            Knockback();
        }
        else if (other.tag == "Arrow") {
            RefreshHealth(-40f);
            Knockback();
        }
        else if (other.tag == "Damage") {
            RefreshHealth(-40f);
            Knockback();
        }
        if (healthValue <= 0) {
            Destroy(gameObject);
        }
    }

    protected override void Move() {
        if (tracked) {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
    }

    protected override void Rotate() {
        if (tracked) {
            transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
        }
    }

    protected bool ProximityCheck() {
        if (Vector3.Distance(transform.position, playerTransform.position) < trackDistance) {
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

    void Knockback() {
        Vector3 knockbak = transform.position - playerTransform.position;
        rb.AddForce(knockbak.normalized * knockbackForce, ForceMode.Impulse);
    }

}
