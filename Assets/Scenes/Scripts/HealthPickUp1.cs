using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp1 : MonoBehaviour
{
    private AudioSource PickUpAudio;
    
    public AudioClip PickUpClip;

    public GameObject Disappear;
    
    // Start is called before the first frame update
    void Start()
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
            PlayerCharacter.instance.Heal();
            
            Instantiate(Disappear, transform.position, transform.rotation);
            Destroy(gameObject, 1.0f);
            PlayerCharacter.instance.increaseScoreBy5();
            PlayerCharacter.instance.increaseScoreBy5();
        }
    }
}