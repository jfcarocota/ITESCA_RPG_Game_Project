using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {
    
    [SerializeField, Range(0, 10)]
    float jumpForce;

    float originalMovementSpeed;
    bool touchGround;
    bool isInAir;
    bool canAttackAgain;
    
    [SerializeField]
    bool instantiatesSlimes;
    [SerializeField]
    GameObject slimesToInstantiate;

    [SerializeField]
    AudioClip audioJump, audioDamageSlime;

    override protected void Start() {
        base.Start();
        canAttackAgain = true;
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
        if (collision.gameObject.tag == "Player") {
            Vector3 knockbak = player.transform.position - transform.position;
            knockbak = new Vector3(knockbak.x, 0f, knockbak.z);
            knockbak = knockbak.normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(knockbak * 4 + Vector3.up * 2, ForceMode.Impulse);
            if (canAttackAgain) {
                canAttackAgain = false;
                collision.gameObject.GetComponent<Character3D>().RefreshHealth(-attackValue);
                if(gameObject.activeSelf)
                    StartCoroutine(MakeCanAttackAgain());
            }
        }
        else if (isInAir && collision.gameObject.tag == "Floor") {
            touchGround = true;
            isInAir = false;
        }
    }

    protected override void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
        if (gameObject.activeSelf) {
            if (other.tag == "Spell") {
                audioSource.PlayOneShot(audioDamageSlime);
            }
            else if (other.tag == "Arrow") {
                audioSource.PlayOneShot(audioDamageSlime);
            }
            else if (other.tag == "Damage") {
                audioSource.PlayOneShot(audioDamageSlime);
            }
        }
    }

    IEnumerator Jump() {
        touchGround = false;
        movementSpeed = 0;
        audioSource.PlayOneShot(audioJump);
        animator.SetTrigger("TouchGround");  
        yield return new WaitForSeconds(20f / 30f);
        movementSpeed = originalMovementSpeed;
        rb.AddForce(transform.right * Random.Range(-2f, 2f) + Vector3.up * jumpForce, ForceMode.Impulse);
        isInAir = true;
    }

    protected override void OnDeath() {
        base.OnDeath();
        if (instantiatesSlimes) {
            GameObject go = null;
            for (int i = 0; i < 2; i++) {
                go = Instantiate(slimesToInstantiate, transform.position, transform.rotation);
                go.GetComponent<Rigidbody>().AddForce( (i == 0? 2f : -2f) * transform.right + Vector3.up * 3, ForceMode.Impulse);
                go.GetComponent<Slime>().player = player;
            }
        }
    }

    IEnumerator MakeCanAttackAgain() {
        yield return new WaitForSeconds(1f);
        canAttackAgain = true;
    }

}
