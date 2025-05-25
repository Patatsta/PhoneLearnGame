using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;

public class LoginScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField[] _InputFields; // 0,1 Create, 2,3 Login
    [SerializeField] private Button[] _buttons;
    [SerializeField] private GameObject _login, _create, _mainmenu;
   
    [SerializeField] private TMP_Text _debugText;

    private string filePath;
    private AccountList accountList;

    [SerializeField] private GameManager _gameManager;

    private AccountData _currentAccount;

    [SerializeField] private TMP_Text[] gameHScoreText;
    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "accounts.json");
        Debug.Log("File Path: " + filePath);

        _login.SetActive(true);
        _create.SetActive(false);
        _debugText.text = "";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            accountList = JsonUtility.FromJson<AccountList>(json);
            Debug.Log("Loaded accounts: " + json);
        }
        else
        {
            accountList = new AccountList();
            Debug.Log("No existing account file found. Starting with empty account list.");
        }
    }

    public void CreateAccount()
    {
        string username = _InputFields[0].text;
        string password = _InputFields[1].text;

        if (username.Length > 3 && password.Length > 3)
        {
            string salt = GenerateSalt();
            string hash = HashPassword(password, salt);


            AccountData newAccount = new AccountData
            {
                username = username,
                salt = salt,
                passwordHash = hash,
            };

            accountList.accounts.Add(newAccount);

            SaveAccountsToFile();

            _debugText.text = "Account successfully created";
            OpenLoginAccountMenu();
        }
        else
        {
            _debugText.text = "Username and password must be at least 4 characters.";
            _InputFields[0].text = "";
            _InputFields[1].text = "";
        }
    }

    private void SaveAccountsToFile()
    {
      
        string json = JsonUtility.ToJson(accountList, true);
        File.WriteAllText(filePath, json);

        Debug.Log("Saved JSON: " + json);
    }

    public void LoginAccount()
    {
        string inputUsername = _InputFields[2].text;
        string inputPassword = _InputFields[3].text;

        foreach (var account in accountList.accounts)
        {
            if (inputUsername == account.username)
            {
                string inputHash = HashPassword(inputPassword, account.salt);
                if (inputHash == account.passwordHash)
                {
                    _currentAccount = account;
                    for (int i = 0; i < _currentAccount.highScores.Length; i++)
                    {
                        gameHScoreText[i].text = _currentAccount.highScores[i].ToString();
                    }
                    _debugText.text = "";
                    _InputFields[2].text = "";
                    _InputFields[3].text = "";
                    _gameManager.LoginSuccess();
                    return;
                }
                else
                {
                    _debugText.text = "Password incorrect.";
                    _InputFields[3].text = "";
                    return;
                }
            }
        }

        _debugText.text = "Username not found.";
        _InputFields[2].text = "";
        _InputFields[3].text = "";
    }

    public void LogOut()
    {
        _currentAccount = null;
    }

    public void UpdateHighScore(int index, int score)
    {
        _currentAccount.highScores[index -1] = score;
        gameHScoreText[index -1].text = _currentAccount.highScores[index -1].ToString();
        SaveAccountsToFile();
    }
    public void OpenCreateAccountMenu()
    {
        _login.SetActive(false);
        _create.SetActive(true);
    }

    public void OpenLoginAccountMenu()
    {
        _login.SetActive(true);
        _create.SetActive(false);
    }

    private string GenerateSalt()
    {
        byte[] saltBytes = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    private string HashPassword(string password, string salt)
    {
        string combined = password + salt;
        using (SHA256 sha = SHA256.Create())
        {
            byte[] hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(combined));
            return Convert.ToBase64String(hashBytes);
        }
    }

    public void DeleteAllAccounts()
    {
        accountList.accounts.Clear();

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("All accounts deleted.");
        }

        _debugText.text = "All accounts deleted.";
    }

    public int GetHighscores(int index)
    {
        return _currentAccount.highScores[index - 1];
    }

 
}

[System.Serializable]
public class AccountData
{
    public string username;
    public string salt;
    public string passwordHash;
    public int[] highScores = new int[3];
}

[System.Serializable]
public class AccountList
{
    public List<AccountData> accounts = new List<AccountData>();
}
