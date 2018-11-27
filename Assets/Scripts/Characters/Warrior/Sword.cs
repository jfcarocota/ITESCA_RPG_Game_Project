using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    WarriorMan father;

    void Start()
    {
        father = this.transform.root.gameObject.GetComponent<WarriorMan>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        father.audioSourceWarrior.PlayOneShot(father.audioWoosh);
    }
}
