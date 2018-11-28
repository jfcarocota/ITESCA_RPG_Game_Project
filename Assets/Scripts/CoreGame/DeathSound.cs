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
    [SerializeField]
    AudioClip audioVictory, audioFailure, audioMega;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
    public void PlaySound(Vector3 pos, AudioClip clip) {
        transform.position = pos;
        audioSource.PlayOneShot(clip);
    }

    public void PlayVictory(Vector3 pos) {
        transform.position = pos;
        audioSource.PlayOneShot(audioVictory);
    }

    public void PlayFailure(Vector3 pos) {
        transform.position = pos;
        audioSource.PlayOneShot(audioFailure);
    }

    public void PlayMega(Vector3 pos) {
        transform.position = pos;
        audioSource.PlayOneShot(audioMega);
    }

}
