using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private float _force = 10f;
    public void TurnRight()
    {
        transform.Rotate(0, 15, 0);
    }

    public void TurnLeft() 
    {
        transform.Rotate(0, -15, 0);

    }

    public void Shoot()
    {
        GameObject ball = Instantiate(_ballPrefab, _shotPoint);
        ball.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * _force, ForceMode.Impulse);
    }
}
