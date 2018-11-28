using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.ObjectPooler;

public class Arrows : MonoBehaviour {

	[SerializeField]
	protected string poolTag;
	[SerializeField]
	float force;
	Rigidbody rb;
	ObjectPooler objectPooler;

    DeathSound deathSound;
    AudioSource audioSource;
    [SerializeField]
    AudioClip audioShotHit;

    bool firstEnable;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        firstEnable = true;
    }

    void Start() {
        deathSound = DeathSound.Instance;
        audioSource = GetComponent<AudioSource>();
		objectPooler = ObjectPooler.Instance;
	}

    void OnTriggerEnter(Collider other){
		if (other.tag != "Player" && other.tag != "NPC" && other.tag != "Damage" && other.tag != "Guard" && other.tag != "Arrows" && other.tag != "HealthPickup" && other.tag != "ManaPickup" && other.tag != "Music") {
            if(other.tag != "Floor")
                deathSound.PlaySound(transform.position, audioShotHit);
            rb.velocity = Vector3.zero;
			rb.useGravity = false;
			objectPooler.ReturnObjectToPool("Arrow", gameObject);
		}
	}

    private void OnEnable() {
        if(!firstEnable) {
            rb.AddForce(transform.forward * force, ForceMode.Impulse);
        }
        else {
            firstEnable = false;
        }
    }

}
