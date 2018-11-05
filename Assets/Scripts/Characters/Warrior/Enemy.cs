using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character3D
{
    
     protected override void Start () {
        base.Start();
    }

    
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Damage" )
        {
            RefreshHealth(-30f);
        }



    }

    protected override void Move()
    {



    }
    protected override void Rotate()
    {

    }



}
