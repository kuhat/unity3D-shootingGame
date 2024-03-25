using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 2);
    }
}
