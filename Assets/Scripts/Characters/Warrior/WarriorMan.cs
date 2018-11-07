using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;
using GameCore.SystemMovements;

public class WarriorMan : Character3D
{

    
    [SerializeField]
    float shieldTime = .1f, currentTime = 0;
    [SerializeField]
    float AttackTimeBonus = .5f;
    float oldMovementSpeed;
    bool cooldown = false;
    public bool Guarded = false;
    public bool Attacking = false;
    public float Damage = 10;
    public float AttackTime = 0, timeAttack=0;
   

    override protected void Start()
    {
        
        
        base.Start();
        
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
        

        if (Controllers.GetButton(1, "A", 1) )
        {
            animator.SetTrigger("Attack");
         //   SetCollidersStatus(true, "Sword");
            Attacking = true;
        }

       
    }

    protected override void Move()
    {
        base.Move();
        animator.SetFloat("Velocity", Mathf.Abs(Movement.Axis.magnitude));
        
    }

    protected override void Guard()
    {
        base.Guard();
        if (Controllers.GetButton(1, "B", 1))
        {

            animator.SetBool("Guard",true);
            Guarded = true;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |  RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;

        } else if (Controllers.GetButton(1, "B", 2))
            {
                cooldown = true;
            if (currentTime > shieldTime) {
                animator.SetBool("Guard", false);
                Guarded = false;
                currentTime = 0;
            }
                 
            

            }

        if (cooldown)
        {
            currentTime += Time.deltaTime;
        }

        if (!animator.GetBool("Guard") && cooldown)
        {
            
            if(currentTime > shieldTime)
            {
                currentTime = 0;
                cooldown = false;
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionY;
            }
        }

    }
    protected override void OnTriggerEnter(Collider other)
    {

     //  Debug.Log("Collider: "+other.name+ " Padre: " + other.transform.root.name+ " Objeto tocado: "+this.name);
      //  Debug.Log("Collider: " + other.tag + " Padre tag: " + other.transform.root.tag + " Objeto tocado: " + this.tag);

        if (other.tag == "Damage"  && (this.name != other.transform.root.name))
        {
            RefreshHealth(-30f);
        }
        
        if (other.transform.root.tag == "Player" ) 
        {
            SetCollidersStatus(false, "Sword");
        }
        




    }

    protected  void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision: " + other.collider.name + " Padre: " + other.transform.root.name + " Objeto tocado: " + this.name);
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
