﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;
using GameCore.ObjectPooler;
using UnityEngine.UI;

public class Archer : Character3D {

	[SerializeField]
	Animator anim;
	[SerializeField]
	GameObject arrowSpawner;
	ObjectPooler objectPooler;
	[SerializeField]
	Text textArrows;
	[SerializeField]
	int maxArrows;
	int contadorflechas;

	AnimatorStateInfo animStateInfo;

    float originalMovementSpeed;

	override protected void Start(){
		base.Start ();
        objectPooler = ObjectPooler.Instance;
        originalMovementSpeed = movementSpeed;
        contadorflechas = maxArrows;
		textArrows.text = "x " + contadorflechas;
	}

	override protected void Move() {
        if (Controllers.GetFire(1, 0)) {
            movementSpeed = 0;
            anim.SetFloat("Velocity", 0f);
        }
        else {
            movementSpeed = originalMovementSpeed;
            anim.SetFloat("Velocity", Mathf.Abs(Controllers.Axis.magnitude));
        }
        base.Move();
    }

	override protected void Attack() {
		if (contadorflechas > 0) {
			if (Controllers.GetFire (1, 1)) {
				anim.SetBool ("Attack", true);
			}
			if (Controllers.GetFire (1, 2)) {
				animStateInfo = anim.GetCurrentAnimatorStateInfo (0);
				if (animStateInfo.IsName ("shoot-still")) {
					objectPooler.GetObjectFromPool ("Arrow", arrowSpawner.transform.position, arrowSpawner.transform.rotation, null);
                    contadorflechas -= 1;
                    textArrows.text = "x " + contadorflechas;
                }
				anim.SetBool ("Attack", false);
			}
		}
	}

	override protected void OnTriggerEnter(Collider other){
        base.OnTriggerEnter(other);
		if (other.tag.Equals("Arrows")) {
			if ((contadorflechas + 5) > maxArrows) {
				contadorflechas = 10;
			}else {
				contadorflechas += 5;
			}
			textArrows.text = "x " + contadorflechas;
            Destroy(other.transform.parent.gameObject);
        }
	}
}
