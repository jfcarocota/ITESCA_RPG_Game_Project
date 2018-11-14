using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelyEnemy : Character3D {

    public bool followPlayer;
    public Vector3 playerPosition;
    public bool attackPlayer;
    protected Animator anim;
    [SerializeField]
    protected BoxCollider hurtBox;
    [SerializeField]
    protected SphereCollider attackArea;


    override protected void Start()
    {
        base.Start();
        followPlayer = false;
        anim = GetComponent<Animator>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spell")
        {
            RefreshHealth(-other.gameObject.GetComponent<Spell>().damageValue);
            anim.SetTrigger("Damage");
            StartCoroutine(StopAndWait(1f));
        }
        if (other.tag == "Arrow")
        {
            RefreshHealth(-40f);
            anim.SetTrigger("Damage");
            StartCoroutine(StopAndWait(1f));
        }
        if (other.tag == "Damage")
        {
            RefreshHealth(-40f);
            anim.SetTrigger("Damage");
            StartCoroutine(StopAndWait(1f));
        }
    }


    protected override void Move()
    {
        if (followPlayer)
        {
            float step = movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);
            transform.rotation = Quaternion.LookRotation(playerPosition- transform.position );
            anim.SetFloat("Speed",1);
        }
    }

    protected override void Attack()
    {
        if (attackPlayer)
        {
            print("ATAC");
            attackPlayer = false;
            anim.SetTrigger("Attack");
            StartCoroutine(AttackAndWait(2f));
        }
    }

    private IEnumerator StopAndWait(float waitTime)
    {
        anim.SetFloat("Speed", 0);
        followPlayer = false;
        yield return new WaitForSeconds(waitTime);
        anim.SetFloat("Speed", 1);
        followPlayer = true;
    }

    private IEnumerator AttackAndWait(float waitTime)
    {
        anim.SetFloat("Speed", 0);
        followPlayer = false;
        attackArea.enabled = false;
        yield return new WaitForSeconds(waitTime/4);
        hurtBox.enabled = true;
        yield return new WaitForSeconds(waitTime / 4);
        hurtBox.enabled = false;
        yield return new WaitForSeconds(waitTime / 2);
        anim.SetFloat("Speed", 1);
        followPlayer = true;
        attackArea.enabled = true;
    }

}
