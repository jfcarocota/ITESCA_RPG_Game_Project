using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.ObjectPooler;

#pragma warning disable 0649

public class Spell : PooledObjectBehavior {

    [SerializeField]
    public float damageValue;

    [SerializeField]
    float velocity;
    Rigidbody speelRigidBody;

    bool collide;

    Vector3 lastPosition;

    [SerializeField]
    GameObject spell;
    [SerializeField, Range(0, 2)]
    float scaleFactor;
    [SerializeField, Range(0, 1)]
    float spellStartDecresingTime;

    Vector3 spelltInitialScale;
    float scale;

    DeathSound deathSound;
    AudioSource audioSource;
    [SerializeField]
    AudioClip audioFireExplosion;

    override protected void Awake() {
        audioSource = GetComponent<AudioSource>();
        speelRigidBody = GetComponent<Rigidbody>();
        deathSound = DeathSound.Instance;
    }
    
    override protected void Start() {
        base.Start();
        collide = false;
        speelRigidBody.velocity = transform.forward * velocity * Time.deltaTime;
        spelltInitialScale = spell.transform.localScale;
    }
    
    override protected void Update() {
        base.Update();
        if (!collide) {
            //change velocity
            lastPosition = transform.position;
            speelRigidBody.velocity = transform.forward * velocity * Time.deltaTime;
            //Decrease size
            if (lifeTime > maxLifeTime * spellStartDecresingTime) {
                scale = scaleFactor * Time.deltaTime;
                spell.transform.localScale -= new Vector3(scale, scale, scale);
            }
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player" && other.tag != "NPC" && other.tag != "Damage" && other.tag != "Guard" && other.tag != "Arrows" && other.tag != "HealthPickup" && other.tag != "ManaPickup" && other.tag != "Music") {
            deathSound.PlaySound(transform.position, audioFireExplosion);
            collide = true;
            speelRigidBody.velocity = Vector3.zero;
            objectPooler.GetObjectFromPool("SpellDissolve", lastPosition, transform.rotation, null);
            ReturnObjectToPool();
        }
    }

    override protected void ReturnObjectToPool() {
        collide = false;
        spell.transform.localScale = spelltInitialScale;
        base.ReturnObjectToPool();
    }

}
