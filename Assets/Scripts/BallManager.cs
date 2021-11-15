using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private Vector3 home = new Vector3(37.5f, 3f, .5f);
    private Vector3 hit_to;
    public static double game_time = 0;
    void Start()
    {
        game_time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (utils.question_perspective == "")
        {
            return;
        }
        if (Vector3.Distance(transform.position, hit_to) < 0.001f)
        {
            utils.ball_at_dest = true;
            if (utils.timer_active)
            {
                utils.updatePlayerTimer();
            }
            return;
        }
        hit_to = GameObject.Find(utils.question_perspective).transform.position;
        float step =  utils.ball_speed * Time.deltaTime;
        game_time += Time.deltaTime;
        if (game_time >= 2.0f & game_time <=3.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, home, step);
            return;
        }

        hit_to.y = 3;
        if (game_time > 3.5f)
        {
            utils.pitch_is_complete = true;
            transform.position = Vector3.MoveTowards(transform.position, hit_to, step);
        }
        
        if (Vector3.Distance(transform.position, hit_to) < .01f)
        {
            GameObject camera = GameObject.Find("MainCamera");
            camera.transform.position = GameObject.Find(utils.question_perspective).transform.position;
            camera.transform.rotation = GameObject.Find(utils.question_perspective).transform.rotation;
            camera.transform.Translate(-3, 7, 0);
            
        }

        
    }
}
