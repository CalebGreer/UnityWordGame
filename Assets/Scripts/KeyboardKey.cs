using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardKey : MonoBehaviour
{
    [SerializeField] private Button m_button;
    [SerializeField] private Image m_keyImage;
    [SerializeField] private Text m_keyText;

    [Header("Colours")]
    [SerializeField] private Color m_incorrectColour;
    [SerializeField] private Color m_correctLetterColour;
    [SerializeField] private Color m_correctPositionColour;
    private Color m_defaultColour = Color.grey;

    private LetterState m_currentState = LetterState.Default;
    private KeyboardView m_parentView;
    private char m_keyLetter;

    public void Setup(KeyboardView parent)
    {
        m_parentView = parent;
        m_keyImage.color = m_defaultColour;

        if(m_keyText != null && m_keyText.text.Length == 1)
        {
            m_keyLetter = m_keyText.text[0];
        }
    }

    public void OnLetterKeyPress()
    {
        m_parentView.AddLetterToGuess(m_keyText.text);
    }

    public void OnEnterPressed()
    {
        m_parentView.SubmitGuess();
    }

    public void OnDeletePressed()
    {
        m_parentView.RemoveLastLetter();
    }

    public char GetKeyLetter()
    {
        return m_keyLetter;
    }

    public void SetKeyboardLetterState(LetterState newState, bool forceChange = false)
    {
        if (m_currentState != LetterState.CorrectPosition && forceChange == false)
        {
            m_currentState = newState;
            UpdateKeyboardLetterStateVisuals();
        }
    }

    private void UpdateKeyboardLetterStateVisuals()
    {
        switch (m_currentState)
        {
            case LetterState.Default:
                m_keyImage.color = m_defaultColour;
                break;

            case LetterState.Incorrect:
                m_keyImage.color = m_incorrectColour;
                break;

            case LetterState.CorrectLetter:
                m_keyImage.color = m_correctLetterColour;
                break;

            case LetterState.CorrectPosition:
                m_keyImage.color = m_correctPositionColour;
                break;
        }
    }
}
