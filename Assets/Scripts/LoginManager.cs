using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Auth;

public class LoginManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
       
        if (Firebase_Helper.isUserSignedIn())
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public async void loginPressed()
    {
        InputField email = GameObject.Find("email_input").GetComponent<InputField>();
        InputField password = GameObject.Find("password_input").GetComponent<InputField>();
        
        bool created = await Firebase_Helper.signInExistingUser(email.text, password.text);
        if (created)
        {
            /*
             * If the user is verified, then we need to load the home scene 
             */
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            /*
             * If the user is not verified, then we need to let them know that
             * verification has failed.
             */
            Debug.Log("Not a valid login");
            
        }
    }

    public void signUpPressed()
    {
        /*
         * We if this button is pressed then we need to call the
         * scene manager and load the sign up screen.
         */
        SceneManager.LoadScene("SignUpScene");
    }
    
    
    

   
}
