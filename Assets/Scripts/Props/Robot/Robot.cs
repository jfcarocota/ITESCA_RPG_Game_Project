using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Enemy {

    [SerializeField]
    GameObject footColliderGO;
    //SphereCollider footCollider;
    [SerializeField]
    float trackDistanceToStep;
    bool trackedToStep;
    bool isSteping;

    [SerializeField]
    GameObject lasers;
    [SerializeField]
    float minTrackDistanceToLaser;
    [SerializeField]
    float maxTrackDistanceToLaser;
    bool trackedToLaser;
    bool isLasering;

    float originalMovementSpeed;

    protected override void Start() {
        base.Start();
        originalMovementSpeed = movementSpeed;
        //footCollider = footColliderGO.GetComponentInChildren<SphereCollider>();
        trackedToStep = false;
        isSteping = false;
        StartCoroutine(CheckProximityToPlayerForStep());
    }

    protected override void Move() {
        if (tracked && !isSteping && !isLasering) {
            animator.SetFloat("Speed", 1);
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
        if(!tracked) {
            animator.SetFloat("Speed", 0);
        }
        if (trackedToStep && !isSteping && !isLasering) {
            StartCoroutine(Step());
        }
        if (trackedToLaser && !isSteping && !isLasering) {
            StartCoroutine(Laser());
        }
        if (healthValue <= 0) {
            OnDeath();
        }
    }

    protected override void Rotate() {
        if (tracked && !isSteping && !isLasering) {
            transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
        }
    }

    //Step: foot hits ground in 36/46 > 1 * 6/30 
    IEnumerator Step() {
        //activar animacion y quitar velocidad
        isSteping = true;
        animator.SetTrigger("Step");
        movementSpeed = 0f;
        //esperar el pisoton
        yield return new WaitForSeconds(1.2f);
        //poner animacion idle
        animator.SetFloat("Speed", 0);
        //activar la hitbox y animacion del polvo
        footColliderGO.SetActive(true);
        //esperar parado un ratin
        yield return new WaitForSeconds(.6f);
        //desactivar la hitbox y animacion del polvo
        footColliderGO.SetActive(false);
        //poner velocidad
        animator.SetFloat("Speed", 1);
        movementSpeed = originalMovementSpeed;
        isSteping = false;
    }
    
    IEnumerator Laser() {
        //activar animacion y quitar velocidad
        isLasering = true;
        animator.SetTrigger("LaserOn");
        movementSpeed = 0f;
        //esperar la cabecita
        yield return new WaitForSeconds(1f);
        //activar el laser
        lasers.SetActive(true);
        //esperar el laser
        yield return new WaitForSeconds(4f);
        //desactivar el laser 
        lasers.SetActive(false);
        //poner velocidad
        animator.SetTrigger("LaserOff");
        movementSpeed = originalMovementSpeed;
        isLasering = false;
    }

    IEnumerator CheckProximityToPlayerForStep() {
        for (; ; ) {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            print(distance);
            trackedToStep = distance < trackDistanceToStep;
            trackedToLaser = distance < maxTrackDistanceToLaser && distance > minTrackDistanceToLaser;
            yield return new WaitForSeconds(.5f);
        }
    }


    protected override void OnCollisionEnter(Collision collision) {

    }

    protected override void OnTriggerEnter(Collider other) {

    }

}
