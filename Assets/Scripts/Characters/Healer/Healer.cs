using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;
using GameCore.SystemMovements;

public class Healer : Character3D {

    [SerializeField]
    protected HealArea healArea;
    [SerializeField,Range(0,50)]
    protected int healManaCost;
    protected ParticleSystem particles;

    override protected void Start()
    {
        usesMana = true;//Tells the Character3D to use and activate the Mana bar
        base.Start();
        particles = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// Attack of Heal in the Healer case, contains the logic for the heals
    /// </summary>
    protected override void Attack()
    {
        //if you press the heal button and you have enoght mana
        if (Controllers.GetButton(1, "A", 1) && RefreshMana(-healManaCost))
        {
            healArea.effectArea.enabled = true;//enables the EffectArea trigger
            
            StartCoroutine(WaitAndHeal());
            animator.SetTrigger("Cast");
            particles.Play();
        }
    }
    protected override void Move()
    {
        base.Move();
        animator.SetFloat("Walking", Mathf.Abs(Movement.Axis.magnitude));
    }

    /// <summary>
    /// Cooroutine so the EffectArea is active for .5secconds before turning back off. 
    /// This is also where the heal takes effect, and heals the user and other players inside the EffectArea.
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitAndHeal()
    {
        yield return new WaitForSeconds(.5f);
        RefreshHealth(attackValue * 1f);
        foreach (GameObject player in healArea.gOInside)
        {
            player.GetComponent<Character3D>().RefreshHealth(attackValue * 1f);
        }
        healArea.DisableEffectArea();
    }
    
}
