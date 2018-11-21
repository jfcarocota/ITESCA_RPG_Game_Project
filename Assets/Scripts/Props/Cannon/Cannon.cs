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
    float timeshoot=3f;
    bool Attacking = true;

    ObjectPooler objectPooler;

    protected override void Start()
    {
        base.Start();
        objectPooler = ObjectPooler.Instance;
        
    }

    override protected void Move()
    {
        base.Move();
    }

    protected override void Attack()
    {
        base.Attack();

       
        if (Attacking)
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
