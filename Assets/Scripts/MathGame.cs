using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MathGame : MonoBehaviour
{
    [SerializeField] TMP_Text[] _numberTexts;
    [SerializeField] TMP_InputField _answerField;
    [SerializeField] TMP_Text _debugText;
    private int _number1;
    private int _number2;
    private int _answer;

    private void Start()
    {
        CreateTask();
    }

    public void CreateTask()
    {
        _answerField.text = "";
        _debugText.text = "Solve";

        _number1 = Random.Range(1, 30);
        _number2 = Random.Range(1, 30);

        _numberTexts[0].text = _number1.ToString();
        _numberTexts[1].text = _number2.ToString();

        _answer = _number1 + _number2;
    }

    public void LogAnswer()
    {
        if(_answer == int.Parse(_answerField.text))
        {
            _debugText.text = "Correct!";
        }
        else
        {
            _debugText.text = "Incorrect";
            _answerField.text = "";
        }
    }
}
