using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameViewController : MonoBehaviour
{
    [SerializeField] private GuessView m_guessView;
    [SerializeField] private KeyboardView m_keyboardView;

    [SerializeField] private string m_wordGuess;

    private int m_wordLength = 5;

    public void Setup()
    {
        m_guessView.Setup(this);
        m_keyboardView.Setup(this);
    }

    public void AddLetterToGuess(string letter)
    {
        m_wordGuess += letter;
        m_guessView.AddLetterToGuess(letter);
    }

    public void RemoveLastLetter()
    {
        if (m_wordGuess.Length > 0)
        {
            m_wordGuess = m_wordGuess.Remove(m_wordGuess.Length - 1);
            m_guessView.RemoveLetterFromGuess();
        }
    }

    public int GetWordLength()
    {
        return m_wordLength;
    }
}
