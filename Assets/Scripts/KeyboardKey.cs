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

    KeyboardView m_parentView;

    public void Setup(KeyboardView parent)
    {
        m_parentView = parent;
    }

    public void OnLetterKeyPress()
    {
        m_parentView.AddLetterToGuess(m_keyText.text);
    }

    public void OnEnterPressed()
    {

    }

    public void OnDeletePressed()
    {
        m_parentView.RemoveLastLetter();
    }
}
