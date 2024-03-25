using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieController : MonoBehaviour
{
    public static ZombieController instance;

    private Rigidbody _rigidbody;

    private GameObject player;

    private NavMeshAgent agent;

    public float sightRange, attackRange;

    public bool baseInSightRange, playerInAttackRange;

    public LayerMask playerMask;

    public LayerMask baseMask;

    public GameObject Zombie;

    public int health;

    private Animator Animator;

    private bool baseInRange = false;

    private AudioSource zombieAudio;

    public AudioClip zombieShow;

    public GameObject dieEffect;

    private GameObject dieEffectInstance;

    // Time interval that zombie can attack
    private float attackCd;

    // Time interval that zombie would be attracted by a base
    private float attractCd;

    private void Awake()
    {
        attractCd = 8;
        attackCd = 0.8f;
        instance = this;
        zombieAudio = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                                 RigidbodyConstraints.FreezeRotationZ;
        zombieAudio.PlayOneShot(zombieShow);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.pause |
            GameManager.instance.currentGameState == GameState.losingBigger |
            GameManager.instance.currentGameState == GameState.losingBigger)
        {
            agent.enabled = false;
        }
        else if (GameManager.instance.currentGameState == GameState.inGame)
        {
            agent.enabled = true;
            // Check if the base is in range and if player is in the attack range
            baseInSightRange = Physics.CheckSphere(transform.position, sightRange, baseMask);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
            if (attractCd >= 0)
            {
                attractCd -= Time.deltaTime;
            }

            if (baseInSightRange)
            {
                baseInRange = true;
                // Debug.Log("base In range");
                // Only if the cd is zero can the zombie be attracted by the base
                if (attractCd <= 0)
                {
                    RaycastHit[] bases = Physics.SphereCastAll(transform.position, sightRange, Vector3.forward);
                    GameObject baseObj = null;
                    for (int i = 0; i < bases.Length; i++)
                    {
                        if (bases[i].collider.gameObject.layer == LayerMask.NameToLayer("Base"))
                        {
                            // Debug.Log("Attricted by: -" + bases[i].collider.gameObject.tag);
                            baseObj = bases[i].collider.gameObject;
                        }
                    }

                    AttractedByBase(baseObj);
                }
                else
                {
                    ChasePlayer();
                }
            }
            else if (playerInAttackRange)
            {
                AttackPlayer();
            }
            else
            {
                ChasePlayer();
            }
        }
    }

    private void AttackPlayer()
    {
        attackCd -= Time.deltaTime;
        Animator.SetBool("attack", true);
        if (attackCd <= 0)
        {
            // Debug.Log("attack!!!");
            PlayerCharacter.instance.decreaseHealth();
            player.gameObject.GetComponent<Animator>().SetBool("underAttack", true);
            attackCd = 0.8f;
        }
    }

    private void ChasePlayer()
    {
        // Debug.Log("Chase Player");
        Animator.SetBool("run", true);
        Animator.SetBool("attack", false);
        agent.SetDestination(player.transform.position);
        transform.LookAt(player.transform);
    }

    private void AttractedByBase(GameObject baseObj)
    {
        // Debug.Log("Coming to base");
        // Debug.Log(baseObj.tag);
        Animator.SetBool("attack", false);
        agent.SetDestination(baseObj.transform.position);
        transform.LookAt(baseObj.transform);
    }

    public void decreaseHealth()
    {
        health -= 1;
        // Debug.Log("Health decrease");
        // Debug.Log("die, current health: " + health);
        if (health <= 0)
        {
            if (!dieEffectInstance)
            {
                dieEffectInstance = Instantiate(dieEffect, transform);
                Destroy(gameObject, 1.5f);
                Animator.SetBool("die", true);
                PlayerCharacter.instance.decreaseEnemyText();
                PlayerCharacter.instance.increaseScoreBy2();
            }
        }
    }

    public void decreaseHealthPlus()
    {
        health -= 3;
        if (health <= 0)
        {
            if (!dieEffectInstance)
            {
                dieEffectInstance = Instantiate(dieEffect, transform);
                Destroy(gameObject, 1.5f);
                Animator.SetBool("die", true);
                PlayerCharacter.instance.decreaseEnemyText();
                PlayerCharacter.instance.increaseScoreBy2();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Base") && other.gameObject.tag != "InitialBase")
        {
            if (attractCd <= 0)
            {
                attractCd = 10;
                Animator.SetBool("attack", true);
                Animator.SetBool("run", false);
                Instantiate(Zombie, transform.position, transform.rotation);
                PlayerCharacter.instance.increaseEnemyText();
                other.GetComponentInParent<SpawnBasesArea>().currentBases--;
                Destroy(other.gameObject, 1f);
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Base") && other.gameObject.tag == "InitialBase")
        {
            if (attractCd <= 0)
            {
                attractCd = 10;
                Animator.SetBool("attack", true);
                Animator.SetBool("run", false);
                PlayerCharacter.instance.increaseEnemyText();
                Instantiate(Zombie, transform.position, transform.rotation);
            }
        }
        else if (other.gameObject.tag == "Player")
        {
            if (attackCd <= 0)
            {
                PlayerCharacter.instance.decreaseHealth();
                player.gameObject.GetComponent<Animator>().SetBool("underAttack", true);
                attackCd = 0.8f;
            }
        }
    }
}