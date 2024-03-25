using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosion;

    private AudioSource bulletAudio;

    public AudioClip hitAudio;
    void Start()
    {
        // Give a velocity
        GetComponent<Rigidbody>().AddForce(transform.forward * 800);
        bulletAudio = GetComponent<AudioSource>();
        Destroy(gameObject, 20);
    }

    private void OnCollisionEnter(Collision other)
    {
        // Destroy itself when touching other obj
        if (other.gameObject.tag == "Enemy" && gameObject.tag == "Bullet")
        {
            // Debug.Log("hit zombie!!");
            other.gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * 100);
            other.collider.GetComponent<ZombieController>().decreaseHealth();
            bulletAudio.PlayOneShot(hitAudio);
        } else if (other.gameObject.tag == "Enemy" && gameObject.tag == "BulletPlus")
        {
            other.collider.GetComponent<ZombieController>().decreaseHealthPlus();
            other.gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * 1000);
            bulletAudio.PlayOneShot(hitAudio);
        }
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
