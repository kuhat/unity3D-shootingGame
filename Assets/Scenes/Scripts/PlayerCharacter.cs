using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    public static PlayerCharacter instance;
    private CharacterMovement _CharacterMovement;
    private Animator Animator;
    private AudioSource PlayerAudio;
    public AudioClip WalkAudioClip;
    private Rigidbody Rigidbody;
    private float walkTimer = 0;
    private float walkAnimCd = 0.4f;
    public Text HpText;
    public int Hp;
    public GameObject unhealthyEffect;
    public Text scoreText;
    public int score;
    private float scoreCd = 10f;
    private float scoreDoubleCd = 60f;
    public Text enemyNumText;
    public int enemyNum;

    private void Start()
    {
        instance = this;
        string fileName = MainSceneInit.instance.loadedFileName;
        if (!string.IsNullOrEmpty(fileName))
        {
            Hp = MainSceneInit.instance.save.HP;
            Debug.Log("Previous score: " + MainSceneInit.instance.save.score);
            score = MainSceneInit.instance.save.score;
            enemyNum = MainSceneInit.instance.save.existingEnemy;
            HpText.text = Hp.ToString();
            scoreText.text = score.ToString();
            enemyNumText.text = enemyNum.ToString();
        }
        else
        {
            score = 0;
            Hp = 100;
            enemyNum = 0;
            HpText.text = Hp.ToString();
            scoreText.text = score.ToString();
            enemyNumText.text = enemyNum.ToString();
        }
    }

    private void Awake()
    {
        _CharacterMovement = GetComponent<CharacterMovement>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
        PlayerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            UpdateMovementInput();
            FollowMouse();
            increaseScore();
        }
    }

    private void increaseScore()
    {
        scoreCd -= Time.deltaTime;
        scoreDoubleCd -= Time.deltaTime;
        if (scoreCd <= 0)
        {
            score += 1;
            scoreCd = 10f;
            scoreText.text = score + "";
        }

        if (scoreDoubleCd <= 0)
        {
            score *= 2;
            scoreDoubleCd = 60;
            scoreText.text = score + "";
        }
    }

    public void increaseEnemyText()
    {
        enemyNum++;
        enemyNumText.text = enemyNum + "";
    }

    public void decreaseEnemyText()
    {
        enemyNum--;
        enemyNumText.text = enemyNum + "";
    }

    private void UpdateMovementInput()
    {
        walkTimer += Time.deltaTime;
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        _CharacterMovement.SetMovementInput(dir);
        if (dir != Vector3.zero)
        {
            // If user press the button
            transform.rotation = Quaternion.LookRotation(dir);
            // Walk animation
            Animator.SetBool("move", true);
            Animator.SetBool("underAttack", false);
            if (Input.GetKey("left shift"))
            {
                walkAnimCd = 0.3f;
            }
            else
            {
                walkAnimCd = 0.4f;
            }

            if (walkTimer > walkAnimCd)
            {
                // Play Walk Audio
                PlayerAudio.PlayOneShot(WalkAudioClip);
                walkTimer = 0;
            }
        }
        else
        {
            Animator.SetBool("move", false);
            // stand still animation
        }
    }

    private void FollowMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Debug.Log(ray);    
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 200, LayerMask.GetMask("Environment")))
        {
            Vector3 target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    public void Heal()
    {
        if (Hp < 100)
        {
            if (90 <= Hp && Hp <= 99)
            {
                Hp += 100 - Hp;
            }
            else
            {
                Hp += 10;
            }
        }

        HpText.text = Hp + "";
    }

    public void decreaseHealth()
    {
        // Debug.Log("Decrease Health");
        Instantiate(unhealthyEffect, transform);
        if (Hp >= 5 && Hp <= 100)
        {
            Hp -= 20;
            HpText.text = Hp + "";
        }

        if (Hp <= 0)
        {
            if (score <= 50)
            {
                GameManager.instance.SetGameState(GameState.losingSmaller);
            } else if (score > 50)
            {
                GameManager.instance.SetGameState(GameState.losingBigger);
                Debug.Log("bigger than 50");
            }
            Debug.Log("die");
        }
    }

    public void increaseScoreBy5()
    {
        score += 5;
        scoreText.text = score + "";
    }

    public void increaseScoreBy2()
    {
        score += 2;
        scoreText.text = score + "";
    }
}