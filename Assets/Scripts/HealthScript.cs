using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Slider _slider;
    private int _health = 100;

    private void Start()
    {
        _slider.value = _health;
        _text.text = _health.ToString();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _health -= 20;
            _slider.value = _health;
            _text.text = _health.ToString();
        }
    }
}
