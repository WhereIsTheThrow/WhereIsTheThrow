using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void playButtonPressed()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void leaderboardButtonPressed()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void accountButtonPressed()
    {
        SceneManager.LoadScene("AccountScene");
    }

    public void signoutPressed()
    {
        bool didSignOut = Firebase_Helper.signOutUser();
        if (didSignOut)
        {
            SceneManager.LoadScene("LoginScene");
        }
        else
        {
            Debug.Log("Unable to sign out user, please try again?");
        }
    }
}
