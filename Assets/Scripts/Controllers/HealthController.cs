using System;
using UnityEngine;
using UnityEngine.UI;


public class HealthController : MonoBehaviour
    {
        private int Live;
        private int numberOfLive;

        [SerializeField] private Image [] lives;

        public void updateLive()
        {
            for (int i = 0; i < lives.Length; i++)
            {
                if (i < numberOfLive)
                {
                    lives[i].enabled = true;
                }
                else
                {
                    lives[i].enabled = false;
                }
            }
        }
    }