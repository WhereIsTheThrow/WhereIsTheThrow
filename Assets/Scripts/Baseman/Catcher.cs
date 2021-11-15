using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GameObject.Find(name).GetComponent<Animator>();
    }

    void Update()
    {
        if (utils.question_perspective == "" | !utils.pitch_is_complete)
        {
            _animator.enabled = false;
            return;
        }
        _animator.enabled = true;
    }
}
