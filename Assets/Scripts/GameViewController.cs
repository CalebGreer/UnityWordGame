using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameViewController : MonoBehaviour
{
    [SerializeField] private GuessView m_guessView;
    [SerializeField] private KeyboardView m_keyboardView;

    [SerializeField] private string m_wordGuess;

    private WordGameManager m_wordGameManger;
    private string m_currentGameWord;
    private int m_currentGameWordLength;

    public void Setup(WordGameManager wordGameManager)
    {
        m_wordGameManger = wordGameManager;

        m_guessView.Setup(this);
        m_keyboardView.Setup(this);

        m_currentGameWord = m_wordGameManger.GetCurrentGameWord();
        m_currentGameWordLength = m_wordGameManger.GetCurrentGameWordLength();
    }

    public void AddLetterToGuess(string letter)
    {
        if (m_wordGuess.Length < m_currentGameWordLength)
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

    public void SubmitGuess()
    {
        // Add Checks to make sure word is valid and long enough
        if (m_wordGuess.Length != m_currentGameWordLength)
        {
            m_guessView.ShowErrorMessage("Not enough letters");
            return;
        }

        if (!m_wordGameManger.IsGuessValid(m_wordGuess))
        {
            m_guessView.ShowErrorMessage("Not in word list");
            return;
        }

        UpdateLetterStatus();
        m_wordGuess = "";
        m_guessView.MoveToNextGuess();
    }

    public int GetCurrentGameWordLength()
    {
        return m_wordGameManger.GetCurrentGameWordLength();
    }

    private void UpdateLetterStatus()
    {
        List<int> correctIndices = new List<int>();

        // Check if the letter is correct and in the right position
        for (int i = 0; i < m_wordGuess.Length; i++)
        {
            if (m_wordGuess[i] == m_currentGameWord[i])
            {
                m_guessView.SetGuessLetterState(i, LetterState.CorrectPosition);
                m_keyboardView.SetKeyboardLetterState(m_wordGuess[i], LetterState.CorrectPosition);
                correctIndices.Add(i);
            }
            else
            {
                m_guessView.SetGuessLetterState(i, LetterState.Incorrect);
                m_keyboardView.SetKeyboardLetterState(m_wordGuess[i], LetterState.Incorrect);
            }
        }

        // Remove the correct letters to check for doubles
        char[] tempWord = RemoveCorrectLetters(correctIndices, m_currentGameWord).ToCharArray();
        char[] guessAsChars = RemoveCorrectLetters(correctIndices, m_wordGuess).ToCharArray();

        // Check if the letter is correct but in the wrong position
        for (int i = 0; i < tempWord.Length; i++)
        {
            for (int j = 0; j < guessAsChars.Length; j++)
            {
                if (tempWord[i] == guessAsChars[j] && !char.IsWhiteSpace(tempWord[i]))
                {
                    m_guessView.SetGuessLetterState(j, LetterState.CorrectLetter);
                    m_keyboardView.SetKeyboardLetterState(guessAsChars[j], LetterState.CorrectLetter);
                    tempWord[i] = ' ';
                    guessAsChars[j] = ' ';
                }
            }
        }
    }

    private string RemoveCorrectLetters(List<int> indices, string word)
    {
        char[] tempWord = word.ToCharArray();
        string result = "";

        for (int i = 0; i < tempWord.Length; i++)
        {
            if (indices.Contains(i))
            {
                result += " ";
                continue;
            }

            result += tempWord[i];
        }

        return result;
    }
}
