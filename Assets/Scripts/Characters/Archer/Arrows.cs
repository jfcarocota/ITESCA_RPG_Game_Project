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

	void Start(){
		rb = GetComponent<Rigidbody>();
		objectPooler = ObjectPooler.Instance;
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
	}

    void OnTriggerEnter(Collider other){
		if (other.name != "Archer" & other.name != "Arrows" && other.name != "HealthPickup") {
			rb.velocity = Vector3.zero;
			rb.useGravity = false;
			objectPooler.ReturnObjectToPool ("Arrow", gameObject);
		}
	}

}
