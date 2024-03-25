using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public static SceneMgr instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    // Pass data through two scenes
    public string fileName = null;

    private void WriteSceneData(string data)
    {
        if (fileName != null)
        {
            Debug.Log("The filename cannot be null, the data of the last scene did not be read");   
        }

        fileName = data;
        Debug.Log(fileName);
    }

    public string ReadSceneData()
    {
        Debug.Log(fileName);
        string temp = fileName;
        fileName = null;
        return temp;
    }

    public void ToNewScene(string sceneName, string param)
    {
        this.WriteSceneData(param);
        SceneManager.LoadScene(sceneName);
    }
}
