using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialBase : MonoBehaviour
{
    public static InitialBase instance;
    
    private float cd = 30;

    public GameObject motherZombie;

    private void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        Instantiate(motherZombie, transform.position, transform.rotation);
        PlayerCharacter.instance.increaseEnemyText();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            // Generate a zombie every 30s
            cd -= Time.deltaTime;
            if (cd <= 0)
            {
                Instantiate(motherZombie, transform.position, transform.rotation);
                PlayerCharacter.instance.increaseEnemyText();
                cd = 30;
            }     
        }
    }
}
