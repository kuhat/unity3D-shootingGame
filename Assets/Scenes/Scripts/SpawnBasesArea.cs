using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnBasesArea : MonoBehaviour
{
    public static SpawnBasesArea instance;
    
    public GameObject[] bases;

    public int baseCount;

    public int currentBases;

    private float respawnTime = 120;

    private Transform parent;

    private void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        parent = transform;
        Vector3 pos = transform.position;
        currentBases = baseCount;
        for (int i = 0; i < baseCount; i++)
        {
            float x = Random.Range(pos.x - gameObject.GetComponent<MeshRenderer>().bounds.size.x / 2,
                pos.x + gameObject.GetComponent<MeshRenderer>().bounds.size.x / 2);
            float z = Random.Range(pos.z - gameObject.GetComponent<MeshRenderer>().bounds.size.z / 2,
                pos.z + gameObject.GetComponent<MeshRenderer>().bounds.size.z / 2);
            GameObject objClone = Instantiate(bases[Random.Range(0, bases.Length)]);
            objClone.transform.parent = parent;
            objClone.transform.position = new Vector3(x, 10, z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            respawnTime -= Time.deltaTime;
            respawn();      
        }
    }

    private void respawn()
    {
        // if 60s have passed and bonus item decreases, respawn them
        if (currentBases < baseCount && respawnTime <= 0)
        {
            parent = transform;
            Vector3 pos = transform.position;

            int respawnNum = baseCount - currentBases;
            respawnTime = 120;
            for (int i = 0; i < respawnNum; i++)
            {
                float x = Random.Range(pos.x - gameObject.GetComponent<MeshRenderer>().bounds.size.x / 2,
                    pos.x + gameObject.GetComponent<MeshRenderer>().bounds.size.x / 2);
                float z = Random.Range(pos.z - gameObject.GetComponent<MeshRenderer>().bounds.size.z / 2,
                    pos.z + gameObject.GetComponent<MeshRenderer>().bounds.size.z / 2);
                GameObject objClone = Instantiate(bases[Random.Range(0, bases.Length)]);  
                objClone.transform.parent = parent;
                objClone.transform.position = new Vector3(x, 10, z);
            }
            currentBases = baseCount;
        }
    }
}