using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    WarriorMan father;
    
	void Start()
    {
        father = this.transform.root.gameObject.GetComponent<WarriorMan>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {


    
        string padre = other.transform.root.name;
        string padreeste = this.transform.root.name;
        WarriorMan thisone = null;
        WarriorDummy thatone = null;

        WarriorMan otherone = null;
        WarriorDummy otherwise = null;
        if (padreeste.StartsWith("WarriorD") )
        {
           thatone = this.transform.root.gameObject.GetComponent<WarriorDummy>();

        }
        else if (padreeste.StartsWith("Warrior") )
        {
            thisone = this.transform.root.gameObject.GetComponent<WarriorMan>();

        }

        if (padre.StartsWith("WarriorD"))
        {
            otherwise = other.transform.root.gameObject.GetComponent<WarriorDummy>();

        }
        else if (padre.StartsWith("Warrior"))
        {
            otherone = other.transform.root.gameObject.GetComponent<WarriorMan>();

        }

        if (other.tag == "Damage" && (this.transform.root.name != other.transform.root.name))
        {
        //    Debug.Log("The shield has been hit by something that does damage");
         //   Debug.Log("WarriorMan: " + otherwise + ":" + otherone + " DummyMan: " + thatone + ":" + thisone);

            if(thatone != null)
            {
                if (thatone.Guarded)
                {
                    Debug.Log("Hit has been Shielded"); 
                    otherone.SetCollidersStatus(false, "Sword");
                }
            } 
            
            if(thisone != null)
            {
                if (thisone.Guarded)
                {
                    Debug.Log("Hit has been Shielded");
                    otherwise.SetCollidersStatus(false, "Sword");
                }
            }
            


        }        
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        father.audioSourceWarrior.PlayOneShot(father.audioWoosh);
    }
}

