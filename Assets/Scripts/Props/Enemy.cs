using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameCore.ObjectPooler;

public class Enemy : Character3D {

    [Range(0, 99)]
    public int startDamage;
    
    protected GameObject player;
    [SerializeField]
    NavMeshAgent agent;
    protected float distanceToPlayer;
    protected bool tracked;
    [SerializeField]
    protected float trackDistance;

    [SerializeField, Range(0,10)]
    float knockbackForce;
    
    override protected void Start() {
        base.Start();
        RefreshHealth((float)-startDamage);
        player = PartyManager.members[0];
        StartCoroutine(CheckProximityToPlayer());
        StartCoroutine(CheckLeader());
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
            OnDeath();
        }
    }
    
    protected override void Move() {
        if (tracked) {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
            //agent.SetDestination(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
        }
    }

    protected override void Rotate() {
        if (tracked) {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        }
    }
    
    IEnumerator CheckProximityToPlayer() {
        for (;;) {
            tracked = Vector3.Distance(transform.position, player.transform.position) < trackDistance;
            yield return new WaitForSeconds(.5f);
        }
    }

    public virtual void Knockback() {
        Vector3 knockbak = transform.position - player.transform.position;
        rb.AddForce(knockbak.normalized * knockbackForce, ForceMode.Impulse);
    }

    protected virtual void OnDeath() {
        objectPooler.GetObjectFromPool("EnemyExplosion", transform.position, transform.rotation, null);
        Destroy(gameObject);
    }

    IEnumerator CheckLeader() {
        do {
            player = PartyManager.members[0];
            yield return new WaitForSeconds(.1f);
        } while (PartyManager.members.Length > 0);
    }

}
