using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    menu,
    inGame,
    losingSmaller,
    losingBigger,
    pause,
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.menu;
    public static GameManager instance;
    public Canvas inGameCanvas;
    public GameObject pauseCanvas;
    public Canvas losingCanvasBigger;
    public Text LosingBigScore;
    public Canvas losingCanvasSmaller;
    public Text LosingSmallScore;

    private GameObject main_camera;
    private GameObject pause_camera;
    public static bool isPaused;

    public string loadedFileName;

    private void Awake()
    {
        main_camera = GameObject.FindWithTag("MainCamera");
        pause_camera = GameObject.FindWithTag("MenuCamera");
        main_camera.SetActive(true);
        instance = this;
        isPaused = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetGameState(GameState.inGame);
        currentGameState = GameState.inGame;
        loadedFileName = MainSceneInit.instance.loadedFileName;
        Debug.Log("SSSSSSSSSSSSSSSSS" + loadedFileName);
        // If load from disk, custom the initialization 
        if (!string.IsNullOrEmpty(loadedFileName))
        {
            MainSceneInit.instance.initGame();
        }
        else
        {
            startGame();
        }
    }

    public void startGame()
    {
        // Start to initiate spawn bases and bonuses
        GameObject[] sBaseA = GameObject.FindGameObjectsWithTag("BaseGenPlane");
        for (int i = 0; i < sBaseA.Length; i++)
        {
            sBaseA[i].GetComponent<SpawnBasesArea>().StartGame();
        }

        GameObject[] sBonusA = GameObject.FindGameObjectsWithTag("BonusGenPlane");
        for (int i = 0; i < sBonusA.Length; i++)
        {
            sBonusA[i].GetComponent<SpawnBonusArea1>().StartGame();
        }

        GameObject[] IT = GameObject.FindGameObjectsWithTag("InitialBase");
        for (int i = 0; i < IT.Length; i++)
        {
            IT[i].GetComponent<InitialBase>().StartGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkLosing();
        checkPause();
    }

    private void checkLosing()
    {
        if (PlayerCharacter.instance.Hp <= 0 && PlayerCharacter.instance.score < 50)
        {
            isPaused = true;
            SetGameState(GameState.losingSmaller);
        }
        else if (PlayerCharacter.instance.Hp <= 0 && PlayerCharacter.instance.score > 50)
        {
            isPaused = true;
            SetGameState(GameState.losingBigger);
        }
    }

    private void checkPause()
    {
        if (currentGameState == GameState.inGame)
        {
            if (!isPaused && Input.GetKeyDown(KeyCode.Space))
            {
                isPaused = true;
                SetGameState(GameState.pause);
            }
        }
    }

    public void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            inGameCanvas.enabled = false;
            pauseCanvas.SetActive(false);
            losingCanvasBigger.enabled = false;
            losingCanvasSmaller.enabled = false;
        }
        else if (newGameState == GameState.inGame)
        {
            inGameCanvas.enabled = true;
            pauseCanvas.SetActive(false);
            losingCanvasBigger.enabled = false;
            losingCanvasSmaller.enabled = false;
            currentGameState = GameState.inGame;
        }
        else if (newGameState == GameState.pause)
        {
            Debug.Log(newGameState);
            inGameCanvas.enabled = false;
            pauseCanvas.SetActive(true);
            main_camera.SetActive(false);
            pause_camera.SetActive(true);
            losingCanvasBigger.enabled = false;
            losingCanvasSmaller.enabled = false;
            currentGameState = GameState.pause;
        }
        else if (newGameState == GameState.losingBigger)
        {
            isPaused = true;
            inGameCanvas.enabled = false;
            pauseCanvas.SetActive(false);
            losingCanvasBigger.enabled = true;
            losingCanvasSmaller.enabled = false;
            currentGameState = GameState.losingBigger;
            LosingBigScore.text = "You get "+ PlayerCharacter.instance.score + " this time.";
            // Debug.Log("losing bigger:");
        }
        else if (newGameState == GameState.losingSmaller)
        {
            isPaused = true;
            inGameCanvas.enabled = false;
            pauseCanvas.SetActive(false);
            losingCanvasBigger.enabled = false;
            losingCanvasSmaller.enabled = true;
            currentGameState = GameState.losingSmaller;
            LosingSmallScore.text = "You get "+ PlayerCharacter.instance.score + " this time.";
        }
    }

    public void resume()
    {
        if (isPaused)
        {
            print("resume");
            isPaused = false;
            SetGameState(GameState.inGame);
            main_camera.SetActive(true);
            pause_camera.SetActive(false);
        }
    }

    public void mainMenu()
    {
        // Save game

        // load scene 
        SceneManager.LoadSceneAsync("MenuScene");
    }

    public void newGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void returnMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}