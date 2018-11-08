using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Character3D {

    override protected void Start()
    {
        base.Start();
        RefreshHealth(-50f);
    }

    protected override void OnTriggerEnter(Collider other) {
        if (other.tag == "Spell") {
            RefreshHealth(-20f);
        }
        if (other.tag == "Arrow") {
            RefreshHealth(-40f);
        }
        if (other.tag == "Damage")
        {
            RefreshHealth(-40f);
        }

    }

    protected override void Move()
    {
        
    }

    protected override void Rotate()
    {
        
    }
}
