using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game2 : GameAbstract
{
    [SerializeField] private Transform _buttonPrefab;

    [SerializeField] private Transform[] _buttonPos;

    private int _buttonIndex;
    
    private List<int> _buttonList = new List<int>();
    private List<NumberButton> _buttons = new List<NumberButton>();

    [SerializeField] private float _maxTimer = 0;
    [SerializeField] private TMP_Text _upDown;

    private bool _isUp;
  

    [SerializeField] private GameObject _gameCanvas;

    public override void Enter()
    {
        gameManager.SetCurGameCanvas(_gameCanvas, this);
        NumberButton.OnButtonClick += NumberSelected;
        _buttonIndex = 1;
        _score = 0;
        _scoreText.text = _score.ToString();
        _timer = _maxTimer;
        for (int i = 1; i < 7; i++)
        {

            CreateButton();
        }

        RandomUpDown();
    }

    public override void Execute()
    {
        TimeCalc();
    }
   

    void NumberSelected(int posIndex, NumberButton selectedButton)
    {
    
        int selectedNumber = selectedButton.GetNumber();
        bool isCorrect = true;

        foreach (NumberButton button in _buttons)
        {
            int num = button.GetNumber();
            if (_isUp)
            {
                if (num > selectedNumber)
                {
                    
                    isCorrect = false;
                    break;
                }
            }
            else
            {
                if (num < selectedNumber)
                {
                   
                    isCorrect = false;
                    break;
                }
            }
        }

        if (isCorrect)
        {
            
            _score += 10;
            _scoreText.text = _score.ToString();
        }

        CreateButton();
        _buttonList.Remove(posIndex);
      

        _buttons.Remove(selectedButton);
        RandomUpDown();
    }

    void CreateButton()
    {
      
        List<int> availablePositions = new List<int>();

        for (int i = 0; i < _buttonPos.Length; i++)
        {
            if (!_buttonList.Contains(i))
            {
                availablePositions.Add(i);
            }
        }
        if (availablePositions.Count == 0)
            return;

        int randomIndex = UnityEngine.Random.Range(0, availablePositions.Count);
        int tmp = availablePositions[randomIndex];

        _buttonList.Add(tmp);

        Transform button = Instantiate(_buttonPrefab, _buttonPos[tmp]);
        NumberButton butscript = button.GetComponent<NumberButton>();
        _buttons.Add(butscript);
        butscript.SetNumber(_buttonIndex, tmp);
        _buttonIndex++;
    }

    public override void Exit()
    {
        NumberButton.OnButtonClick -= NumberSelected;
        foreach (NumberButton but in _buttons)
        {
            Destroy(but.gameObject);
        }
        _buttons.Clear();

        _buttonList.Clear();

        _buttonIndex = 0;
    }

    void RandomUpDown()
    {
        int r = UnityEngine.Random.Range(0, 2);
        if(r == 0)
        {
            _upDown.text = "Up";
            _isUp = true;
        }
        else
        {
            _upDown.text = "Down";
            _isUp = false;
        }
    }
}
