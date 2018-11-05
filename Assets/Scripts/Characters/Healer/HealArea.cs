using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArea : MonoBehaviour {

    public ArrayList gOInside;
    [SerializeField, Range(1,5)]
    protected int maxNumberOfHeals;
    [Range(.5f,10f)]
    public float areaRadius;
    public SphereCollider effectArea;

    private void Start()
    {
        effectArea = GetComponent<SphereCollider>();
        effectArea.radius = areaRadius;
        gOInside = new ArrayList();
        effectArea.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!gOInside.Contains(other.gameObject) && gOInside.Count < maxNumberOfHeals -1 )
            {
                gOInside.Add(other.gameObject);
                //agregar algun efecto o algo cuando estan dentro del healArea
                //print("Adentro");
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
                //quitar el efecto??
            }
        }
    }

    public void DisableEffectArea()
    {
        effectArea.enabled = false;
        gOInside.Clear();
    }


}
