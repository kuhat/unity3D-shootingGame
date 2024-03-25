using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace SlimUI.ModernMenu
{
    public class OptionsMenuNew : MonoBehaviour
    {
        // toggle buttons

        [Header("VIDEO SETTINGS")] public GameObject fullscreentext;
        public GameObject shadowofftextLINE;
        public GameObject shadowlowtextLINE;
        public GameObject shadowhightextLINE;
        public GameObject texturelowtextLINE;
        public GameObject texturemedtextLINE;
        public GameObject texturehightextLINE;

        // sliders
        public GameObject musicSlider;
        private float sliderValue = 0.0f;
        private float sliderValueXSensitivity = 0.0f;
        private float sliderValueYSensitivity = 0.0f;
        private float sliderValueSmoothing = 0.0f;

        public List<ResItem> resolutions = new List<ResItem>();

        private int selectedResolution;
        public TMP_Text resolutionLabel;

        public void Start()
        {
            // check slider values
            musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");

            // check full screen
            if (Screen.fullScreen == true)
            {
                fullscreentext.GetComponent<TMP_Text>().text = "on";
            }
            else if (Screen.fullScreen == false)
            {
                fullscreentext.GetComponent<TMP_Text>().text = "off";
            }
            
            // check and set new resolution
            bool foundRes = false;
            for (int i = 0; i < resolutions.Count; i++)
            {
                if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
                {
                    foundRes = true;
                    selectedResolution = i;
                    updateResLabel();
                }
            }

            if (!foundRes)
            {
                ResItem newRes = new ResItem();
                newRes.horizontal = Screen.width;
                newRes.vertical = Screen.height;
                resolutions.Add(newRes);
                selectedResolution = resolutions.Count - 1;
                updateResLabel();
            }

            // check shadow distance/enabled
            if (PlayerPrefs.GetInt("Shadows") == 0)
            {
                QualitySettings.shadowCascades = 0;
                QualitySettings.shadowDistance = 0;
                shadowofftextLINE.gameObject.SetActive(true);
                shadowlowtextLINE.gameObject.SetActive(false);
                shadowhightextLINE.gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Shadows") == 1)
            {
                QualitySettings.shadowCascades = 2;
                QualitySettings.shadowDistance = 75;
                shadowofftextLINE.gameObject.SetActive(false);
                shadowlowtextLINE.gameObject.SetActive(true);
                shadowhightextLINE.gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Shadows") == 2)
            {
                QualitySettings.shadowCascades = 4;
                QualitySettings.shadowDistance = 500;
                shadowofftextLINE.gameObject.SetActive(false);
                shadowlowtextLINE.gameObject.SetActive(false);
                shadowhightextLINE.gameObject.SetActive(true);
            }

            // check texture quality
            if (PlayerPrefs.GetInt("Textures") == 0)
            {
                QualitySettings.masterTextureLimit = 2;
                texturelowtextLINE.gameObject.SetActive(true);
                texturemedtextLINE.gameObject.SetActive(false);
                texturehightextLINE.gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Textures") == 1)
            {
                QualitySettings.masterTextureLimit = 1;
                texturelowtextLINE.gameObject.SetActive(false);
                texturemedtextLINE.gameObject.SetActive(true);
                texturehightextLINE.gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Textures") == 2)
            {
                QualitySettings.masterTextureLimit = 0;
                texturelowtextLINE.gameObject.SetActive(false);
                texturemedtextLINE.gameObject.SetActive(false);
                texturehightextLINE.gameObject.SetActive(true);
            }
        }

        public void Update()
        {
            sliderValue = musicSlider.GetComponent<Slider>().value;
        }

        public void FullScreen()
        {
            Screen.fullScreen = !Screen.fullScreen;

            if (Screen.fullScreen == true)
            {
                fullscreentext.GetComponent<TMP_Text>().text = "off";
            }
            else if (Screen.fullScreen == false)
            {
                fullscreentext.GetComponent<TMP_Text>().text = "on";
            }
        }

        public void ResLeft()
        {
            selectedResolution--;
            if (selectedResolution < 0)
            {
                selectedResolution = 0;
            }

            updateResLabel();
            Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical,
                Screen.fullScreen);
        }

        public void ResRight()
        {
            selectedResolution++;
            if (selectedResolution > resolutions.Count - 1)
            {
                selectedResolution = resolutions.Count - 1;
            }
            updateResLabel();
            Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical,
                Screen.fullScreen);
        }

        public void updateResLabel()
        {
            resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + "x" +
                                   resolutions[selectedResolution].vertical.ToString();
        }

        public void MusicSlider()
        {
            //PlayerPrefs.SetFloat("MusicVolume", sliderValue);
            PlayerPrefs.SetFloat("MusicVolume", musicSlider.GetComponent<Slider>().value);
        }

        public void ShadowsOff()
        {
            PlayerPrefs.SetInt("Shadows", 0);
            QualitySettings.shadowCascades = 0;
            QualitySettings.shadowDistance = 0;
            shadowofftextLINE.gameObject.SetActive(true);
            shadowlowtextLINE.gameObject.SetActive(false);
            shadowhightextLINE.gameObject.SetActive(false);
        }

        public void ShadowsLow()
        {
            PlayerPrefs.SetInt("Shadows", 1);
            QualitySettings.shadowCascades = 2;
            QualitySettings.shadowDistance = 75;
            shadowofftextLINE.gameObject.SetActive(false);
            shadowlowtextLINE.gameObject.SetActive(true);
            shadowhightextLINE.gameObject.SetActive(false);
        }

        public void ShadowsHigh()
        {
            PlayerPrefs.SetInt("Shadows", 2);
            QualitySettings.shadowCascades = 4;
            QualitySettings.shadowDistance = 500;
            shadowofftextLINE.gameObject.SetActive(false);
            shadowlowtextLINE.gameObject.SetActive(false);
            shadowhightextLINE.gameObject.SetActive(true);
        }

        public void TexturesLow()
        {
            PlayerPrefs.SetInt("Textures", 0);
            QualitySettings.masterTextureLimit = 2;
            texturelowtextLINE.gameObject.SetActive(true);
            texturemedtextLINE.gameObject.SetActive(false);
            texturehightextLINE.gameObject.SetActive(false);
        }

        public void TexturesMed()
        {
            PlayerPrefs.SetInt("Textures", 1);
            QualitySettings.masterTextureLimit = 1;
            texturelowtextLINE.gameObject.SetActive(false);
            texturemedtextLINE.gameObject.SetActive(true);
            texturehightextLINE.gameObject.SetActive(false);
        }

        public void TexturesHigh()
        {
            PlayerPrefs.SetInt("Textures", 2);
            QualitySettings.masterTextureLimit = 0;
            texturelowtextLINE.gameObject.SetActive(false);
            texturemedtextLINE.gameObject.SetActive(false);
            texturehightextLINE.gameObject.SetActive(true);
        }
    }

    [System.Serializable]
    public class ResItem
    {
        public int horizontal, vertical;
    }
}