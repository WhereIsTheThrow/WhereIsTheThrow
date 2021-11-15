using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SignUpManager : MonoBehaviour
{
    bool validEmailAdress = false;
    bool validPass = false;
    bool validVerify = false;
    public Text errorText;
    public InputField email;
    public InputField password;
    public InputField pass_verify;
    public InputField username;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void validPassword(string input)
    {
        var hasNumber = new Regex(@"[0-9]+");
        var hasUpper = new Regex(@"[A-Z]+");
        var hasLower = new Regex(@"[a-z]+");
        var hasMin8chars = new Regex(@".{8,}");

        errorText.text = "";
        validPass = false;

        if (!(hasNumber.IsMatch(input) && hasLower.IsMatch(input) && hasUpper.IsMatch(input) && hasMin8chars.IsMatch(input)))
        {
            errorText.text = "Error: The password must include an uppercase, a number and be a minimum of 8 characters";
        }
        else
            validPass = true;

    }

    public void verifyPass(string input) {
        validVerify = false;
        errorText.text = "";

        if (password.text != input)
        {
            errorText.text = "Error: The passwords do not match";
        }
        else
            validVerify = true;
    }

    public void validEmail(string input)
    {
        validEmailAdress = false;
        errorText.text = "";

        var hasEmail = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        if (!hasEmail.IsMatch(input))
        {
            errorText.text = "Error: Please input a valid email";
        }
        else
            validEmailAdress = true;
    }


        public async void createNewUser()
    {
        if (validEmailAdress && validPass && validVerify)
        {
            bool created = await Firebase_Helper.createNewUser(email.text, password.text, username.text);
            if (created)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                Debug.Log("Failed to create new user");
            }
        }
        else {
            if (email.text == "" || password.text == "" || pass_verify.text == "") {
                errorText.text = "Error: Please fill out all fields";
            }
            Debug.Log("Validation error");
        }
        
    }

    public void cancelPressed()
    {
        SceneManager.LoadScene("LoginScene");
    }
} 
