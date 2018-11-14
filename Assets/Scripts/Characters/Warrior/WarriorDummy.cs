using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;


public class WarriorDummy: Character3D
{

    
   
    float shieldTime = .1f, currentTime = 0;
    
    //float AttackTimeBonus = .5f;
    float oldMovementSpeed;
    bool cooldown = false;
    public bool Guarded = false;
   

    override protected void Start()
    {


        base.Start();
        

    }

    protected override void Attack()
    {
        base.Guard();
        
        if (Controllers.GetFire(4, 1))
        {

            animator.SetBool("Guard", true);
            Guarded = true;

        }
        else if (Controllers.GetFire(4, 2))
        {
            cooldown = true;
            if (currentTime > shieldTime)
            {
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

            if (currentTime > shieldTime)
            {
                currentTime = 0;
                cooldown = false;
                
            }
        }



    }

    protected override void Move()
    {
      
  

    }
    protected override void Rotate()
    {
        
    }

    protected override void Guard()
    {
        
       /* checkAnimation = GetComponent<Animator>().GetComponent<Animation>();

        Animator animator = GetComponent<Animator>();



        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (animStateInfo.IsName("Attack") || animStateInfo.IsName("Walk and Attack") || animStateInfo.IsName("Shield and Attack"))
        {
            AttackTime = animStateInfo.length + AttackTimeBonus;
            Debug.Log("The character is attacking and his length is : " + AttackTime);

           



        }
        if (Attacking)
        {
         //   Debug.Log("Time attack!! " + timeAttack);
            timeAttack += Time.deltaTime;

            if (timeAttack > AttackTime)
            {
                //    Debug.Log("Attack animation has finished");
                SetCollidersStatus(false, "Sword");
                Attacking = false;
                timeAttack = 0;
            }
        }
        */

        if (Controllers.GetFire(3, 1))
        {
            animator.SetTrigger("Attack");
           // SetCollidersStatus(true, "Sword");
            
        }

    }

   
    public void SetCollidersStatus(bool active, string Collider)
    {
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            if (collider.name == Collider)
            {
                //    Debug.Log("The collider name to disable is: " + collider.name);
                collider.enabled = active;
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {

        //  Debug.Log("Collider: "+other.name+ " Padre: " + other.transform.root.name+ " Objeto tocado: "+this.name);
        //  Debug.Log("Collider: " + other.tag + " Padre tag: " + other.transform.root.tag + " Objeto tocado: " + this.tag);

        if (other.tag == "Damage" && (this.name != other.transform.root.name))
        {
            RefreshHealth(-30f);
           // Debug.Log("Daño hecho");
        }

        if (other.transform.root.tag == "Player")
        {
            SetCollidersStatus(false, "Sword");
        }





    }


}
