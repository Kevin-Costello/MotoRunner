﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    // Start is called before the first frame update
    public int playerScore = 0;
    public Text speedText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = playerScore.ToString("Score: " + playerScore);
    }
}