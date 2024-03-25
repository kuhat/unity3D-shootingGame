using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunControl : MonoBehaviour
{
    public static GunControl instance;

    //Associate the firePoint
    public Transform FirePoint;

    //Associate the FireObj
    public GameObject FireObj;

    // Associate the bulletObj
    public GameObject BulletPre;

    public GameObject BulletPlusPre;

    // Associate the bullet point
    public Transform BulletPoint;

    // Associate Audio clip
    public AudioClip clip;

    // Associate the reload clip
    public AudioClip check;

    // Associate the Bullet text
    public Text BulletText;

    // Number of Bullets
    public int bulleCount;

    // Fire Interval
    private float fireCd = 0.3f;

    // timer
    private float timer = 0;

    // Audio component
    private AudioSource gunPlay;

    private bool unlimitedAmmo;

    private bool ammoPlus;

    public GameObject unlimitedAmmoEffect;

    private GameObject unlimitedAmmoEffectObj;

    public GameObject ammoPlusEffect;

    private GameObject ammoPlusEffectObj;

    private float unlimitedAmmoCd;

    private float ammoPlusCd;

    private GameObject bulletObj;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        gunPlay = GetComponent<AudioSource>();
        string fileName = MainSceneInit.instance.loadedFileName;
        if (!string.IsNullOrEmpty(fileName))
        {
            Debug.Log(MainSceneInit.instance.save.bulletNum);
            bulleCount = MainSceneInit.instance.save.bulletNum;
            BulletText.text = bulleCount + "";
        }
        else
        {
            bulleCount = 20;
        }
    }

    void Awake()
    {
        unlimitedAmmo = false;
        unlimitedAmmoCd = 0;
        ammoPlus = false;
        ammoPlusCd = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            timer += Time.deltaTime;
            checkAmmoPlus();
            fire();
        }
    }

    private void checkAmmoPlus()
    {
        if (ammoPlusCd <= 0)
        {
            ammoPlus = false;
            bulletObj = BulletPre;
            if (ammoPlusEffectObj)
            {
                Destroy(ammoPlusEffectObj);
                BulletText.text = bulleCount + "";
            }
        }
        if (ammoPlus)
        {
            ammoPlusCd -= Time.deltaTime;
            bulletObj = BulletPlusPre;
            fireCd = 0.2f;
        }
        else
        {
            bulletObj = BulletPre;
            fireCd = 0.3f;
        }
    }

    private void fire()
    {
        if (unlimitedAmmo)
        {
            unlimitedAmmoCd -= Time.deltaTime;
            BulletText.text = "∞";
            if (unlimitedAmmoCd <=0)
            {
                unlimitedAmmo = false;
                if (unlimitedAmmoEffectObj)
                {
                    Destroy(unlimitedAmmoEffectObj);
                    BulletText.text = bulleCount + "";
                }
            }
        }
        // If the timer is satisfied with cd, and the mouse left button is pressed, open fire
        if (timer > fireCd && Input.GetMouseButton(0))
        {
            // Reset the timer
            timer = 0;
            // If the player get buff for unlimited Ammo
            if (!unlimitedAmmo)
            {
                // If there are bullets remaining
                if (bulleCount > 0)
                {
                    bulleCount--;
                    // Initialize the fire
                    Instantiate(FireObj, FirePoint.position, FirePoint.rotation);
                    // Initialize the Bullet
                    Instantiate(bulletObj, BulletPoint.position, BulletPoint.rotation);
                    // Decrease the number of bullet
                    // refresh the ui
                    BulletText.text = bulleCount + "";
                    // Play gunplay Audio
                    gunPlay.PlayOneShot(clip);
                }

                // If there are no bullet left change 
                if (bulleCount == 0)
                {
                    // Play audio clip
                    BulletText.text = "No Bullet! Find Supply!";
                    gunPlay.PlayOneShot(check);
                    // 1.5s interval to change
                    // Invoke("reload", 1.5f);
                }
            }
            else
            {
                // If enter the unlimited Ammo state, do not decrease ammo number 
                Instantiate(FireObj, FirePoint.position, FirePoint.rotation);
                // Initialize the Bullet
                Instantiate(bulletObj, BulletPoint.position, BulletPoint.rotation);
                gunPlay.PlayOneShot(clip);
            }
        }
    }

    public void CollectAmmo()
    {
        bulleCount += 20;
        BulletText.text = bulleCount + "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AmmoBase")
        {
            if (unlimitedAmmoCd <= 0)
            {
                // Debug.Log("Unlimited Ammo");
                unlimitedAmmoCd = 10;
                BulletText.text = "∞";
                unlimitedAmmo = true;
                PlayerCharacter.instance.increaseScoreBy5();
                if (!unlimitedAmmoEffectObj)
                {
                    unlimitedAmmoEffectObj = Instantiate(unlimitedAmmoEffect, transform);
                }
            }
        }else if (other.tag == "AmmoPlusBase")
        {
            if (ammoPlusCd <= 0)
            {
                ammoPlusCd = 10;
                BulletText.text = bulleCount + " ++";
                PlayerCharacter.instance.increaseScoreBy5();
                if (!ammoPlusEffectObj)
                {
                    ammoPlusEffectObj = Instantiate(ammoPlusEffect, GameObject.FindWithTag("Player").transform);
                }
                ammoPlus = true;
            }
        }
    }
}