using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordGameErrorMessagePopup : MonoBehaviour
{
    [SerializeField] private Text m_messageText;
    [SerializeField] private Animation m_animation;

    public void ShowErrorMessage(string message)
    {
        m_messageText.text = message;

        if (!m_animation.isPlaying)
        {
            m_animation.Play();
        }
    }
}
