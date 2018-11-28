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

    [SerializeField]
    protected AudioClip audioAttack;
    [SerializeField]
    protected AudioClip audioBones;
    [SerializeField]
    protected AudioClip audioDamageSkely;

    protected AudioSource walkingSounds;


    override protected void Start()
    {
        base.Start();
        followPlayer = false;
        animator = GetComponent<Animator>();
        firstTracked = true;
        walkingSounds = GetComponents<AudioSource>()[1];
        walkingSounds.clip = audioBones;
        walkingSounds.Play();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.tag == "Spell" && healthValue > 0)
        {
            animator.SetTrigger("Damage");
            StartCoroutine(StopAndWait(1f));
            audioSource.PlayOneShot(audioDamageSkely);
        }
        if (other.tag == "Arrow" && healthValue > 0)
        {
            animator.SetTrigger("Damage");
            StartCoroutine(StopAndWait(1f));
            audioSource.PlayOneShot(audioDamageSkely);
        }
        if (other.tag == "Damage" && healthValue > 0)
        {
            animator.SetTrigger("Damage");
            StartCoroutine(StopAndWait(1f));
            audioSource.PlayOneShot(audioDamageSkely);
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
        /*
        if (tracked && !walkingSounds.isPlaying)
        {
            walkingSounds.Play();
        }
        else if(!tracked && walkingSounds.isPlaying)
        {
            walkingSounds.Stop();
        }*/

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
        audioSource.PlayOneShot(audioAttack);
        yield return new WaitForSeconds(waitTime / 4);
        hurtBox.enabled = false;
        yield return new WaitForSeconds(waitTime / 2);
        animator.SetFloat("Speed", 1);
        followPlayer = true;
        attackArea.enabled = true;
    }

}
