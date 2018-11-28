using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    WarriorMan father;
    
	void Start()
    {
        father = this.transform.parent.parent.GetComponent<WarriorMan>();
    }

   
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag != "Player" && other.tag != "NPC" && other.tag != "Damage" && other.tag != "Guard" && other.tag != "Arrows" && other.tag != "HealthPickup" && other.tag != "ManaPickup" && other.tag != "Music") {
        {
            father.audioSourceWarrior.PlayOneShot(father.audioShield);
            print("Escudo: " + other.gameObject.name);
                if (other.gameObject.GetComponent<Character3D>())
                {
                    father.RefreshHealth(other.gameObject.GetComponent<Character3D>().attackValue);
                }
        }
       
        }
    }


}

