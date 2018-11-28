using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemMovements;
using UnityEngine.UI;
using GameCore.ObjectPooler;
using GameCore.TalkSystem;

[RequireComponent(typeof(Animator))]
public abstract class Character3D : MonoBehaviour {

    protected float healthValue;
    protected int manaValue;
    [SerializeField, Range(1, 1000)]
    protected float maxHealthValue;
    [SerializeField, Range(50,1000)]
    protected int maxManaValue;
    [SerializeField]
    protected float movementSpeed;
    [SerializeField]
    protected float guardValue;
    [SerializeField]
    public float attackValue;
    protected Rigidbody rb;
    protected bool usesMana;

    [SerializeField]
    protected GameObject healthBar;
    protected Image healthBarValue;
    [SerializeField]
    protected GameObject manaBar;
    protected Image manaBarValue;
    [HideInInspector]
    public int partyNumber;

    protected Animator animator;

    [SerializeField]
    protected float followDistance;

    protected ObjectPooler objectPooler;
    private TalkSystem talkSystem;
    protected DeathSound deathSound;
    protected Music music;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    protected AudioSource audioSource;

    [SerializeField]
    protected AudioClip audioDamage;
    [SerializeField]
    protected AudioClip audioDeath;
    [SerializeField]
    protected AudioClip audioExplosion;
    [SerializeField]
    protected AudioClip audioPickUp;



    // Use this for initialization
    protected virtual void Start () {
        deathSound = DeathSound.Instance;
        music = Music.Instance;
        objectPooler = ObjectPooler.Instance;
        talkSystem = TalkSystem.Instance;
        rb = GetComponent<Rigidbody>();
        healthBarValue = healthBar.transform.GetChild(1).GetComponent<Image>();
        healthValue = maxHealthValue;

        RefreshHealth(0f);
        if (usesMana)
        {
            manaValue = maxManaValue;
            manaBar.SetActive(true);
            manaBarValue = manaBar.transform.GetChild(1).GetComponent<Image>();
            RefreshMana(0);
            StartCoroutine(ManaRegen());
        }
        //partyNumber = 0;
        audioSource = GetComponents<AudioSource>()[0];
    }
	
	// Update is called once per frame
	protected void Update () {
        if (!MenuController.isPaused && (tag != "Player" || (tag == "Player" && partyNumber == 0)))
        {
            Move();
            Rotate();
            Attack();
            Guard();
            if(Input.GetButtonUp("Fire3") && tag == "Player")
            {
                PartyManager.SwapPartyMember();
            }
        }
        else if(tag == "Player" && partyNumber > 0)
        {
            //follow the number blabla
            if((transform.position - PartyManager.members[partyNumber - 1].transform.position).magnitude > followDistance)
            {
                MoveFollow();
                animator.SetFloat("Velocity", 1f);
            }
            else
            {
                animator.SetFloat("Velocity", 0f);
            }
            //hay alguna manera mejor de hacer esto???
                
            RotateFollow(PartyManager.members[partyNumber - 1].transform);
        }
    }

    protected virtual void Move()
    {
        Movement.MoveTopDown(transform, movementSpeed);
    }

    protected virtual void Rotate()
    {

    }

    protected virtual void Attack()
    {

    }

    protected virtual void Guard()
    {

    }

    protected virtual void MoveFollow()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }

    protected virtual void RotateFollow(Transform playerTransform)
    {
        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spike") {
            RefreshHealth(-collision.gameObject.GetComponent<Spike>().spike);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ManaPickup" && usesMana) {
            RefreshMana(other.GetComponent<PickupValue>().value);
            Destroy(other.transform.parent.gameObject);
            audioSource.PlayOneShot(audioPickUp);
        }
        else if (other.tag == "HealthPickup") {
            RefreshHealth(other.GetComponent<PickupValue>().value);
            Destroy(other.transform.parent.gameObject);
            audioSource.PlayOneShot(audioPickUp);
        }
        else if (other.tag == "Skeley") {
            Vector3 knockbak = transform.position - other.transform.position;
            knockbak = new Vector3(knockbak.x, 0f, knockbak.z).normalized;
            rb.AddForce(knockbak * 4 + Vector3.up * 1, ForceMode.Impulse);
            RefreshHealth(-other.transform.GetComponentInParent<Character3D>().attackValue);
        }
        else if (other.tag == "Ball")
        {
            Vector3 knockbak = transform.position - other.transform.position;
            knockbak = new Vector3(knockbak.x, 0f, knockbak.z).normalized;
            rb.AddForce(knockbak * 3 + Vector3.up * 2, ForceMode.Impulse);
            RefreshHealth(-other.GetComponent<CannonBall>().AttackValue);
            audioSource.PlayOneShot(audioDamage);
        }
        else if (other.tag == "NPC") {
            other.gameObject.SetActive(false);
            talkSystem.ShowDialog(int.Parse(other.name.Split('_')[2]) - 1);
        }
        else if (other.tag == "Music") {
            other.gameObject.SetActive(false);
            switch (other.name) {
                case "Music_1": music.PlayMusic(1); break;
                case "Music_2": music.PlayMusic(2); break;
                case "Music_3": music.PlayMusic(3); break;
                case "Music_4": deathSound.PlayMega(transform.position); break;
            }
        }
        /*else if (other.tag == "Damage") {
            RefreshHealth(-100f);
        }*/
    }

    public void RefreshHealth(float healthChange)
    {
        healthValue = healthValue + healthChange < 0 ? 0 :
            healthValue + healthChange > maxHealthValue ? maxHealthValue :
            healthValue + healthChange;

        healthBarValue.fillAmount = healthValue / maxHealthValue;
        if(healthChange < 0)
        {
            audioSource.PlayOneShot(audioDamage);
        }
        if (healthValue <= 0)
        {
            deathSound.PlaySound(transform.position, audioDeath);
            deathSound.PlaySound(transform.position, audioExplosion);
            PartyManager.DeletePartyMember(gameObject);
            if(PartyManager.members.Length == 0)
            {
                deathSound.PlayFailure(transform.position);
            }
            objectPooler.GetObjectFromPool("EnemyExplosion", transform.position, transform.rotation, null);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// regresa true si se hay mana suficiente para substraer el mana del mana actual,
    /// regresa false si no hay suficiente mana
    /// </summary>
    /// <param name="manaChange">cantidad de mana en la que va a cambiar; (-) para quitar, (+) para agregar</param>
    /// <returns></returns>
    protected virtual bool RefreshMana(int manaChange)
    {
        if (manaValue + manaChange < 0)
            return false;
        manaValue = manaValue + manaChange > maxManaValue ? maxManaValue
            : manaValue + manaChange;
        //refrescar la barra de mana
        manaBarValue.fillAmount = (float)manaValue / maxManaValue;

        return true;
    }

    IEnumerator ManaRegen()
    {
        while (true)
        {
            RefreshMana(5);
            yield return new WaitForSeconds(1f);
        }
    }
}
