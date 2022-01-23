using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameViewController : MonoBehaviour
{
    [SerializeField] private GuessView m_guessView;
    [SerializeField] private KeyboardView m_keyboardView;

    [SerializeField] private string m_wordGuess;

    // TEST DATA
    private string m_correctWord = "WATER";
    private int m_wordLength = 5;

    public void Setup()
    {
        m_guessView.Setup(this);
        m_keyboardView.Setup(this);
    }

    public void AddLetterToGuess(string letter)
    {
        if (m_wordGuess.Length < m_wordLength)
        {
            m_wordGuess += letter;
            m_guessView.AddLetterToGuess(letter);
        }
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

    public void SubmitGuess()
    {
        // Add Checks to make sure word is valid and long enough
        if(m_wordGuess.Length != m_wordLength)
        {
            Debug.LogError("Word is not " + m_wordLength + " characters long");
            return;
        }

        UpdateLetterStatus();
    }


    private void UpdateLetterStatus()
    {
        // Initially set everything as incorrect
        for (int i = 0; i < m_wordGuess.Length; i++)
        {
            m_guessView.SetGuessLetterState(i, LetterState.Incorrect);
        }

        List<int> correctIndices = new List<int>();

        // Check if the letter is correct and in the right position
        for (int i = 0; i < m_wordGuess.Length; i++)
        {
            if (m_wordGuess[i] == m_correctWord[i])
            {
                m_guessView.SetGuessLetterState(i, LetterState.CorrectPosition);
                m_keyboardView.SetKeyboardLetterState(m_wordGuess[i], LetterState.CorrectPosition);
                correctIndices.Add(i);
            }
            else
            {
                m_keyboardView.SetKeyboardLetterState(m_wordGuess[i], LetterState.Incorrect);
            }    
        }

        // Remove the correct letters to check for doubles
        string tempWord = RemoveCorrectLetters(correctIndices, m_correctWord);

        // Check if the letter is correct but in the wrong position
        for (int i = 0; i < m_wordGuess.Length; i++)
        {
            if (tempWord.Contains(m_wordGuess[i].ToString()))
            {
                m_guessView.SetGuessLetterState(i, LetterState.CorrectLetter);
                m_keyboardView.SetKeyboardLetterState(m_wordGuess[i], LetterState.CorrectLetter);
            }
        }
    }

    private string RemoveCorrectLetters(List<int> indices, string word)
    {
        char[] tempWord = word.ToCharArray();
        string result = "";

        for (int i = 0; i < tempWord.Length; i++)
        {
            if(indices.Contains(i))
            {
                continue;
            }

            result += tempWord[i];
        }

        return result;
    }
}
