using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;

public class WarriorDummy: Character3D
{

    [SerializeField]
    Animator anim;
    [SerializeField]
    float shieldTime = .1f, currentTime = 0;
    [SerializeField]
    float AttackTimeBonus = .5f;
    float oldMovementSpeed;
    bool cooldown = false;
    public bool Guarded = false;
    public bool Guarded2 = false;
    public bool Attacking = false;
    public float Damage = 10;
    public float AttackTime = 0, timeAttack = 0;
    public Animation checkAnimation;

    override protected void Start()
    {


        base.Start();
        

    }

    protected override void Attack()
    {
        base.Guard();
        
        if (Controllers.GetButton(1, "Y", 1))
        {

            anim.SetBool("Guard", true);
            Guarded = true;

        }
        else if (Controllers.GetButton(1, "Y", 2))
        {
            cooldown = true;
            if (currentTime > shieldTime)
            {
                anim.SetBool("Guard", false);
                Guarded = true;
                currentTime = 0;
            }



        }

        if (cooldown)
        {
            currentTime += Time.deltaTime;
        }

        if (!anim.GetBool("Guard") && cooldown)
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

        checkAnimation = GetComponent<Animator>().GetComponent<Animation>();

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


        if (Controllers.GetButton(1, "X", 1))
        {
            anim.SetTrigger("Attack");
            SetCollidersStatus(true, "Sword");
            Attacking = true;
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
        }

        if (other.tag == "Player")
        {
            SetCollidersStatus(false, "Sword");
        }





    }


}
