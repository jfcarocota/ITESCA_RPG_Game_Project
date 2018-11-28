using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;

public class WarriorMan : Character3D
{

    
    
    float shieldTime = .1f, currentTime = 0;
    
    //float AttackTimeBonus = .5f;
    float oldMovementSpeed;
    bool cooldown = false;
    public bool Guarded = false;

    [SerializeField]
    public AudioClip audiohit;
    [SerializeField]
    public AudioClip audioWoosh;
    [SerializeField]
    public AudioClip audioShield;

    [HideInInspector]
    public AudioSource audioSourceWarrior;
    


    override protected void Start()
    {
        
        
        base.Start();
        audioSourceWarrior = audioSource;
        Debug.Log("Warrior Man audio source: "+audioSource);
        Debug.Log("Warrior Man audio source: " + audiohit);
        oldMovementSpeed = movementSpeed;
    }

    protected override void Attack()
    {
        base.Attack();

        //Animator animator = GetComponent<Animator>();
    /*
        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
       if (animStateInfo.IsName("Attack") || animStateInfo.IsName("Walk and Attack") || animStateInfo.IsName("Shield and Attack"))
        {
            AttackTime = animStateInfo.length + AttackTimeBonus ;
            Debug.Log("The character is attacking and his length is : "+AttackTime);
            if (Attacking)
            {
                timeAttack += Time.deltaTime;
                if (timeAttack > AttackTime)
                {
                    //    Debug.Log("Attack animation has finished");
                    SetCollidersStatus(false, "Sword");
                    Attacking = false;
                    timeAttack = 0;
                }
            }
        }
        */
        

        if (Controllers.GetFire(1, 1) )
        {
            animator.SetTrigger("Attack");
            
            //   SetCollidersStatus(true, "Sword");

        }

       
    }

    protected override void Move()
    {
      base.Move();
      animator.SetFloat("Velocity", Mathf.Abs(Controllers.Axis.magnitude));  
    }

    protected override void Guard()
    {
        base.Guard();
        if (Controllers.GetFire(2, 1))
        {
            
            animator.SetBool("Guard",true);
            Guarded = true;
            //rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |  RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
            movementSpeed = 0;
        }
        if (Controllers.GetFire(2, 2))
        {
            animator.SetBool("Guard", false);
            Guarded = false;
            currentTime = 0;
            movementSpeed = oldMovementSpeed;
        }
        

    }
    protected override void OnTriggerEnter(Collider other)
    {

        base.OnTriggerEnter(other);
        //  Debug.Log("Collider: "+other.name+ " Padre: " + other.transform.root.name+ " Objeto tocado: "+this.name);
        //  Debug.Log("Collider: " + other.tag + " Padre tag: " + other.transform.root.tag + " Objeto tocado: " + this.tag);

        
       // if (other.transform.root.tag == "Player" ) 
       // {
        //    SetCollidersStatus(false, "Sword");
        //}
        




    }

    override protected void OnCollisionEnter(Collision other)
    {
        if(!Guarded && other.gameObject.tag != "Skelly")
        {
            base.OnCollisionEnter(other);
        }

        
        //Debug.Log("Collision: " + other.collider.name + " Padre: " + other.transform.root.name + " Objeto tocado: " + this.name);
    }
    public void SetCollidersStatus(bool active, string Collider)
    {
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders) { 
            if(collider.name == Collider) { 
        //    Debug.Log("The collider name to disable is: " + collider.name);
            collider.enabled = active;
            }
        }
    }

   
}
