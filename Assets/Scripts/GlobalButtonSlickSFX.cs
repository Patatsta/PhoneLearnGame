using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalButtonSlickSFX : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;

    private void Awake()
    {
        List<Button> allButtons = new List<Button>();
        foreach (var root in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
        {
            allButtons.AddRange(GetAllButtonsInChildren(root.transform));
        }

        foreach (var btn in allButtons)
        {
            btn.onClick.AddListener(() => PlayClickSound());
        }
    }

    private List<Button> GetAllButtonsInChildren(Transform parent)
    {
        List<Button> result = new List<Button>();

        foreach (Transform child in parent)
        {
            Button btn = child.GetComponent<Button>();
            if (btn != null)
            {
                result.Add(btn);
            }
            result.AddRange(GetAllButtonsInChildren(child));
        }

        return result;
    }

    private void PlayClickSound()
    {
        if (SoundManager.Instance != null && clickSound != null)
        {
            SoundManager.Instance.PlaySFX(clickSound);
        }
    }
}


