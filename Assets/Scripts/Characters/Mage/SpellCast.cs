using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.ObjectPooler;

#pragma warning disable 0649

public class SpellCast : PooledObjectBehavior {
    
    [SerializeField]
    GameObject blast;

    [SerializeField, Range(0, 2)]
    float scaleFactor;
    [SerializeField, Range(0, 1)]
    float blastInitialScaleFactor;
    [SerializeField, Range(0, 1)]
    float blastStartDecresingTime;

    Vector3 blastInitialScale;
    float scale;
    
    protected override void Start () {
        base.Start();
        blastInitialScale = new Vector3(blastInitialScaleFactor, blastInitialScaleFactor, blastInitialScaleFactor);
        blast.transform.localScale = blastInitialScale;
    }
	
	protected override void Update () {
        base.Update();
        scale = scaleFactor * Time.deltaTime;
        if (lifeTime < maxLifeTime * blastStartDecresingTime) {
            blast.transform.localScale += new Vector3(scale, scale, scale);
        }
        else {
            blast.transform.localScale -= new Vector3(scale, scale, scale);
        }
    }

    protected override void ReturnObjectToPool() {
        blast.transform.localScale = blastInitialScale;
        base.ReturnObjectToPool();
    }
}
