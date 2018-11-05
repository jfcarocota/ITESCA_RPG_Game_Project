using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;
using GameCore.ObjectPooler;
using UnityEngine.UI;
using GameCore.SystemMovements;

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

	override protected void Start(){
		base.Start ();
		objectPooler = ObjectPooler.Instance;
		contadorflechas = maxArrows;
		textArrows.text = "Flechas: " + contadorflechas;
	}

	override protected void Move() {
        /*if (!Controllers.GetButton (1, "A", 2)) {
			base.Move ();
			anim.SetFloat ("Velocity", Mathf.Abs (rb.velocity.x + rb.velocity.z));
		}*/
        base.Move();
        anim.SetFloat("Velocity", Mathf.Abs(Movement.Axis.magnitude));
	}

	override protected void Attack() {
		if (contadorflechas > 0) {
			if (Controllers.GetButton (1, "A", 1)) {
				anim.SetBool ("Attack", true);
			}
			if (Controllers.GetButton (1, "A", 2)) {
				animStateInfo = anim.GetCurrentAnimatorStateInfo (0);
				if (animStateInfo.IsName ("shoot-still")) {
					objectPooler.GetObjectFromPool ("Arrow", arrowSpawner.transform.position, arrowSpawner.transform.rotation, null);
				}
				anim.SetBool ("Attack", false);
				contadorflechas -= 1;
				textArrows.text = "Flechas: " + contadorflechas;
			}
		}
	}

	override protected void OnTriggerEnter(Collider other){
		if (other.tag.Equals("Arrows")) {
			if ((contadorflechas + 5) > maxArrows) {
				contadorflechas = 10;
			}else {
				contadorflechas += 5;
			}
			textArrows.text = "Flechas: " + contadorflechas;
		}
		Destroy(other.transform.parent.gameObject);
	}
}
