using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Toggle[] _toggle;

    public void Toggle0True()
    {
        if (_toggle[0].isOn)
        {
            Debug.Log("Game is set to easy");
        }
        if (_toggle[1].isOn)
        {
            Debug.Log("Game is set to medium");
        }
        if (_toggle[2].isOn)
        {
            Debug.Log("Game is set to hard");
        }
    }
}
