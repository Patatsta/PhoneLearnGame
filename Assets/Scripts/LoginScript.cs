using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField[] _InputFields;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private GameObject _login;
    [SerializeField] private GameObject _create;
    [SerializeField] private TMP_Text _debugText;
    [SerializeField] private string _username;
    [SerializeField] private string _password;

    private void Start()
    {
        _login.SetActive(true);
        _create.SetActive(false);
        _username = null;
        _password = null;
        _debugText.text = "";
    }

    public void LoginAccount()
    {
        if (_InputFields[2].text == _username && _InputFields[3].text == _password)
        {
            if(_username == null || _password == null)
            {
                _debugText.text = "There is no account. Please create one";
                _InputFields[2].text = "";
                _InputFields[3].text = "";
                return;
            }
            else
            {

            }
            _debugText.text = "You successfully logged in";
        }

        if (_username == null || _password == null)
        {
            _debugText.text = "There is no account. Please create one";
            _InputFields[2].text = "";
            _InputFields[3].text = "";
            return;
        }

        if (_InputFields[2].text != _username || _InputFields[3].text != _password)
        {
            _debugText.text = "Username or Password is Incorrect. Try again";
            _InputFields[2].text = "";
            _InputFields[3].text = "";
        }
    }
    public void CreateAccount()
    {
        if (_InputFields[0].text.Length > 3 && _InputFields[1].text.Length > 3)
        {
            _username = _InputFields[0].text;
            _password = _InputFields[1].text;
            _debugText.text = "Account successfully created";
            OpenLoginAccountMenu();
        }
        else
        {
            _debugText.text = "Your username and password must be at least 4 digits or more";
            _InputFields[0].text = "";
            _InputFields[1].text = "";

        }
    }
    public void OpenCreateAccountMenu()
    {
        _login.SetActive(false);
        _create.SetActive(true);
    }

    public void OpenLoginAccountMenu()
    {

        _login.SetActive(true);
        _create.SetActive(false);
    }
}
