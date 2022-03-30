using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordGameWordPopup : MonoBehaviour
{
    private const string WORD_MESSAGE = "The word was: {0}";

    [SerializeField] private Text m_wordMessage;

    public void ShowPopup(string word)
    {
        m_wordMessage.text = string.Format(WORD_MESSAGE, word);
        this.gameObject.SetActive(true);
    }

    public void HidePopup()
    {
        this.gameObject.SetActive(false);
    }
}
