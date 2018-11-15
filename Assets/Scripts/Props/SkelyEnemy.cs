using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelyEnemy : Enemy {

    public bool followPlayer;
    public bool attackPlayer;
    [SerializeField]
    protected BoxCollider hurtBox;
    [SerializeField]
    protected SphereCollider attackArea;
    private bool firstTracked;


    override protected void Start()
    {
        base.Start();
        followPlayer = false;
        animator = GetComponent<Animator>();
        RefreshHealth(-50);
        firstTracked = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.tag == "Spell")
        {
            animator.SetTrigger("Damage");
            StartCoroutine(StopAndWait(1f));
        }
        if (other.tag == "Arrow")
        {
            animator.SetTrigger("Damage");
            StartCoroutine(StopAndWait(1f));
        }
        if (other.tag == "Damage")
        {
            animator.SetTrigger("Damage");
            StartCoroutine(StopAndWait(1f));
        }
    }

    protected override void Move()
    {
        animator.SetFloat("Speed", !tracked ? 0 : followPlayer ? 1 : 0 );
        if (followPlayer | firstTracked)
        {
            base.Move();
            firstTracked = tracked ? false : firstTracked;
            followPlayer = !firstTracked;
        }
        
            
    }
    protected override void Attack()
    {
        
        if (attackPlayer)
        {
            attackPlayer = false;
            animator.SetTrigger("Attack");
            StartCoroutine(AttackAndWait(2f));
        }
    }

    private IEnumerator StopAndWait(float waitTime)
    {
        animator.SetFloat("Speed", 0);
        followPlayer = false;
        yield return new WaitForSeconds(waitTime);
        animator.SetFloat("Speed", 1);
        followPlayer = true;
    }

    private IEnumerator AttackAndWait(float waitTime)
    {
        animator.SetFloat("Speed", 0);
        followPlayer = false;
        attackArea.enabled = false;
        yield return new WaitForSeconds(waitTime/4);
        hurtBox.enabled = true;
        yield return new WaitForSeconds(waitTime / 4);
        hurtBox.enabled = false;
        yield return new WaitForSeconds(waitTime / 2);
        animator.SetFloat("Speed", 1);
        followPlayer = true;
        attackArea.enabled = true;
    }

}
