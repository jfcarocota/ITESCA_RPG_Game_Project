using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingBag : Character3D {

    [SerializeField, Range(0, 100)]
    int tookDamage;

    override protected void Start() {
        base.Start();
    }

    protected override void Move() {

    }
    protected override void Rotate() {

    }

    protected override void OnTriggerEnter(Collider other) {
        RefreshHealth(-tookDamage);
    }

}
