using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

namespace SlimUI.ModernMenu
{
    public class MainMenuNew : MonoBehaviour
    {
        Animator CameraObject;

        [Header("Loaded Scene")]
        public string sceneName = "MainScene";

        public string MenuSceneName = "MenuScene";

        public enum Theme
        {
            custom1,
            custom2,
            custom3
        };

        [Header("Theme Settings")] public Theme theme;
        int themeIndex;
        public FlexibleUIData themeController;

        [Header("Panels")] 
        public GameObject mainCanvas;

        [Tooltip("The UI Panel that holds the CONTROLS window tab")]
        public GameObject PanelControls;

        [Tooltip("The UI Panel that holds the VIDEO window tab")]
        public GameObject PanelVideo;


        [Header("SFX")] [Tooltip("The GameObject holding the Audio Source component for the HOVER SOUND")]
        public AudioSource hoverSound;

        [Tooltip("The GameObject holding the Audio Source component for the AUDIO SLIDER")]
        public AudioSource sliderSound;

        [Tooltip(
            "The GameObject holding the Audio Source component for the SWOOSH SOUND when switching to the Settings Screen")]
        public AudioSource swooshSound;

        // campaign button sub menu
        [Header("Menus")] [Tooltip("The Menu for when the MAIN menu buttons")]
        public GameObject mainMenu;

        [Tooltip("THe first list of buttons")] public GameObject firstMenu;

        [Tooltip("The Menu for when the PLAY button is clicked")]
        public GameObject playMenu;

        [Tooltip("The Menu for when the EXIT button is clicked")]
        public GameObject exitMenu;

        [Tooltip("Optional 4th Menu")] public GameObject extrasMenu;
        public GameObject guideMenu;

        [Header("LOADING SCREEN")] public GameObject loadingMenu;
        public Slider loadBar;
        public TMP_Text finishedLoadingText;

        [Header("Guide Pictures")] public GameObject[] pic;
        private int selectedPic = 0;

        [Header("Save and Load")] public GameObject saveSuccess;

        public Dropdown savedChoose;
        public GameObject dropdownObj;
        public int saveNum;
        public GameObject loadGameOptions;
        private string selectedGame;

        void Start()
        {
            CameraObject = transform.GetComponent<Animator>();

            playMenu.SetActive(false);
            exitMenu.SetActive(false);
            if (extrasMenu) extrasMenu.SetActive(false);
            firstMenu.SetActive(true);
            mainMenu.SetActive(true);

            SetThemeColors();
        }

        void SetThemeColors()
        {
            if (theme == Theme.custom1)
            {
                themeController.currentColor = themeController.custom1.graphic1;
                themeController.textColor = themeController.custom1.text1;
                themeIndex = 0;
            }
            else if (theme == Theme.custom2)
            {
                themeController.currentColor = themeController.custom2.graphic2;
                themeController.textColor = themeController.custom2.text2;
                themeIndex = 1;
            }
            else if (theme == Theme.custom3)
            {
                themeController.currentColor = themeController.custom3.graphic3;
                themeController.textColor = themeController.custom3.text3;
                themeIndex = 2;
            }
        }

        public void PlayCampaign()
        {
            exitMenu.SetActive(false);
            if (extrasMenu) extrasMenu.SetActive(false);
            if (guideMenu) guideMenu.SetActive(false);
            playMenu.SetActive(true);
        }

        public void ReturnMenu()
        {
            playMenu.SetActive(false);
            if (extrasMenu) extrasMenu.SetActive(false);
            exitMenu.SetActive(false);
            mainMenu.SetActive(true);
        }

