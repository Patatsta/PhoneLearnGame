using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public static event Action addPoints;
    private bool _isIn = false;
    private float _time = 0;    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            _isIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            _isIn = false;
            _time = 0;
        }
    }

    private void Update()
    {
        if (_isIn)
        {
            _time += Time.deltaTime;
            if(_time > 2f)
            {
                _time = 0;
                addPoints?.Invoke();
            }
        }
    }
}
