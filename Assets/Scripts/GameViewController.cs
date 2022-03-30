using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameViewController : MonoBehaviour
{
    [SerializeField] private GuessView m_guessView;
    [SerializeField] private KeyboardView m_keyboardView;
    [SerializeField] private WordGameErrorMessagePopup m_errorMessage;
    [SerializeField] private WordGameStatScreenPopup m_statPopup;
    [SerializeField] private WordGameWordPopup m_wordPopup;

    private string m_wordGuess = "";
    private WordGameManager m_wordGameManger;
    private string m_currentGameWord;
    private int m_currentGameWordLength;

    public void Setup(WordGameManager wordGameManager)
    {
        m_wordGameManger = wordGameManager;

        m_statPopup.Setup(this);
        m_guessView.Setup(this);
        m_keyboardView.Setup(this);

        m_currentGameWord = m_wordGameManger.GetCurrentGameWord();
        m_currentGameWordLength = m_wordGameManger.GetCurrentGameWordLength();
    }

    public void ResetViews()
    {
        m_wordGuess = "";
        m_currentGameWord = m_wordGameManger.GetCurrentGameWord();
        m_currentGameWordLength = m_wordGameManger.GetCurrentGameWordLength();
        m_wordPopup.HidePopup();

        m_guessView.ResetViews();
        m_keyboardView.ResetViews();
    }

    public void ShowStatPopup(bool wordGuessed, int guessAttempt)
    {
        m_statPopup.ShowPopup(wordGuessed, guessAttempt);
    }

    public void OnStatsClicked()
    {
        m_statPopup.ShowPopup();
    }

    public void AddLetterToGuess(string letter)
    {
        if (IsGameFinished())
        {
            return;
        }

        if (m_wordGuess.Length < m_currentGameWordLength)
        {
            m_wordGuess += letter;
            m_guessView.AddLetterToGuess(letter);
        }
    }

    public void RemoveLastLetter()
    {
        if (IsGameFinished())
        {
            return;
        }

        if (m_wordGuess.Length > 0)
        {
            m_wordGuess = m_wordGuess.Remove(m_wordGuess.Length - 1);
            m_guessView.RemoveLetterFromGuess();
        }
    }

    public void SubmitGuess()
    {
        if (IsGameFinished())
        {
            return;
        }

        // Add Checks to make sure word is valid and long enough
        if (m_wordGuess.Length != m_currentGameWordLength)
        {
            m_errorMessage.ShowErrorMessage("Not enough letters");
            return;
        }

        if (!m_wordGameManger.IsGuessValid(m_wordGuess))
        {
            m_errorMessage.ShowErrorMessage("Not in word list");
            return;
        }

        UpdateLetterStatus();

        // Check if Game is done
        if (m_wordGuess == m_currentGameWord.ToUpper())
        {
            WordFinished(m_guessView.GetCurrentGuessAttempt(), true);
        }
        else
        {
            m_wordGuess = "";
            m_guessView.MoveToNextGuess();
        }
    }

    public bool NewWordClicked()
    {
        if (m_wordGameManger.IsGameFinished())
        {
            m_wordGameManger.NextWord();
            return true;
        }
        else
        {
            m_errorMessage.ShowErrorMessage("Finish the current word.");
            return false;
        }
    }

    public void WordFinished(int guessAttempt, bool correctWordGuessed)
    {
        m_wordGameManger.CorrectWordGuessed(guessAttempt, correctWordGuessed);

        if (!correctWordGuessed)
        {
            m_wordPopup.ShowPopup(m_currentGameWord);
        }
    }

    public bool IsGameFinished()
    {
        return m_wordGameManger.IsGameFinished();
    }

    public int GetCurrentGameWordLength()
    {
        return m_wordGameManger.GetCurrentGameWordLength();
    }

    public WordGamePlayerPrefHandler GetPlayerPrefs()
    {
        return m_wordGameManger.GetPlayerPrefs();
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
