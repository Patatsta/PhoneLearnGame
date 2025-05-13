using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PinVerfication : MonoBehaviour
{
    [SerializeField] private TMP_InputField _createPinField;
    [SerializeField] private TMP_InputField _enterPinField;
    [SerializeField] private TMP_Text _debugText;

    private int _pinNumber;
    private int _enteredPin;

    private void Start()
    {
        _debugText.text = "";
    }



    public void CreatePin()
    {
        if (_createPinField.text.Length < 4)
        {
            _debugText.text = "Error - I need at least 4 digits";
            _createPinField.text = "";
        }
        else
        {
            _pinNumber = int.Parse(_createPinField.text);
            _createPinField.gameObject.SetActive(false);
            _enterPinField.gameObject.SetActive(true);
            _debugText.text = "";
        }
    }

    public void EnterPin()
    {
        _enteredPin = int.Parse(_enterPinField.text);

        if (_enteredPin == _pinNumber)
        {
            _debugText.text = "Correct Pin Entered";
        }
        else
        {
            _debugText.text = "Try Again";
            _enterPinField.text = "";
        }
    }
}
