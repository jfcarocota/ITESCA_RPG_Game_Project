using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("El escudo ha chocado");
        if (other.tag == "Damage")
        {


            WarriorMan another = this.transform.root.gameObject.GetComponent<WarriorMan>();
            another.Guarded = true;
            Debug.Log("Te has cubrido men!!");


        }

    }
}
