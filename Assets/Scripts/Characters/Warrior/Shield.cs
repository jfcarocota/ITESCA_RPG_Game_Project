using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {


     //   Debug.Log("Shield");
        //Debug.Log("SCollider: " + other.name + " Padre: " + other.transform.root.name + " Objeto tocado: " + this.name);
       // Debug.Log("PCollider padre: " + other.transform.root.name + " Padre de este: " + this.transform.root.name );

        string padre = other.transform.root.name;
        string padreeste = this.transform.root.name;
        WarriorMan thisone = null;
        WarriorDummy thatone = null;

        WarriorMan otherone = null;
        WarriorDummy otherwise = null;
        if (padreeste.StartsWith("WarriorD") )
        {
          //  Debug.Log("Please work este1");
           thatone = this.transform.root.gameObject.GetComponent<WarriorDummy>();

        }
        else if (padreeste.StartsWith("Warrior") )
        {
            //Debug.Log("Please work este2");
            thisone = this.transform.root.gameObject.GetComponent<WarriorMan>();

        }

        if (padre.StartsWith("WarriorD"))
        {
           // Debug.Log("Please work padre1");
            otherwise = other.transform.root.gameObject.GetComponent<WarriorDummy>();

        }
        else if (padre.StartsWith("Warrior"))
        {
          //  Debug.Log("Please work padre2");
            otherone = this.transform.root.gameObject.GetComponent<WarriorMan>();

        }

        if (other.tag == "Damage" && (this.transform.root.name != other.transform.root.name))
        {
            Debug.Log("El escudo ha sido golpeado por algo que hace daño");
            Debug.Log("WarriorMan: " + otherwise + ":" + otherone + " DummyMan: " + thatone + ":" + thisone);

            if(thatone != null)
            {
                if (thatone.Guarded)
                {
                    Debug.Log("Golpe anulado men"); 
                    otherone.SetCollidersStatus(false, "Sword");
                }
            }
            if(thisone != null)
            {
                if (thisone.Guarded)
                {
                    Debug.Log("Golpe anulado men");
                    otherwise.SetCollidersStatus(false, "Sword");
                }
            }
            


        }
        
       
    }
    private void OnCollisionExit(Collision collision)
    {
        
    }
}

