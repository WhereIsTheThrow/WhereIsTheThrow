using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class third_runner : MonoBehaviour
{
    private Vector3 run_to = new Vector3(26f, 0.0f, -8.0f);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (utils.question_perspective == "" | !utils.pitch_is_complete)
        {
            transform.rotation = Quaternion.Euler(0, 45, 0);
            return;
        }
        if (utils.pitch_is_complete & (Vector3.Distance(transform.position, run_to) > 0.001f))
        {
            float step =  utils.speed * Time.deltaTime;
            GameObject.Find(name).GetComponent<Animator>().enabled = true;
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
