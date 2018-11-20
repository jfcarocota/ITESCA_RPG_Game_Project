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

}
