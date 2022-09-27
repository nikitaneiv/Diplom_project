using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Spikes>())
        {
            gameObject.SetActive(false);
        }
        if (other.gameObject.GetComponent<PlayerController>())
        {
            gameObject.SetActive(false);
        }
    }
}
