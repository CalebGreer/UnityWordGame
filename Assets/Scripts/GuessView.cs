using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessView : MonoBehaviour
{
    [SerializeField] private int m_numGuesses = 6;
    [SerializeField] private GameObject m_guessesContainer;
    [SerializeField] private GuessWordRow m_guessRow;

    private GameViewController m_parentController;
    private List<GuessWordRow> m_guessRows = new List<GuessWordRow>();
    private int m_currentGuessIndex = 0;


    [ContextMenu("Setup")]
    public void Setup(GameViewController parent)
    {
        m_parentController = parent;

        for (int i = 0; i < m_numGuesses; i++)
        {
            GuessWordRow row = Instantiate(m_guessRow, m_guessesContainer.transform);
            row.Setup(this);
            m_guessRows.Add(row);
        }

        m_currentGuessIndex = 0;
    }

    public void ResetViews()
    {
        m_currentGuessIndex = 0;

        for (int i = 0; i < m_guessRows.Count; i++)
        {
            m_guessRows[i].ResetRow();
        }
    }

    public void AddLetterToGuess(string letter)
    {
        m_guessRows[m_currentGuessIndex].AddLetterToGuess(letter);
    }

    public void RemoveLetterFromGuess()
    {
        m_guessRows[m_currentGuessIndex].RemoveLetterFromGuess();
    }

    public int GetCurrentGameWordLength()
    {
        return m_parentController.GetCurrentGameWordLength();
    }

    public void SetGuessLetterState(int index, LetterState state)
    {
        m_guessRows[m_currentGuessIndex].SetGuessLetterState(index, state);
    }

    public int GetCurrentGuessAttempt()
    {
        return m_currentGuessIndex + 1;
    }

    public void MoveToNextGuess()
    {
        m_currentGuessIndex++;

        if (m_currentGuessIndex >= m_numGuesses)
        {
            m_parentController.WordFinished(GetCurrentGuessAttempt(), false);
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Reset")]
    private void ClearRows()
    {
        foreach (GuessWordRow row in m_guessRows)
        {
            DestroyImmediate(row.gameObject);
        }
        m_guessRows.Clear();
    }
#endif
}
