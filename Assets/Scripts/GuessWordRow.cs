using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessWordRow : MonoBehaviour
{
    [SerializeField] private GuessLetterCell m_letterCell;

    private List<GuessLetterCell> m_letterCells = new List<GuessLetterCell>();
    private GuessView m_parentView;
    private int m_currentLetterIndex = 0;

    public void Setup(GuessView parent)
    {
        m_parentView = parent;

        for (int i = 0; i < m_parentView.GetCurrentGameWordLength(); i++)
        {
            GuessLetterCell cell = Instantiate(m_letterCell, this.transform);
            cell.Setup(this);
            m_letterCells.Add(cell);
        }

        m_currentLetterIndex = 0;
    }

    public void ResetRow()
    {
        for (int i = 0; i < m_letterCells.Count; i++)
        {
            m_letterCells[i].ResetCell();
        }

        m_currentLetterIndex = 0;
    }

    public void AddLetterToGuess(string letter)
    {
        m_letterCells[m_currentLetterIndex].SetGuessLetter(letter);
        m_currentLetterIndex = Mathf.Clamp(m_currentLetterIndex + 1, 0, m_parentView.GetCurrentGameWordLength());
    }

    public void RemoveLetterFromGuess()
    {
        m_currentLetterIndex = Mathf.Clamp(m_currentLetterIndex - 1, 0, m_parentView.GetCurrentGameWordLength());
        m_letterCells[m_currentLetterIndex].SetGuessLetter("");
    }

    public void SetGuessLetterState(int index, LetterState state)
    {
        m_letterCells[index].SetGuessLetterState(state);
    }
}
