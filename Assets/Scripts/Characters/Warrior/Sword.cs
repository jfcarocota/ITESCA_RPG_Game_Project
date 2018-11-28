using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    WarriorMan father;

    void Start()
    {
        father = this.transform.parent.parent.GetComponent<WarriorMan>();
        
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "NPC" && other.tag != "Damage" && other.tag != "Guard" && other.tag != "Arrows" && other.tag != "HealthPickup" && other.tag != "ManaPickup" && other.tag != "Music")
        {
            
            father.audioSourceWarrior.PlayOneShot(father.audiohit);
            father.SetCollidersStatus(false, "Sword");
            print("Sword: " + other.tag);

        }
    }
}
