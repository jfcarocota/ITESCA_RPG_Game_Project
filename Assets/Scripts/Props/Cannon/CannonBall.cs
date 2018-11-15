using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.ObjectPooler;

public class CannonBall : PooledObjectBehavior {


    [HideInInspector]
    public float damageValue;

    [SerializeField]
    float velocity;
    Rigidbody ballRigidBody;

    bool collide;

    
    [SerializeField, Range(0, 2)]
    float scaleFactor;
    [SerializeField, Range(0, 1)]
    float spellStartDecresingTime;

    Vector3 spelltInitialScale;
    float scale;

    override protected void Awake()
    {
        ballRigidBody = GetComponent<Rigidbody>();
        //damageValue = GameObject.Find("Cannon").GetComponent<Enemy>().attackValue;
    }

    override protected void Start()
    {
        base.Start();
        collide = false;
        //ballRigidBody.velocity = transform.forward * velocity * Time.deltaTime;
        
    }

    override protected void Update()
    {
        base.Update();
        if (!collide)
        {
            //change velocity
            // ballRigidBody.velocity = transform.forward * velocity * Time.deltaTime;
            transform.position += transform.forward * Time.deltaTime * velocity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "FollowEnemyTrigger")
        {
            collide = true;
            ballRigidBody.velocity = Vector3.zero;
           
            ReturnObjectToPool();
        }
    }

    override protected void ReturnObjectToPool()
    {
        collide = false;
        
        base.ReturnObjectToPool();
    }
}
