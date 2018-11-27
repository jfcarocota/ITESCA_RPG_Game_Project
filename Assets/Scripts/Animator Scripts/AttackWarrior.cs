using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWarrior : StateMachineBehaviour {

    [SerializeField]

	 //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //   Debug.Log("This object: " + this.name + " has started attacking");
        //   Warrior.GetComponent<WarriorMan>().SetCollidersStatus(true, "Sword");
            
            if(animator.GetComponent<WarriorMan>() != null)
            { 
            WarriorMan warrior = animator.GetComponent<WarriorMan>();
            warrior.SetCollidersStatus(true, "Sword");
            warrior.audioSourceWarrior.PlayOneShot(warrior.audioWoosh);
            
        }
            else if (animator.GetComponent<WarriorDummy>())
            {

            WarriorDummy warrior2 = animator.GetComponent<WarriorDummy>();
            warrior2.SetCollidersStatus(true, "Sword");
            }
       

        //  Debug.Log("Padre: " + this);
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //   Warrior.GetComponent<WarriorMan>().SetCollidersStatus(false, "Sword");
        //   Debug.Log("This object: " + this.name + " has finished attacking");
            if (animator.GetComponent<WarriorMan>() != null)
            {
                WarriorMan warrior = animator.GetComponent<WarriorMan>();
                warrior.SetCollidersStatus(false, "Sword");
            }
            else if (animator.GetComponent<WarriorDummy>())
            {

                WarriorDummy warrior2 = animator.GetComponent<WarriorDummy>();
                warrior2.SetCollidersStatus(false, "Sword");
            }

    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
