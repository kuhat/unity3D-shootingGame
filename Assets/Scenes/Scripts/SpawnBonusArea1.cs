using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class SpawnBonusArea1 : MonoBehaviour
{
    public static SpawnBonusArea1 instance;
    
    public GameObject[] bonusItmes;

    public int bonusCount;

    public int currentCount;

    private float reSpawnTime = 60;

    public Transform parent;

    public Transform ground;

    private void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        for (int i = 0; i < bonusCount; i++)
        {
            GameObject objClone = Instantiate(bonusItmes[Random.Range(0, bonusItmes.Length)]);
            objClone.transform.parent = parent;
            objClone.transform.position =
                ground.transform.TransformPoint(new Vector3(Random.Range(-0.5f, 0.5f), 1, Random.Range(-0.5f, 0.5f)));
        }
        currentCount = bonusCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            reSpawnTime -= Time.deltaTime;
            respawn();     
        }
    }

    private void respawn()
    {
        // if 60s have passed and bonus item decreases, respawn them
        if (currentCount < bonusCount && reSpawnTime <= 0)
        {
            // Debug.Log("current Count: " + currentCount);
            int respawnNum = bonusCount - currentCount;
            // Debug.Log("respawn number: " + respawnNum);
            reSpawnTime = 60;
            for (int i = 0; i < respawnNum; i++)
            {
                GameObject objClone = Instantiate(bonusItmes[Random.Range(0, bonusItmes.Length)]);
                objClone.transform.parent = parent;
                objClone.transform.position =
                    ground.transform.TransformPoint(
                        new Vector3(Random.Range(-0.5f, 0.5f), 1, Random.Range(-0.5f, 0.5f)));
            }
            currentCount = bonusCount;
        }
    }
}