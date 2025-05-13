using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_Text text;

    public void InputFieldTyping()
    {
        text.text = "Typing...";
    }

    public void InputField()
    {
        text.text = "You entered" + _inputField.text + "Thank you";
    }
  
}
