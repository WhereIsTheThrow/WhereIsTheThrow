using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shortstop : MonoBehaviour
{
    private Vector3 target;
    private Animator _animator;
    void Start()
    {
        
        target = new Vector3(-6.0f, 0.0f, 0.5f);
        _animator = GameObject.Find(name).GetComponent<Animator>();
        
    }
    void Update()
    {
        if (utils.question_perspective == "1B" | (utils.question_perspective == "3B") | 
            (utils.question_perspective == "SS") |  (utils.question_perspective == "") | 
            (Vector3.Distance(transform.position, target) < 0.001f) | !utils.pitch_is_complete)
        {
            _animator.enabled = false;
            return;
        }
        float step =  utils.speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        _animator.enabled = true;
    }
}
