using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Game3 : GameAbstract
{
    //[Header("UI Elements")]
    public TMP_Text number1Text;
    public TMP_Text operatorText;
    public TMP_Text number2Text;
    public TMP_Text equalsText;
    public TMP_Text resultText; 

    public List<G3Drag> answerButtons; 

    private int correctSecondNumber; 



    [SerializeField] private float _maxTimer = 0;
    [SerializeField] private GameObject _gameCanvas;

    public override void Enter()
    {
        gameManager.SetCurGameCanvas(_gameCanvas, this);
        _score = 0;
        _scoreText.text = _score.ToString();
        _timer = _maxTimer;
        GenerateNewTask();
    }

    public override void Execute()
    {
        TimeCalc();
    }
    public override void Exit()
    {
    }

    public void GenerateNewTask()
    {
        int a = Random.Range(1, 11);
        bool isPlus = Random.value > 0.5f;

       
        int b = Random.Range(1, 11);
        correctSecondNumber = b;

       
        int result = isPlus ? a + b : a - b;


        number1Text.text = a.ToString();
        operatorText.text = isPlus ? "+" : "-";
        number2Text.text = "?";  
        equalsText.text = "=";
        resultText.text = result.ToString();

       
        List<int> answers = new List<int>();
        answers.Add(correctSecondNumber);

        while (answers.Count < 4)
        {
            int wrong = correctSecondNumber + Random.Range(-3, 4);
            if (wrong != correctSecondNumber && wrong >= 0 && !answers.Contains(wrong))
                answers.Add(wrong);
        }

    
        for (int i = 0; i < answers.Count; i++)
        {
            int tmp = answers[i];
            int randIndex = Random.Range(i, answers.Count);
            answers[i] = answers[randIndex];
            answers[randIndex] = tmp;
        }

   
        for (int i = 0; i < answerButtons.Count; i++)
        {
            TMP_Text answerText = answerButtons[i].GetComponentInChildren<TMP_Text>();
            answerText.text = answers[i].ToString();

            answerButtons[i].SaveStartPosition();
        }

      
        var drop = FindObjectOfType<G3Drop>();
        if (drop != null)
            drop.correctNumber = correctSecondNumber;
    }

    public void ActivateDrag(bool isActiv)
    {
        foreach (G3Drag answer in answerButtons)
        {
            print(isActiv);
            answer.SetActiv(isActiv);
        }
    }

    public void AddScore()
    {
        _score += 10;
        _scoreText.text = _score.ToString();
    }
}
