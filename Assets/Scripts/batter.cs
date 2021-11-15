using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batter : MonoBehaviour
{
    private Vector3 run_to = new Vector3(25.0f, 0.0f, 11.5f);
    void Start()
    {
        
    }

    void Update()
    {
        if (utils.question_perspective == "" | !utils.pitch_is_complete)
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            return;
        }

        if (utils.pitch_is_complete & (Vector3.Distance(transform.position, run_to) > 0.001f))
        {
            float step =  utils.speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 315, 0);
            if (BallManager.game_time >= 4.0f)
            {
                transform.position = Vector3.MoveTowards(transform.position, run_to, step);
            }
        }

        if (Vector3.Distance(transform.position, run_to) < 0.001f)
        {
            GameObject.Find(name).GetComponent<Animator>().enabled = false;
        }

        
    }
}
