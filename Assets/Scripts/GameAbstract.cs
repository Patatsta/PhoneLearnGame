using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class GameAbstract : MonoBehaviour
{
    protected GameManager gameManager;
    protected TMP_Text _timerText;
    protected TMP_Text _scoreText;
    protected float _timer;
    protected int _score;

    public void Setup(GameManager gameManager, TMP_Text _timer, TMP_Text _score)
    {
        this.gameManager = gameManager;
        this._timerText = _timer;
        this._scoreText = _score;
    }

    public void TimeCalc()
    {
        _timer -= Time.deltaTime;
        int totalseconds = Mathf.CeilToInt(_timer);
        int minutes = totalseconds / 60;
        int seconds = totalseconds % 60;
        _timerText.text = $"{minutes}:{seconds:D2}";

        if (_timer <= 0)
        {
            gameManager.EndGame(_score);
        }
    }
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Execute();


}



//public GameAbstract(GameManager gameManager, TMP_Text _timer, TMP_Text _score) 
// {
//     this.gameManager = gameManager;
//     this._timerText = _timer;
//     this._scoreText = _score;

// }