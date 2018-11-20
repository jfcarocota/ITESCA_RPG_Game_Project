using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {
    
    [SerializeField, Range(0, 10)]
    float jumpForce;

    float originalMovementSpeed;
    bool touchGround;
    bool isInAir;
    
    AnimatorStateInfo animStateInfo;

    [SerializeField]
    bool instantiatesSlimes;
    [SerializeField]
    GameObject slimesToInstantiate;

    override protected void Start() {
        base.Start();
        touchGround = false;
        isInAir = true;
        originalMovementSpeed = movementSpeed;
    }
    
    protected override void Move() {
        base.Move();
        if (touchGround) {
            StartCoroutine(Jump());
        }
    }
    
    protected override void OnCollisionEnter(Collision collision) {
        base.OnCollisionEnter(collision);
        if (isInAir && collision.gameObject.tag == "Floor") {
            touchGround = true;
            isInAir = false;
        }
    }

    IEnumerator Jump() {
        touchGround = false;
        movementSpeed = 0;
        animator.SetTrigger("TouchGround");
        yield return new WaitForSeconds(20f / 30f);
        movementSpeed = originalMovementSpeed;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isInAir = true;
    }

    protected override void OnDeath() {
        base.OnDeath();
        if (instantiatesSlimes) {
            GameObject go = null;
            for (int i = 0; i < 2; i++) {
                go = Instantiate(slimesToInstantiate, transform.position, transform.rotation);
                go.GetComponent<Rigidbody>().AddForce( (i == 0? 2f : -2f) * transform.right + Vector3.up * 3, ForceMode.Impulse);
                go.GetComponent<Slime>().playerTransform = playerTransform;
            }
        }
    }

}
