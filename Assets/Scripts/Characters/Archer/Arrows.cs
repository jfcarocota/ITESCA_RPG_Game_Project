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

    AudioSource audioSource;
    [SerializeField]
    AudioClip audioShotHit;

	void Start(){
        audioSource = GetComponent<AudioSource>();
		rb = GetComponent<Rigidbody>();
		objectPooler = ObjectPooler.Instance;
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
	}

    void OnTriggerEnter(Collider other){
		if (other.tag != "Player" && other.tag != "NPC" && other.tag != "Damage" && other.tag != "Guard" && other.tag != "Arrows" && other.tag != "HealthPickup" && other.tag != "ManaPickup") {
            audioSource.PlayOneShot(audioShotHit);
            rb.velocity = Vector3.zero;
			rb.useGravity = false;
			objectPooler.ReturnObjectToPool ("Arrow", gameObject);
		}
	}

}
