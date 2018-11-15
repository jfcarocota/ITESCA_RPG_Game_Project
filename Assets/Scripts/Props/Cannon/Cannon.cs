using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;
using GameCore.ObjectPooler;

public class Cannon : MonoBehaviour {

    [SerializeField]
    GameObject ballSpawner;
    ObjectPooler objectPooler;
    public float attackValue;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Shoot();
	}

    void Shoot()
    {
        if (Controllers.GetFire(1, 2))
        {
            
                
                objectPooler.GetObjectFromPool("Spell", ballSpawner.transform.position, ballSpawner.transform.rotation, null);
                objectPooler.GetObjectFromPool("SpellCast", ballSpawner.transform.position, ballSpawner.transform.rotation, ballSpawner.transform);
            
        }



    }
}
