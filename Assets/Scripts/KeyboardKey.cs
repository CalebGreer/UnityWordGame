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

    KeyboardController m_parentController;

    public void Setup(KeyboardController parent)
    {
        m_parentController = parent;
    }

    public void OnLetterKeyPress()
    {
        m_parentController.AddLetterToGuess(m_keyText.text);
    }

    public void OnEnterPressed()
    {

    }

    public void OnDeletePressed()
    {
        m_parentController.RemoveLastLetter();
    }
}
