using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSound : MonoBehaviour {
    
    #region Singleton
    public static DeathSound Instance;
    private void Awake() {
        Instance = this;
    }
    #endregion

    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
    public void PlaySound(Vector3 pos, AudioClip clip) {
        transform.position = pos;
        audioSource.PlayOneShot(clip);
    }
}
