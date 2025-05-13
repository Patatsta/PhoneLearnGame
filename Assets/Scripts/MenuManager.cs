using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu1;
    [SerializeField] private GameObject _menu2;
    [SerializeField] private Button _nextButton;

    private void Start()
    {
        _menu1.SetActive(true);
        _menu2.SetActive(false);
    }

    public void ButtonPress()
    {
        _menu1.SetActive(false);
        _menu2.SetActive(true);
        _nextButton.Select();
    }
}
