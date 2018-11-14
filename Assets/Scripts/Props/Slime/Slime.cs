using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {
    
    [SerializeField, Range(0, 10)]
    float jumpForce;

    public bool touchingGround;

    override protected void Start() {
        base.Start();
        touchingGround = false;
    }
    
    protected override void Move() {
        base.Move();
        if (touchingGround) {
            touchingGround = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    
    protected override void OnCollisionEnter(Collision collision) {
        base.OnCollisionEnter(collision);
        if (collision.gameObject.tag == "Floor") {
            touchingGround = true;
        }
    }
    
}