        public void NewGame()
        {
            if (sceneName != "")
            {
                StartCoroutine(LoadAsynchronously(sceneName));
                //SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
        }

        public void DisablePlayCampaign()
        {
            playMenu.SetActive(false);
        }

        public void Position2()
        {
            DisablePlayCampaign();
            CameraObject.SetFloat("Animate", 1);
        }

        public void Position1()
        {
            CameraObject.SetFloat("Animate", 0);
        }

        void DisablePanels()
        {
            PanelControls.SetActive(false);
            PanelVideo.SetActive(false);
        }

        public void GamePanel()
        {
            DisablePanels();
        }

        public void VideoPanel()
        {
            DisablePanels();
            PanelVideo.SetActive(true);
        }

        public void ControlsPanel()
        {
            DisablePanels();
            PanelControls.SetActive(true);
        }

        public void KeyBindingsPanel()
        {
            DisablePanels();
            MovementPanel();
        }

        public void MovementPanel()
        {
            DisablePanels();
        }

        public void CombatPanel()
        {
            DisablePanels();
        }

        public void GeneralPanel()
        {
            DisablePanels();
        }

        public void PlayHover()
        {
            hoverSound.Play();
        }

        public void PlaySFXHover()
        {
            sliderSound.Play();
        }

        public void PlaySwoosh()
        {
            swooshSound.Play();
        }

        // Are You Sure - Quit Panel Pop Up
        public void AreYouSure()
        {
            exitMenu.SetActive(true);
            guideMenu.SetActive(false);
            if (extrasMenu) extrasMenu.SetActive(false);
            DisablePlayCampaign();
        }

        public void GuideMenu()
        {
            playMenu.SetActive(false);
            exitMenu.SetActive(false);
            if (extrasMenu) extrasMenu.SetActive(false);
            guideMenu.SetActive(true);
        }

        public void nextPic()
        {
            pic[selectedPic].SetActive(false);
            selectedPic++;
            if (selectedPic > pic.Length - 1)
            {
                selectedPic = pic.Length - 1;
            }

            pic[selectedPic].SetActive(true);
        }

        public void prePic()
        {
            pic[selectedPic].SetActive(false);
            selectedPic--;
            if (selectedPic < 0)
            {
                selectedPic = 0;
            }

            pic[selectedPic].SetActive(true);
        }

        public void ExtrasMenu()
        {
            playMenu.SetActive(false);
            if (extrasMenu) extrasMenu.SetActive(true);
            if (guideMenu) guideMenu.SetActive(false);
            exitMenu.SetActive(false);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
        }

        // start saved game
        public void LoadByJson()
        {
            if (File.Exists(Application.streamingAssetsPath + "/" + selectedGame + ".txt"))
            {
                Debug.Log(selectedGame + ".txt");
                // Pass file name to MainScene
                SceneMgr.instance.ToNewScene(sceneName, selectedGame + ".txt");
            }
            else
            {
                Debug.Log("No file!!");
            }
        }
        
        // Delete saving file
        public void deleteSaves()
        {
            if (File.Exists(Application.streamingAssetsPath + "/" + selectedGame + ".txt"))
            {
                Debug.Log("Delete: "+selectedGame + ".txt");
                // Pass file name to MainScene
                File.Delete( Application.streamingAssetsPath + "/" + selectedGame + ".txt");
                // Refresh dropdown data
                savedChoose.options.Clear();
                Debug.Log("cleared option data");
                DirectoryInfo direction = new DirectoryInfo(Application.streamingAssetsPath);
                FileInfo[] files = direction.GetFiles("*");
                List<string> fileNames = new List<string>();
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Name.EndsWith(".meta"))
                    {
                        continue;
                    }

                    fileNames.Add(files[i].Name);
                    Debug.Log("Add option data: " + files[i].Name);
                    Dropdown.OptionData data = new Dropdown.OptionData();
                    data.text = files[i].Name.Split(".")[0];
                    savedChoose.options.Add(data);
                }

                Debug.Log("files length: " + files.Length);
                Debug.Log("fileNames length" + fileNames.Count);
                // Check if the player has saved games before
                if (fileNames.Count == 0)
                {
                    Dropdown.OptionData data = new Dropdown.OptionData();
                    data.text = "no saved games";
                    savedChoose.options.Add(data);
                }
                else
                {
                    selectedGame = savedChoose.options[0].text;
                    Debug.Log("Selected Game: " + selectedGame);
                }
            }
            else
            {
                Debug.Log("No file!!");
            }
            dropdownObj.SetActive(false);
            dropdownObj.SetActive(true);
        }

        // on dropdown value change
        public void OnloadValueChange()
        {
            var index = savedChoose.value;
            selectedGame = savedChoose.options[index].text;
            Debug.Log(selectedGame);
        }

