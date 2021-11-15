using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBaseman : MonoBehaviour
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
      
        if (utils.question_perspective == "2B" | (Vector3.Distance(transform.position, target) < 0.001f) | 
            utils.question_perspective == "" | !utils.pitch_is_complete)
        {
            _animator.enabled = false;
            return;
        }
        float step =  utils.speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        _animator.enabled = true;
    }
}
