using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UserInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private TMP_Text _textOutput;

    public void InputFieldName()
    {
        PlayerPrefs.SetString("name", _name.text);
    }

    public void InputFieldPassword()
    {
        PlayerPrefs.SetString ("password", _password.text);
    }

    public void OutputText()
    {
        _textOutput.text = "Your Username is " + PlayerPrefs.GetString("name") + " and your Password is " + PlayerPrefs.GetString("password");
    }
}

