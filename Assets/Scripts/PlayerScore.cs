﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    // Start is called before the first frame update
    public int playerScore = 0;
    public int highScore = 0;
    public Text playerScoreText;
    public Text highScoreText;

    private const string DATA_DELIMITER = "#!#";
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        playerScoreText.text = playerScore.ToString("Score: " + playerScore);
        highScoreText.text = playerScore.ToString("High Score: " + highScore);

        if(playerScore > highScore)
        {
            highScore = playerScore;
            Save();
        }
    }

    private void Save()
    {
        string[] contents = new string[]
        {
            "" + highScore
        };

        string saveString = string.Join(DATA_DELIMITER, contents);
        File.WriteAllText(Application.dataPath + "/save.txt", saveString);
    }

    private void Load()
    {
        string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
        Debug.Log("Loaded. HighScore is " + saveString);
        highScore = int.Parse(saveString);
    }
}
