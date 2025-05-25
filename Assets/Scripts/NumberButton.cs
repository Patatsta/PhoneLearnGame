using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberButton : MonoBehaviour
{
    [SerializeField] private int _number;
   
    private int _posIndex;

    public static event Action<int, NumberButton> OnButtonClick;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }

    public void ButtonPress()
    {
        OnButtonClick?.Invoke(_posIndex, this);
        Destroy(gameObject);
    }

    public int GetNumber()
    {
        return _number;
    }

    public void SetNumber(int number, int posIndex)
    {
        _number = number;
        _posIndex = posIndex;
        _text.text = _number.ToString();
    }
}

