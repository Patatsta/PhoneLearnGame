using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G1BotTrigger : MonoBehaviour
{
    public static event Action _botHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
           _botHit?.Invoke();
        }
    }
}
