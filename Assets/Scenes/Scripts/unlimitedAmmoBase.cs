using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class unlimitedAmmoBase : MonoBehaviour
{
    public static unlimitedAmmoBase instance;

    private float cd;

    private Color originalColor;

    private void Awake()
    {
        cd = 0;
        instance = this;
        originalColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (cd > 0)
        {
            cd -= Time.deltaTime;   
        }
        else
        {
            GetComponent<Renderer>().material.color = originalColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cd <= 0)
        {
            if (other.gameObject.tag == "Player")
            {
                GetComponent<Renderer>().material.color = new Color(225, 225, 1);
                cd = 10;
            }     
        }
    }
}
