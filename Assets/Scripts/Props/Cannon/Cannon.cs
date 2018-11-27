using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;
using GameCore.ObjectPooler;

public class Cannon : Enemy
{

    [SerializeField]
    GameObject ballSpawner;
    [SerializeField]
    GameObject Skelly;
    [SerializeField]
    float timeshoot = 3f;
    bool Attacking = true;
    [SerializeField]
    bool AttackPlayer = false;

    ObjectPooler objectPooler1;

    private Transform myTransform;
    [SerializeField]
    float damping = 6.0f;
    protected override void Start()
    {
        base.Start();
        
        
        
        
    }

    override protected void Move()
    {
        base.Move();
    }
    protected override void Rotate()
    {
        
        
        
        if(Skelly){ 
            Quaternion rotation = Quaternion.LookRotation(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        }

    }
    protected override void Attack()
    {
        base.Attack();

       
        if (Attacking && AttackPlayer)
        {
            StartCoroutine(Shoot());
            objectPooler.GetObjectFromPool("CannonBall", ballSpawner.transform.position, ballSpawner.transform.rotation, null);
            objectPooler.GetObjectFromPool("SmokeRing", ballSpawner.transform.position, ballSpawner.transform.rotation, null);
            Attacking=false;
        }





    }

   
    IEnumerator Shoot()
    {

        
        yield return new WaitForSeconds(timeshoot);
       // Debug.Log("Attack!");
        Attacking = true;

    }
}
