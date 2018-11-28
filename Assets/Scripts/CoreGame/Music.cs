using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    #region Singleton
    public static Music Instance;
    private void Awake() {
        Instance = this;
    }
    #endregion
    
    AudioSource audioSource;
    [SerializeField]
    AudioClip villageMusic, forestMusic, battleMusic;
        
    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = villageMusic;
        audioSource.Play();
    }

    public void PlayMusic(int song) {
        switch (song) {
            case 1: audioSource.clip = forestMusic; break;
            case 2: audioSource.clip = battleMusic; break;
            case 3: audioSource.clip = villageMusic; break;
        }
        audioSource.Play();
    }

}
