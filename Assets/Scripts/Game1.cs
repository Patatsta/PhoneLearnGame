using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif 
[System.Serializable]
public class Arrangement 
{
    public Vector2 ballPos;
    public Vector2 cupPos;
    public Vector2 plank1;
    public Quaternion plank1rot;
    public Vector2 plank2;
    public Quaternion plank2rot;
}

public class Game1 : GameAbstract
{

    [SerializeField] private float _maxTimer = 0;
 
  

    [SerializeField] private Transform _ball;
    [SerializeField] private Transform _cup;
    [SerializeField] private Transform _plank1, _plank2;



    [SerializeField] private Vector2 _cupArea = new Vector2(5,5);
    [SerializeField] private Vector2 _ballArea = new Vector2(5, 5);
    [SerializeField] private Vector2 _cupAreaPos = new Vector2(5, 5);
    [SerializeField] private Vector2 _ballAreaPos = new Vector2(5, 5);

    private Vector2 _currentBallPos;
    private Ball _ballScript;

    [SerializeField] private Arrangement[] arrangements;

    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private AudioClip _clip;
    public override void Enter()
    {
        SoundManager.Instance.PlayGameMusic();
        gameManager.SetCurGameCanvas(_gameCanvas, this);
        _ballScript = _ball.GetComponent<Ball>();
        G1BotTrigger._botHit += ResetAfterFail;
        Cup.addPoints += AddScore;
        _score = 0;
        _timer = _maxTimer;
        _scoreText.text = _score.ToString();
        SetPositions();
    }

    public override void Execute()
    {

        TimeCalc(); 
    }


    public override void Exit()
    {
        G1BotTrigger._botHit -= ResetAfterFail;
        Cup.addPoints -= AddScore;
        SoundManager.Instance.PlayMenuMusic();
        _ball.transform.position = _currentBallPos;
        _ballScript.ResetBall();
    }

    void AddScore()
    {
        SoundManager.Instance.PlaySFX(_clip);
        _score += 10;
        _scoreText.text = _score.ToString();
        _ballScript.ResetBall();
        SetPositions();
    }

    public void SetPositions()
    {
        int index = Random.Range(0, arrangements.Length);
        _currentBallPos = arrangements[index].ballPos;
        _ball.transform.position = _currentBallPos;
        _cup.transform.position = arrangements[index].cupPos;
        _plank1.position = arrangements[index].plank1;
        _plank2.position = arrangements[index].plank2;
        _plank1.rotation = arrangements[index].plank1rot;
        _plank2.rotation = arrangements[index].plank2rot;

    }

    public void ResetAfterFail()
    {
        _ball.transform.position = _currentBallPos;
        _ballScript.ResetBall();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireCube(_cupAreaPos, new Vector3(_cupArea.x, _cupArea.y, 0));
        Gizmos.DrawWireCube(_ballAreaPos, new Vector3(_ballArea.x, _ballArea.y, 0));
    }

#if UNITY_EDITOR
    [ContextMenu("Save Current Arrangement")]
    private void SaveCurrentArrangement()
    {
        Undo.RecordObject(this, "Save Arrangement");

        int oldLength = arrangements != null ? arrangements.Length : 0;
        System.Array.Resize(ref arrangements, oldLength + 1);

        arrangements[oldLength] = new Arrangement
        {
            ballPos = _ball.position,
            cupPos = _cup.position,
            plank1 = _plank1.position,
            plank1rot = _plank1.rotation,
            plank2 = _plank2.position,
            plank2rot = _plank2.rotation,
        };


        EditorUtility.SetDirty(this);
    }
#endif

}


//_currentBallPos = new Vector3(
//     _ballAreaPos.x + Random.Range(-_ballArea.x / 2f, _ballArea.x / 2f),
//     _ballAreaPos.y + Random.Range(-_ballArea.y / 2f, _ballArea.y / 2f)
// );

//_ball.transform.position = _currentBallPos;
//_ballScript.ResetBall();

//_cup.transform.position = new Vector3(
//    _cupAreaPos.x + Random.Range(-_cupArea.x / 2f, _cupArea.x / 2f),
//    _cupAreaPos.y + Random.Range(-_cupArea.y / 2f, _cupArea.y / 2f)
//);