        public Save saveGame()
        {
            Save save = new Save();
            save.score = PlayerCharacter.instance.score;
            save.bulletNum = GunControl.instance.bulleCount;
            save.HP = PlayerCharacter.instance.Hp;
            save.SP = CharacterMovement.instance.SP;
            save.existingEnemy = PlayerCharacter.instance.enemyNum;
            save.playerPos = GameObject.FindWithTag("Player").transform.position;

            // Find game objects by tags and add into save objects
            GameObject[] zombies = GameObject.FindGameObjectsWithTag("Enemy");
            Debug.Log("Number of Zombies: " + zombies.Length);
            for (int i = 0; i < zombies.Length; i++)
            {
                save.zombiePos.Add(zombies[i].gameObject.transform.position);
            }

            Debug.Log("Zombie list length: " +save.zombiePos.Count);

            GameObject[] Ammos = GameObject.FindGameObjectsWithTag("Ammo");
            for (int i = 0; i < Ammos.Length; i++)
            {
                save.AmmoPos.Add(Ammos[i].transform.position);
            }

            GameObject[] HealthPack = GameObject.FindGameObjectsWithTag("HealthPack");
            for (int i = 0; i < HealthPack.Length; i++)
            {
                save.HealthPackPos.Add(HealthPack[i].transform.position);
            }

            GameObject[] AmmoBase = GameObject.FindGameObjectsWithTag("AmmoBase");
            for (int i = 0; i < AmmoBase.Length; i++)
            {
                save.AmmoBasePos.Add(AmmoBase[i].transform.position);
            }

            GameObject[] SpeedUpBase = GameObject.FindGameObjectsWithTag("SpeedUpBase");
            for (int i = 0; i < SpeedUpBase.Length; i++)
            {
                save.SpeedUpBasePos.Add(SpeedUpBase[i].transform.position);
            }

            GameObject[] AmmoPlusBase = GameObject.FindGameObjectsWithTag("AmmoPlusBase");
            for (int i = 0; i < AmmoPlusBase.Length; i++)
            {
                save.AmmoPlusBasePos.Add(AmmoPlusBase[i].transform.position);
            }

            GameObject[] Base = GameObject.FindGameObjectsWithTag("Base");
            for (int i = 0; i < Base.Length; i++)
            {
                save.NormalBasePos.Add(Base[i].transform.position);
            }

            return save;
        }

        // save game button
        public void SaveByJson()
        {
            Debug.Log("Save game");
            Save save = saveGame();
            String JsonString = JsonUtility.ToJson(save);
            // Debug.Log(JsonString);
            DateTime dateTime = DateTime.Now;
            string strNowTime = string.Format("{0:D}-{1:D}-{2:D}-{3:D}_{4:D}_{5:D}", dateTime.Year, dateTime.Month,
                dateTime.Day,
                dateTime.Hour, dateTime.Minute, dateTime.Second);
            Debug.Log(strNowTime);
            StreamWriter sw = new StreamWriter(Application.streamingAssetsPath + "/" + strNowTime + ".txt");
            Debug.Log(Application.streamingAssetsPath + "/" + strNowTime + ".txt");
            sw.Write(JsonString);
            sw.Close();

            saveSuccess.SetActive(true);
        }

        // Save success
        public void backToMainMenu()
        {
            SceneManager.LoadScene(MenuSceneName);
        }

        public void notBackToMainMenu()
        {
            saveSuccess.SetActive(false);
        }

        IEnumerator LoadAsynchronously(string sceneName)
        {
            // scene name is just the name of the current scene being loaded
            // Debug.Log(sceneName);
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;
            // Debug.Log("Loading canvas loading");
            mainCanvas.SetActive(false);
            loadingMenu.SetActive(true);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                loadBar.value = progress;

                if (operation.progress >= 0.9f)
                {
                    finishedLoadingText.gameObject.SetActive(true);

                    if (Input.anyKeyDown)
                    {
                        operation.allowSceneActivation = true;
                    }
                }

                yield return null;
            }
        }

        // load game button
        public void Onclick_Dpd()
        {
            dropdownObj.SetActive(true);
           
            // Clear all the original options 
            savedChoose.options.Clear();
            // Find all the saving files under StreamingAssets
            List<String> fileNames = new List<string>();
            string path = string.Format("{0}", Application.streamingAssetsPath);
            if (Directory.Exists(path))
            {
                DirectoryInfo direction = new DirectoryInfo(path);
                FileInfo[] files = direction.GetFiles("*");
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Name.EndsWith(".meta") | files[i].Name.EndsWith(".json"))
                    {
                        continue;
                    }

                    // Debug.Log("Name: " + files[i].Name);
                    fileNames.Add(files[i].Name);
                    Dropdown.OptionData data = new Dropdown.OptionData();
                    data.text = files[i].Name.Split(".")[0];
                    savedChoose.options.Add(data);
                }

                // Check if the player has saved games before
                if (fileNames.Count == 0)
                {
                    Dropdown.OptionData data = new Dropdown.OptionData();
                    data.text = "no saved games";
                    savedChoose.options.Add(data);
                }
                else
                {
                    selectedGame = savedChoose.options[0].text;
                    loadGameOptions.SetActive(true);
                    // Debug.Log(selectedGame);
                }
            }
        }
    }
}