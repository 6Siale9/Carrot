using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;
    private Vector3 _lastPos;
    private Vector3 _dir;
    private int _time = 0;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_time >= 20)
        {
            CalculateLastPositionOffset();
            _time = 0;
        }
        else
        {
            _time += 1;
        }
        if (Mathf.Abs(_dir.magnitude) > 0)
        {
            if (Mathf.Abs(_dir.x) < Mathf.Abs(_dir.y))
            {
                if (_dir.y > 0)
                {
                    _animator.SetBool("Up", true);
                    _animator.SetBool("Down", false);
                    _animator.SetBool("Side", false);
                }
                else
                {
                    _animator.SetBool("Up", false);
                    _animator.SetBool("Down", true);
                    _animator.SetBool("Side", false);
                }
            }
            else
            {
                if (_dir.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    _animator.SetBool("Up", false);
                    _animator.SetBool("Down", false);
                    _animator.SetBool("Side", true);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    _animator.SetBool("Up", false);
                    _animator.SetBool("Down", false);
                    _animator.SetBool("Side", true);
                }
            }
        }
        else
        {
            _animator.SetBool("Up", false);
            _animator.SetBool("Down", false);
            _animator.SetBool("Side", false);
        }
    }

    private void CalculateLastPositionOffset()
    {
        _dir = transform.position - _lastPos;
        _lastPos =transform.position;
    }
}
