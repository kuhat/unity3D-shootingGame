using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    public static CharacterMovement instance;

    public float MoveSpeed = 7.0f;

    private Rigidbody _Rigidbody;

    private Animator Animator;

    public Text SpText;

    public Text HpText;

    public int SP;

    private bool running = false;

    private float timerConsume = 1.0f;

    private bool speedUpState;

    private float speedUpCd;

    public GameObject speedUpEffect;

    private GameObject effect;

    private float dangerousAreaCd = 2;

    public Vector3 CurrentInput { get; private set; }


    private void Start()
    {
        instance = this;
        string fileName = MainSceneInit.instance.loadedFileName;
        if (!string.IsNullOrEmpty(fileName))
        {
            SP = MainSceneInit.instance.save.SP;
        }
        else
        {
            SP = 100;
        }
    }

    public void Awake()
    {
        speedUpCd = 0;
        speedUpState = false;
        SpText.text = SP + "";
        Animator = GetComponent<Animator>();
        _Rigidbody = GetComponent<Rigidbody>();
        _Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                                 RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            checkSpeedUp();
            checkRuning();
            checkDangerousArea();
            _Rigidbody.MovePosition(_Rigidbody.position + MoveSpeed * CurrentInput * Time.fixedDeltaTime * 0.35f);
        }
    }

    private void checkSpeedUp()
    {
        // If cool time is not 0, enter into speed up mode
        if (speedUpCd > 0)
        {
            speedUpCd -= Time.deltaTime;
        }
        else
        {
            speedUpState = false;
            if (effect != null)
            {
                Destroy(effect);
            }
        }

        if (speedUpState)
        {
            MoveSpeed = 14f;
            Animator.speed = 1.2f;
        }
    }

    private void checkRuning()
    {
        // If the player presses left shift, speed up, decrease SP. if not, increase SP
        if (Input.GetKey("left shift"))
        {
            running = true;
            DecreaseSP();
            if (speedUpState)
            {
                if (SP > 0)
                {
                    MoveSpeed = 21f;
                    Animator.speed = 2.2f;
                }
                else
                {
                    MoveSpeed = 14f;
                    Animator.speed = 1.5f;
                }
            }
            else
            {
                if (SP > 0)
                {
                    MoveSpeed = 14.0f;
                    Animator.speed = 1.5f;
                }
                else
                {
                    MoveSpeed = 7.0f;
                    Animator.speed = 1;
                }
              
            }
        }
        else
        {
            increaseSp();
            if (speedUpState)
            {
                Animator.speed = 1.5f;
                MoveSpeed = 14f;
                running = false;
            }
            else
            {
                running = false;
                Animator.speed = 1;
                MoveSpeed = 7.0f;
            }
        }
    }

    private void checkDangerousArea()
    {
        if (dangerousAreaCd >= 0)
        {
            dangerousAreaCd -= Time.deltaTime;
        }
    }

    private void increaseSp()
    {
        if (SP < 100)
        {
            timerConsume -= Time.deltaTime;
            if (timerConsume < 0)
            {
                if (SP == 90)
                {
                    SP += 10;
                    SpText.text = SP + "";
                    timerConsume = 1;
                }
                else
                {
                    SP += 20;
                    SpText.text = SP + "";
                    timerConsume = 1;
                }
            }
        }
    }

    public void SetMovementInput(Vector3 input)
    {
        CurrentInput = Vector3.ClampMagnitude(input, 1);
    }

    private void DecreaseSP()
    {
        // If the player presses left shift, move into the running state
        if (running)
        {
            timerConsume -= Time.deltaTime;
            if (timerConsume < 0)
            {
                if (SP <= 0)
                {
                    SP = 0;
                    SpText.text = "No SP!";
                    timerConsume = 1;
                    running = false;
                }
                else
                {
                    SP -= 10;
                    SpText.text = SP + "";
                    timerConsume = 1;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpeedUpBase")
        {
            speedUpCd = 10;
            speedUpState = true;
            if (!effect)
            {
                effect = Instantiate(speedUpEffect, transform);
                PlayerCharacter.instance.increaseScoreBy5();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "DangerousArea")
        {
            HpText.text = "Dangerous Area! " + PlayerCharacter.instance.Hp;
            Debug.Log("Stay DangerousArea");
            if (dangerousAreaCd <= 0)
            {
                PlayerCharacter.instance.Hp -= 1;
                dangerousAreaCd = 2;
                HpText.text = PlayerCharacter.instance.Hp + "";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DangerousArea")
        {
            HpText.text = PlayerCharacter.instance.Hp + "";
        }
    }
}