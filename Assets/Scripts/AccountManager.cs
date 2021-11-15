using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
    public GameObject email;
    public GameObject highscore;
    void Start()
    {
        setText();
    }

    void Update()
    {
        
    }

    public async void setText()
    {
        Text email_text = email.GetComponent<Text>();
        Text highscore_text = highscore.GetComponent<Text>();
        Dictionary<string, object> info = await Firebase_Helper.getHighScore();
        email_text.text = String.Format("Email: {0}", info["email"]);
        highscore_text.text = String.Format("Highscore: {0}", info["score"]);
    }

    public void backButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
