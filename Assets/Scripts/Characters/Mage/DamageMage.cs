using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;
using GameCore.ObjectPooler;

public class DamageMage : Character3D {
    
    [SerializeField]
    GameObject spellSpawner;
    [SerializeField, Range(0,100)]
    int manaSpell;

    [SerializeField]
    AudioClip audioFireCast;

    protected override void Start() {
        usesMana = true;
        base.Start();
    }

    override protected void Move() {
        base.Move();
        animator.SetFloat("Velocity", Mathf.Abs(Controllers.Axis.magnitude));
    }

    protected override void Attack() {
        base.Attack();
        if(Controllers.GetFire(1, 2)) {
            if (RefreshMana(-manaSpell)) {
                audioSource.PlayOneShot(audioFireCast);
                animator.SetTrigger("Attack");
                objectPooler.GetObjectFromPool("Spell", spellSpawner.transform.position, spellSpawner.transform.rotation, null);
                objectPooler.GetObjectFromPool("SpellCast", spellSpawner.transform.position, spellSpawner.transform.rotation, spellSpawner.transform);
            }
        }
    }

}
