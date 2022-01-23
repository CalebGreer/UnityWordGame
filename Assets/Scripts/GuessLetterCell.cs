using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuessLetterCell : MonoBehaviour
{
    [SerializeField] private Text m_letterText;
    [SerializeField] private Image m_backgroundImage;
    [SerializeField] private Outline m_cellOutline;

    [Header("Colours")]
    [SerializeField] private Color m_incorrectColour;
    [SerializeField] private Color m_correctLetterColour;
    [SerializeField] private Color m_correctPositionColour;
    
    private Color m_defaultColour = Color.black;
    private LetterState m_currentState = LetterState.Default;
    private GuessWordRow m_parent;

    public void Setup(GuessWordRow parent)
    {
        m_parent = parent;
        // TODO: Keep playerpref and load last state

        m_letterText.text = "";
        SetGuessLetterState(m_currentState);
    }

    public void SetGuessLetter(string letter)
    {
        m_letterText.text = letter;
    }

    public void SetGuessLetterState(LetterState newState)
    {
        m_currentState = newState;
        UpdateGuessLetterStateVisuals();
    }

    private void UpdateGuessLetterStateVisuals()
    {
        switch(m_currentState)
        {
            case LetterState.Default:
                m_backgroundImage.color = m_defaultColour;
                m_cellOutline.effectColor = m_incorrectColour;
                break;

            case LetterState.Incorrect:
                m_backgroundImage.color = m_incorrectColour;
                m_cellOutline.effectColor = m_incorrectColour;
                break;

            case LetterState.CorrectLetter:
                m_backgroundImage.color = m_correctLetterColour;
                m_cellOutline.effectColor = m_correctLetterColour;
                break;

            case LetterState.CorrectPosition:
                m_backgroundImage.color = m_correctPositionColour;
                m_cellOutline.effectColor = m_correctPositionColour;
                break;
        }
    }
}
