using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;
using UnityStandardAssets.Cameras;

public class MainSceneInit : MonoBehaviour
{
    public static MainSceneInit instance;
    public string loadedFileName;
    public Save save;
    public GameObject Zombie;
    public GameObject AmmoPickUp;
    public GameObject HealthPickUp;
    public GameObject AmmoBase;
    public GameObject SpeedUpBase;
    public GameObject AmmoPlusBase;
    public GameObject normalBase;
    
    private void Awake()
    {
        instance = this;
        loadedFileName = SceneMgr.instance.ReadSceneData();
        Debug.Log(loadedFileName);
        // Initialize Game objects
        if (!string.IsNullOrEmpty(loadedFileName))
        {
            StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/" + loadedFileName);
            String JsonString = sr.ReadToEnd();
            Debug.Log(JsonString);
            sr.Close();
            save = JsonUtility.FromJson<Save>(JsonString);
        }
        else
        {
            Debug.Log("filename Null");
        }
    }

    public void initGame()
    {
        Debug.Log("score: " + save.score);
        Debug.Log("HP: " + save.HP);
        Debug.Log("SP: " + save.SP);
        Debug.Log("bullet Num: " + save.bulletNum);
        Debug.Log("existing Zombie num: " + save.existingEnemy);
        GameObject.FindWithTag("Player").transform.position = save.playerPos;
        Debug.Log("Instantiate zombie");
        Debug.Log(save.zombiePos.Count);
        // No keys!!!!! solve this
        for (int i = 0; i < save.zombiePos.Count; i++)
        {
            Debug.Log("Zombie Position: " + save.zombiePos[i]);
            Instantiate(Zombie, save.zombiePos[i], Quaternion.identity);
        }
        
        for (int i = 0; i < save.AmmoPos.Count; i++)
        {
            Debug.Log("ammo pos: " + save.AmmoPos[i]);
            Instantiate(AmmoPickUp, save.AmmoPos[i], Quaternion.identity);
        }

        for (int i = 0; i < save.HealthPackPos.Count; i++)
        {
            Debug.Log("Health pick up pos: " + save.HealthPackPos[i]);
            Instantiate(HealthPickUp, save.HealthPackPos[i], Quaternion.identity);
        }

        for (int i = 0; i < save.AmmoBasePos.Count; i++)
        {
            Debug.Log("Ammo base Pos: " + save.AmmoBasePos[i]);
            Instantiate(AmmoBase, save.AmmoBasePos[i], Quaternion.identity);
        }

        for (int i = 0; i < save.NormalBasePos.Count; i++)
        {
            Debug.Log("Normal base pos: " + save.NormalBasePos.Count);
            Instantiate(normalBase, save.NormalBasePos[i], Quaternion.identity);
        }

        for (int i = 0; i < save.AmmoPlusBasePos.Count; i++)
        {
            Debug.Log("ammo plus base pos: " + save.AmmoPlusBasePos[i]);
            Instantiate(AmmoPlusBase, save.AmmoPlusBasePos[i], Quaternion.identity);
        }

        for (int i = 0; i < save.SpeedUpBasePos.Count; i++)
        {
            Instantiate(SpeedUpBase, save.SpeedUpBasePos[i], Quaternion.identity);
        }
        
    }
}
