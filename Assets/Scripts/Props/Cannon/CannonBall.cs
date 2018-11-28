using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.ObjectPooler;

public class CannonBall : PooledObjectBehavior {


    
    [SerializeField]
    GameObject smoke;

    [SerializeField]
    float velocity;
    Rigidbody ballRigidBody;

    bool collide;

    
    [SerializeField, Range(-10, 10)]
    float scaleFactor;

    [SerializeField, Range(-10, 10)]
    float smokeStartDecresingTime;

    Vector3 smokeInitialScale;
    float scale;
    [SerializeField]
    float life;
    
    public float AttackValue;

    override protected void Awake()
    {
        ballRigidBody = GetComponent<Rigidbody>();
        
        if (scaleFactor != 0)
        {
            smokeInitialScale = smoke.transform.localScale;
        }
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
            if(scaleFactor != 0) {
                if (lifeTime > maxLifeTime * smokeStartDecresingTime)
                {
                    scale = scaleFactor * Time.deltaTime;
                    smoke.transform.localScale += new Vector3(scale, scale, scale);

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "NPC" && other.tag != "Arrows" && other.tag != "HealthPickup" && other.tag != "ManaPickup" && other.tag != "Music") 
        {
            collide = true;
            ballRigidBody.velocity = Vector3.zero;
           
            ReturnObjectToPool();
        }
    }

    override protected void ReturnObjectToPool()
    {
        collide = false;
        if(scaleFactor != 0) {
            smoke.transform.localScale = smokeInitialScale;
        }
        base.ReturnObjectToPool();
    }
}
