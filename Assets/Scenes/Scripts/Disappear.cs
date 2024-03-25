using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    // Update is called once per frame
    private void Awake()
    {
        if (gameObject.tag == "UnHealthyEffect")
        {
            Destroy(gameObject, 0.5f);
        }
        Destroy(gameObject, 2f);
    }
}
