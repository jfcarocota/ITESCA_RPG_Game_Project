using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArea : MonoBehaviour {

    /// <summary>
    /// Represents the GameObjects that area inside the EffectArea
    /// </summary>
    public ArrayList gOInside;
    /// <summary>
    /// Represent the max number of charater that can be heal at the same time, this number includes the caster.
    /// </summary>
    [SerializeField, Range(1,5)]
    protected int maxNumberOfHeals;
    /// <summary>
    /// Represents the size of the collider in where the characters inside get healed.
    /// </summary>
    [Range(.5f,20f)]
    public float areaRadius;
    /// <summary>
    /// This is the collider witch represents the EffectArea of the heal.
    /// </summary>
    [HideInInspector]
    public SphereCollider effectArea;

    private void Start()
    {
        effectArea = GetComponent<SphereCollider>();
        effectArea.radius = areaRadius;
        gOInside = new ArrayList();
        effectArea.enabled = false;//disables the effectArea so is only active when the spell is casted.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!gOInside.Contains(other.gameObject) && gOInside.Count < maxNumberOfHeals -1 )
            {
                gOInside.Add(other.gameObject);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (gOInside.Contains(other.gameObject))
            {
                gOInside.Remove(other.gameObject);
            }
        }
    }

    /// <summary>
    /// Disables the effectArea and clears the gOInside Array
    /// </summary>
    public void DisableEffectArea()
    {
        effectArea.enabled = false;
        gOInside.Clear();
    }


}
