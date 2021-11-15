using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;
using Random = System.Random;



public class PlayManager : MonoBehaviour
{
    private List<Dictionary<string, object>> local_questions;
    private string answer_position;
    private bool repeat = false;
    private bool didPlay = false;
    private int index;
    
    public GameObject batter;
    public GameObject firstRunner;
    public GameObject secondRunner;
    public GameObject thirdRunner;
    public GameObject outs;
    public GameObject scenario;
    public GameObject correctPlay;
    public GameObject wrongPlay;

    public GameObject firstBaseman;
    public GameObject secondBaseman;
    public GameObject thirdBaseman;
    public GameObject shortStop;
    public GameObject pitcher;
    public GameObject catcher;
    


    // Start is called before the first frame update
    void Start()
    {
        utils.question_perspective = "";
        clearScene();
        setBaseman(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(!didPlay && hit.collider.tag == "Baseman")
                {
                    utils.timer_active = false;
                    didPlay = true;
                    if (hit.collider.name == answer_position)
                    {
                        correctPlay.SetActive(true);
                        repeat = false;
                        utils.updateScore(utils.player_timer);
                    }
                    else
                    {
                        wrongPlay.SetActive(true);
                        setCorrectAnswer();
                        repeat = true;
                    }

                   
                }
            }
        }
    }

    public void runToBase()
    {
        utils.timer_active = false;
        didPlay = true;
        if (answer_position == utils.question_perspective)
        {
            correctPlay.SetActive(true);
            repeat = false;
            utils.updateScore(utils.player_timer);
        }
        else
        {
            wrongPlay.SetActive(true);
            setCorrectAnswer();
            repeat = true;
        }
    }

    public void Play()
    {
        setBaseman(true);
        string perspective = GameObject.Find("PositionLabel").GetComponent<Text>().text;
        utils.question_perspective = perspective;
        setupRandomQuestion();
        GameObject.Find("Position_Overlay").SetActive(false);
        if (perspective == "1B")
        {
            GameObject.Find("RunToBase").GetComponentInChildren<Text>().text = "Run To 1B";
        }

        else if (perspective == "3B")
        {
            GameObject.Find("RunToBase").GetComponentInChildren<Text>().text = "Run To 3B";
        }
        else
        {
            GameObject.Find("RunToBase").SetActive(false);
        }
        
    }
    

    public void left()
    {
        GameObject camera = GameObject.Find("MainCamera");
        camera.transform.Rotate(0,-5, 0);
    }

    public void right()
    {
        GameObject camera = GameObject.Find("MainCamera");
        camera.transform.Rotate(0,5, 0);
    }

    public void Quit()
    {
        utils.player_timer = 0;
        utils.question_perspective = "";
        utils.pitch_is_complete = false;
        utils.ball_at_dest = false;
        BallManager.game_time = 0;
        SceneManager.LoadScene("MainMenu");
        
    }

    public async void PlayAgain() {
        clearScene();
        didPlay = false;
        utils.pitch_is_complete = false;
        utils.timer_active = true;
        if (repeat)
        {
            /*
             * If play again is hit and the user got it wrong
             *     - We need to push the score to the leaderboard
             *     - reset everything. 
             */
            await Firebase_Helper.addScoreToLeaderBoard();
            utils.resetScore();
            setupRandomQuestion();
        }
        else
        {
            repeat = false;
            setupRandomQuestion();
        }
    }

    /*
     * setupQuestion() is meant to be a helper method to 
     * (1) generate a random question from our local copy of the questions
     * (2) find and replace the Unity GameObject(s) text components
     * (3) dynamically update the variable "answer_position" that is called
     * (4) dynamically update the runners.
     * in update() which is where the hit detection occurs.
     */
    public void setupQuestion(Dictionary<string, object> question)
    {
        setupQuestionText(question);
        setupOuts(question);
        answer_position = question["answer_base"].ToString();
        setupRunners(question);
    }

    private void setupRunners(Dictionary<string, object> question)
    {
        if (question["runner_on_first"].ToString() == "True")
        {
            firstRunner.SetActive(true);
            Animator animator = firstRunner.GetComponent<Animator>();
            animator.Rebind();
            animator.Update(0f);
            animator.enabled = false;
            firstRunner.transform.position = new Vector3(11, 0, 19);
            firstRunner.transform.rotation = Quaternion.Euler(0,230,0);
        }

        if (question["runner_on_second"].ToString() == "True")
        {
            secondRunner.SetActive(true);
            Animator animator = secondRunner.GetComponent<Animator>();
            animator.Rebind();
            animator.Update(0f);
            animator.enabled = false;
            secondRunner.transform.position = new Vector3(-4, 0, -4);
            secondRunner.transform.rotation = Quaternion.Euler(0,130,0);
        }

        if (question["runner_on_third"].ToString() == "True")
        {
            thirdRunner.SetActive(true);
            Animator animator = thirdRunner.GetComponent<Animator>();
            animator.Rebind();
            animator.Update(0f);
            animator.enabled = false;
            thirdRunner.transform.position = new Vector3(18, 0, -17);
            thirdRunner.transform.rotation = Quaternion.Euler(0,45,0);
        }
    }

    private void setCorrectAnswer()
    {
        GameObject.Find("correctAnswerText").GetComponent<Text>().text = String.Format("The correct answer was {0}", answer_position);
    }

    private void setupOuts(Dictionary<string, object> question)
    {
        Text txt = outs.GetComponent<Text>();
        String temp = String.Format("Outs: {0}", question["outs"]);
        txt.text = temp;
    }

    private void setupQuestionText(Dictionary<string, object> question)
    {
        Text txt = scenario.GetComponent<Text>();
        txt.text = question["question_text"].ToString();
    }

    private async void setupSameQuestion()
    {
        if (local_questions == null)
        {
            local_questions = await Firebase_Helper.updateQuestions();
            Debug.Log("Calling Database");
        }
        Dictionary<string, object> question = local_questions[index];

        setupQuestion(question);
    }


    private async void setupRandomQuestion()
    {
        if (local_questions == null)
        {
            // query database and load save them to "local_questions"
            local_questions = await Firebase_Helper.updateQuestions();
            Debug.Log("Calling Database");
        }
        // if we are here, we can imply that "local_questions" has some questions for us
        
        /*
         * We need some way of generating a random number, within rage of local_questions.Count
         * This will simulate shuffling the questions and randomly picking a question. 
         */
        Random random = new Random();
        
        index = random.Next(0, local_questions.Count);
        
        /*
         * Then we call setupQuestion to populate the GameObjects with the
         * corresponding values.
         *
         * random.Next(0, local_questions.Count)
         * produces some number between
         * [0... local_questions.Count)
         */
        Dictionary<string, object> question = local_questions[index];
        
        setupQuestion(question);
    }

    public void clearScene()
    {
        utils.ball_at_dest = false;
        correctPlay.SetActive(false);
        wrongPlay.SetActive(false);
        firstRunner.SetActive(false);
        secondRunner.SetActive(false);
        thirdRunner.SetActive(false);
        utils.resetFirstBaseman();
        utils.resetSecondsBaseman();
        utils.resetThirdBaseman();
        utils.resetShortStop();
        utils.resetPitcher();
        utils.resetBall();
        utils.resetCamera();
        utils.resetTimer();
        utils.resetCatcher();
        utils.resetBatter();
    }
    
    public void setBaseman(bool answer)
    {
        if (answer)
        {
            firstBaseman.SetActive(true);
            secondBaseman.SetActive(true);
            thirdBaseman.SetActive(true);
            shortStop.SetActive(true);
            pitcher.SetActive(true);
            catcher.SetActive(true);
            batter.SetActive(true);
        }
        else
        {
            firstBaseman.SetActive(false);
            secondBaseman.SetActive(false);
            thirdBaseman.SetActive(false);
            shortStop.SetActive(false);
            pitcher.SetActive(false);
            catcher.SetActive(false);
            batter.SetActive(false);
        }
    }
}
