using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Character3D {

    override protected void Start()
    {
        base.Start();
        RefreshHealth(-50f);
    }

    protected override void Move()
    {
        
    }
    protected override void Rotate()
    {
        
    }
}
