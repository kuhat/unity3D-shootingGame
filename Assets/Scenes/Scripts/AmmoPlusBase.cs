using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPlusBase : MonoBehaviour
{
    private float cd;

    private Color originalColor;

    private void Awake()
    {
        cd = 0;
        originalColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
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
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cd <= 0)
        {
            if (other.gameObject.tag == "Player")
            {
                GetComponent<Renderer>().material.color = new Color(128, 0, 128);
                cd = 10;
            }     
        }
    }
}
