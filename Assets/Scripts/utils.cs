using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class utils
{
    public static string question_perspective = "";
    public static bool pitch_is_complete = false;
    public static bool ball_at_dest = false;
    public static bool timer_active = true;
    public static float speed = 4.0f;
    public static float ball_speed = 15.0f;
 
    public static float player_timer = 0.0f;
    public static int score = 0;
    
    

    public static void resetBall()
    {
        GameObject.Find("Ball").transform.position = new Vector3(15, 3, 0);
    }
    public static void resetFirstBaseman()
    {
        GameObject.Find("1B").transform.position = new Vector3(7, 0, 23);
        Animator animator = GameObject.Find("1B").GetComponent<Animator>();
        animator.Rebind();
        animator.Update(0f);
    }

    public static void resetSecondsBaseman()
    {
        GameObject.Find("2B").transform.position = new Vector3(-6, 0, 14);
        Animator animator = GameObject.Find("2B").GetComponent<Animator>();
        animator.Rebind();
        animator.Update(0f);
    }

    public static void resetThirdBaseman()
    {
        GameObject.Find("3B").transform.position = new Vector3(8, 0, -20);
        Animator animator = GameObject.Find("3B").GetComponent<Animator>();
        animator.Rebind();
        animator.Update(0f);
    }

    public static void resetShortStop()
    {
        GameObject.Find("SS").transform.position = new Vector3(-6, 0, -11);
        Animator animator = GameObject.Find("SS").GetComponent<Animator>();
        animator.Rebind();
        animator.Update(0f);
    }

    public static void resetPitcher()
    {
        Animator animator = GameObject.Find("P").GetComponent<Animator>();
        BallManager.game_time = 0;
        animator.Rebind();
        animator.Update(0f);
    }

    public static void resetCamera()
    {
        GameObject camera = GameObject.Find("MainCamera");
        camera.transform.position = new Vector3(43, 10, 0);
        camera.transform.rotation = Quaternion.Euler(15, 270, 0);
    }

    public static void resetTimer()
    {
        player_timer = 0.0f;
        GameObject timer = GameObject.Find("Time");
        Text time = timer.GetComponent<Text>();
        time.text = "Timer: 00:00";
    }

    public static void resetCatcher()
    {
        Animator animator = GameObject.Find("C").GetComponent<Animator>();
        animator.Rebind();
        animator.Update(0f);
    }

    public static void resetBatter()
    {
        GameObject batter = GameObject.Find("batter");
        Animator animator = batter.GetComponent<Animator>();
        animator.Rebind();
        animator.Update(0f);
        animator.ResetTrigger(0);
        animator.enabled = true;
        batter.transform.position = new Vector3(37.0f, 0.0f, -3.0f);
        batter.transform.rotation = Quaternion.Euler(0,270,0);
    }
    

    public static void updatePlayerTimer()
    {
        GameObject timer = GameObject.Find("Time");
        Text time = timer.GetComponent<Text>();
        player_timer += Time.deltaTime;
        int minutes = (int) player_timer / 60;
        time.text = string.Format("Timer: {0}:{1}", minutes, (int)player_timer);
    }
    
    public static void updateScore(float time)
    {
        GameObject s = GameObject.Find("Score");
        Text text = s.GetComponent<Text>();
        if (time >= 0.0f && time < 2.0f)
        {
            score += 13;
        }
        else if (time >= 2.0f && time < 10.0f)
        {
            score += 10;
        }
        else if (time >= 10.0f)
        {
            score += 7;
        }
        else
        {
            return;
        }
        text.text = String.Format("Score: {0}", score);
    }
    
    public static void resetScore()
    {
        GameObject s = GameObject.Find("Score");
        Text text = s.GetComponent<Text>();
        text.text = "Score: 0";
        score = 0;
    }


  
    
}
