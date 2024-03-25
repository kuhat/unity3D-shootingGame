using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp1 : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource PickUpAudio;
    public AudioClip PickUpClip;
    public AudioClip Check;
    public GameObject Disappear; 
    private void Awake()
    {
        PickUpAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       gameObject.transform.Rotate(Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PickUpAudio.PlayOneShot(PickUpClip);
            PickUpAudio.PlayOneShot(Check);
            GunControl.instance.CollectAmmo();
            
            Instantiate(Disappear, transform.position, transform.rotation);
            Destroy(gameObject, 1.0f);
            PlayerCharacter.instance.increaseScoreBy5();
            PlayerCharacter.instance.increaseScoreBy5();
        }
    }
}
