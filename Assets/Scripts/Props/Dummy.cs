using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Character3D {

    [Range (0,99)]
    public int startDamage;

    override protected void Start()
    {
        base.Start();
        RefreshHealth((float)-startDamage);
    }

    protected override void OnTriggerEnter(Collider other) {
        if (other.tag == "Spell") {
            RefreshHealth(-other.gameObject.GetComponent<Spell>().damageValue);
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
