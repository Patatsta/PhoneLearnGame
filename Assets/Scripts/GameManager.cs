using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _endScoreText;

    private GameAbstract _currentState;

    [SerializeField] private Game1 _game1;
    [SerializeField] private Game2 _game2;
    [SerializeField] private Game3 _game3;
    [SerializeField] private GameEmpty _gameEmpty;

    [SerializeField] private GameObject[] _Canvases;

    private GameObject _currentGameCanvas;

    private GameAbstract _latestgame;
    [SerializeField] LoginScript _loginScript;

    private enum GameMode
    {
        game1,
        game2,
        game3,
        empty
    }
    [SerializeField] private GameMode _mode;
    private void Start()
    {
        
        _scoreText.text = "0";
        _game1.Setup(this, _scoreText, _timerText);
        _game2.Setup(this, _scoreText, _timerText);
        _game3.Setup(this, _scoreText, _timerText);

        switch (_mode)
        {
            case GameMode.game1:
                _currentState = _game1;
                break;
            case GameMode.game2:
                _currentState = _game2;
                break;
            case GameMode.game3:
                _currentState = _game3;
                break;
            case GameMode.empty:
                _currentState = _gameEmpty;
                break;
             default:
                _currentState = _gameEmpty;
                break;
        }

        _currentState.Enter();

    }
    public void ChangeState(GameAbstract newState)
    {
        _Canvases[8].SetActive(newState != _gameEmpty);
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    private void Update()
    {
        _currentState.Execute();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
      
        _Canvases[4].SetActive(true); //4 is PauseCanvas
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
     
        _Canvases[4].SetActive(false); //4 is PauseCanvas
    }
   
    public void LoginSuccess()
    {
        _Canvases[0].SetActive(false);
        _Canvases[1].SetActive(true);
    }

    public void EnterGame1()
    {
        ChangeState(_game1);
    }
    public void EnterGame2()
    {
        ChangeState(_game2);
    }

    public void EnterGame3()
    {
        ChangeState(_game3);
    }

    public void ExitGame()
    {
        _currentGameCanvas.SetActive(false);
        ChangeState(_gameEmpty);
    }

    public void EndGame(int score)
    {
        _currentGameCanvas.SetActive(false);
        _Canvases[9].SetActive(true);
        _endScoreText.text = score.ToString();

        int tmpHighScore = 0;
        int gameindex = 0;
       
        if (_currentState == _game1)
        {
            gameindex = 1;
        }
        else if (_currentState == _game2)
        {
            gameindex = 2;
        }
        else if (_currentState == _game3)
        {
            gameindex = 3;
        }

        tmpHighScore = _loginScript.GetHighscores(gameindex);
        if (score > tmpHighScore)
        {
            _loginScript.UpdateHighScore(gameindex, score);
         
        }
      
   
        _highScoreText.text = _loginScript.GetHighscores(gameindex).ToString();

        ChangeState(_gameEmpty);
    }

    public void SetCurGameCanvas(GameObject can, GameAbstract gam)
    {
        _currentGameCanvas = can;
        _latestgame = gam;
    }

    public void PlayAgain()
    {
        _currentGameCanvas.SetActive(true);
        ChangeState(_latestgame);
    }
}
//foreach (var game in _Canvases)
//{
//    game.SetActive(false);
//}