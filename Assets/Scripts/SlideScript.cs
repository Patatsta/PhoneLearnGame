using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlideScript : MonoBehaviour
{
    [SerializeField] private Toggle[] _toggle;
    [SerializeField] private Sprite[] _sprite;

    [SerializeField] private Image _picture;
    [SerializeField] private TMP_Text text;


    public void Toggle0()
    {
        if (_toggle[0].isOn)
        {
            _picture.sprite = _sprite[0];
            text.text = "Home Button";
        }
        if (_toggle[1].isOn)
        {
            _picture.sprite = _sprite[1];
            text.text = "Sound On";
        }
        if (_toggle[2].isOn)
        {
            _picture.sprite = _sprite[2];
            text.text = "Sound Off";
        }
    }
}
