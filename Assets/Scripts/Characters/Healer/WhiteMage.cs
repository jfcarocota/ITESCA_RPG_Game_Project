﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;
using GameCore.SystemMovements;

public class WhiteMage : Character3D {

    [SerializeField]
    protected HealArea healArea;
    [SerializeField,Range(0,50)]
    protected int healManaCost;
    protected Animator anim;
    protected ParticleSystem particles;

    override protected void Start()
    {
        usesMana = true;
        base.Start();
        RefreshHealth(-50f);
        particles = GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
    }

    protected override void Attack()
    {
        if (Controllers.GetButton(1, "A", 1) && RefreshMana(-healManaCost))
        {
            healArea.effectArea.enabled = true;
            
            StartCoroutine(WaitAndHeal());
            anim.SetTrigger("Cast");
            particles.Play();
        }
        /*
        else if(Controllers.GetButton(1, "A", 1) && !RefreshMana(-healManaCost))
        {
            //hacer algo si no hay mana??
        }
        */
    }
    protected override void Move()
    {
        /*if(Controllers.GetJoystick(1,1).y != 0)
        {
            anim.SetBool("Walking",true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }*/
        base.Move();
        anim.SetFloat("Walking", Mathf.Abs(Movement.Axis.magnitude));
    }

    private IEnumerator WaitAndHeal()
    {
        yield return new WaitForSeconds(.5f);
        RefreshHealth(attackValue * 1f);
        foreach (GameObject player in healArea.gOInside)
        {
            player.GetComponent<Character3D>().RefreshHealth(attackValue * 1f);
            //print("Curado");
        }
        healArea.DisableEffectArea();
    }
    
}
