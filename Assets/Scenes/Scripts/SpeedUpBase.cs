using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpBase : MonoBehaviour
{

    private float cd;
    
    private Color originalColor;
    
    private void Awake()
    {
        cd = 0;
        originalColor = GetComponent<Renderer>().material.color;
    }

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
                GetComponent<Renderer>().material.color = new Color(25, 1, 255);
                cd = 10;
            }     
        }
    }
}
