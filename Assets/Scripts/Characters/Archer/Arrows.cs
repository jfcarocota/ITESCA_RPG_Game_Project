using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.ObjectPooler;

public class Arrows : MonoBehaviour {

	[SerializeField]
	protected string poolTag;
	[SerializeField]
	float velocity;
	Rigidbody rb;
	ObjectPooler objectPooler;

	void Start(){
		rb = GetComponent<Rigidbody>();
		objectPooler = ObjectPooler.Instance;
		rb.velocity = transform.forward * velocity * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other){
		if (other.name != "Archer") {
			rb.velocity = Vector3.zero;
			rb.useGravity = false;
			objectPooler.ReturnObjectToPool ("Arrow", gameObject);
		}
	}

}
