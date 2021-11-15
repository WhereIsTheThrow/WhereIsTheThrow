using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    void Start()
    { 
        updateLeaderBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void updateLeaderBoard()
    {
        List<Dictionary<string, object>> scores = await Firebase_Helper.getScoreFromLeaderboard();
        for (int i = 0; i < scores.Count; i++)
        {
            Dictionary<string, object> entry = scores[i];
            GameObject email = GameObject.Find(String.Format("email{0}", i));
            GameObject score = GameObject.Find(String.Format("score{0}", i));

            Text email_text = email.GetComponent<Text>();
            Text score_text = score.GetComponent<Text>();
            
            email_text.text = entry["email"].ToString();
            score_text.text = entry["score"].ToString();
        }
    }

    public void exitPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
