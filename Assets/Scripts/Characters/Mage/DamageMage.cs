using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;
using GameCore.ObjectPooler;

public class DamageMage : Character3D {
    
    [SerializeField]
    Animator anim;
    [SerializeField]
    GameObject spellSpawner;
    [SerializeField, Range(0,100)]
    int manaSpell;

    ObjectPooler objectPooler;

    protected override void Start() {
        usesMana = true;
        base.Start();
        objectPooler = ObjectPooler.Instance;
    }

    override protected void Move() {
        base.Move();
        if (Mathf.Abs(Controllers.GetJoystick(1, 1).x) > 0) {
            anim.SetFloat("Velocity", 1);
        }
        else {
            anim.SetFloat("Velocity", Mathf.Abs(rb.velocity.x + rb.velocity.z));
        }
    }

    protected override void Attack() {
        base.Attack();
        if(Controllers.GetButton(1, "A", 2)) {
            if (RefreshMana(-manaSpell)) {
                anim.SetTrigger("Attack");
                objectPooler.GetObjectFromPool("Spell", spellSpawner.transform.position, spellSpawner.transform.rotation, null);
                objectPooler.GetObjectFromPool("SpellCast", spellSpawner.transform.position, spellSpawner.transform.rotation, spellSpawner.transform);
            }
        }
    }

}
