using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class Robot : Enemy {
    
    [SerializeField]
    GameObject foot;
    [SerializeField]
    GameObject stepParticles;
    Vector3 stepParticlesInitScale;
    [SerializeField]
    float trackDistanceToStep;
    bool trackedToStep;
    bool isSteping;
    [SerializeField]
    float stepParticlesIncreasingScale;
    float trackDistanceToStepHit;
    bool trackedToStepHit;
    bool stepParticlesOn;

    [SerializeField]
    GameObject lasers;
    [SerializeField]
    float minTrackDistanceToLaser;
    [SerializeField]
    float maxTrackDistanceToLaser;
    bool trackedToLaser;
    bool isLasering;
    [SerializeField]
    GameObject redEyeRight, redEyeLeft;
    Vector3 redEyeFinalScale;

    [SerializeField]
    GameObject beforeRobot, afterRobot;

    float originalMovementSpeed;

    AudioSource audioSource2;
    [SerializeField]
    AudioClip audioSounds, audioDamageRobot, audioDeathRobot, audioLeg, audioStep, audioLaser;

    protected override void Start() {
        base.Start();
        audioSource2 = GetComponents<AudioSource>()[1];
        audioSource2.clip = audioSounds;
        audioSource2.Play();
        originalMovementSpeed = movementSpeed;
        trackedToStep = false;
        isSteping = false;
        stepParticlesOn = false;
        stepParticlesInitScale = stepParticles.transform.localScale;
        StartCoroutine(CheckProximityToPlayerForStep());
        redEyeFinalScale = redEyeRight.transform.localScale;
        redEyeRight.transform.localScale = Vector3.zero;
        redEyeLeft.transform.localScale = Vector3.zero;
        player = PartyManager.members[0];
        StartCoroutine(CheckLeaderGO());
    }

    protected override void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
        if (gameObject.activeSelf) {
            if (other.tag == "Spell") {
                audioSource.PlayOneShot(audioDamageRobot);
            }
            else if (other.tag == "Arrow") {
                audioSource.PlayOneShot(audioDamageRobot);
            }
            else if (other.tag == "Damage") {
                audioSource.PlayOneShot(audioDamageRobot);
            }
        }
        if (healthValue <= 0) {
            deathSound.PlaySound(transform.position, audioDeathRobot);
        }
    }

    protected override void Move() {
        if (tracked && !isSteping && !isLasering) {
            animator.SetFloat("Speed", 1);
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
        if (!tracked) {
            animator.SetFloat("Speed", 0);
        }
        if (trackedToStep && !isSteping && !isLasering) {
            StartCoroutine(Step());
        }
        if (trackedToLaser && !isSteping && !isLasering) {
            StartCoroutine(Laser());
        }
        if (healthValue <= 0) {
            OnDeath();
        }
        if (stepParticlesOn) {
            stepParticles.transform.localScale += Vector3.one * stepParticlesIncreasingScale * Time.deltaTime;
            if (!trackedToStepHit) {
                float distance = Vector3.Distance(foot.transform.position, player.transform.position);
                trackDistanceToStepHit = stepParticles.transform.localScale.x;
                trackedToStepHit = distance < trackDistanceToStepHit;
                if (trackedToStepHit) {
                    Vector3 knockbak = player.transform.position - foot.transform.position;
                    knockbak = new Vector3(knockbak.x, 0f, knockbak.z);
                    knockbak = knockbak.normalized;
                    player.GetComponent<Rigidbody>().AddForce(knockbak * 4 + Vector3.up * 4, ForceMode.Impulse);
                    player.gameObject.GetComponent<Character3D>().RefreshHealth(-attackValue);
                }
            }
        }
        if (isLasering) {
            if (redEyeRight.transform.localScale.x < redEyeFinalScale.x) {
                redEyeRight.transform.localScale += Vector3.one * Time.deltaTime * .06f;
                redEyeLeft.transform.localScale += Vector3.one * Time.deltaTime * .06f;
            }
        }
    }

    private void OnDrawGizmos() {
        if (stepParticlesOn) {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(foot.transform.position, transform.forward * stepParticles.transform.localScale.x);
        }
    }

    protected override void Rotate() {
        if (tracked && !isLasering) {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        }
    }

    //Step: foot hits ground in 36/46 > 1 * 6/30 
    IEnumerator Step() {
        //activar animacion y quitar velocidad
        isSteping = true;
        animator.SetTrigger("Step");
        movementSpeed = 0f;
        audioSource.PlayOneShot(audioLeg);
        //esperar el pisoton
        yield return new WaitForSeconds(1.2f);
        //poner animacion idle
        animator.SetFloat("Speed", 0);
        //activar la hitbox y animacion del polvo
        audioSource.PlayOneShot(audioStep);
        foot.SetActive(true);
        stepParticlesOn = true;
        //esperar parado un ratin
        yield return new WaitForSeconds(.6f);
        //desactivar la hitbox y animacion del polvo
        foot.SetActive(false);
        stepParticlesOn = false;
        trackedToStepHit = false;
        stepParticles.transform.localScale = stepParticlesInitScale;
        //poner velocidad
        animator.SetFloat("Speed", 1);
        movementSpeed = originalMovementSpeed;
        isSteping = false;
    }
    
    IEnumerator Laser() {
        //activar animacion y quitar velocidad
        isLasering = true;
        animator.SetTrigger("LaserOn");
        audioSource.PlayOneShot(audioLaser);
        movementSpeed = 0f;
        //esperar la cabecita
        yield return new WaitForSeconds(1f);
        //activar el laser
        lasers.SetActive(true);
        //esperar el laser
        yield return new WaitForSeconds(4f);
        //desactivar el laser 
        lasers.SetActive(false);
        //poner velocidad
        animator.SetTrigger("LaserOff");
        movementSpeed = originalMovementSpeed;
        isLasering = false;
        redEyeRight.transform.localScale = Vector3.zero;
        redEyeLeft.transform.localScale = Vector3.zero;
    }

    protected override void OnDeath() {
        music.PlayMusic(1);
        beforeRobot.SetActive(false);
        afterRobot.SetActive(true);
        base.OnDeath();
    }

    IEnumerator CheckProximityToPlayerForStep() {
        for (; ; ) {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            trackedToStep = distance < trackDistanceToStep;
            trackedToLaser = distance < maxTrackDistanceToLaser && distance > minTrackDistanceToLaser;
            yield return new WaitForSeconds(.5f);
        }
    }
    
    IEnumerator CheckLeaderGO() {
        do {
            player = PartyManager.members[0];
            yield return new WaitForSeconds(.1f);
        } while (PartyManager.members.Length > 0);
    }
}
