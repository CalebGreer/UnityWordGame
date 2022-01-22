using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessWordRow : MonoBehaviour
{
    [SerializeField] private GuessLetterCell m_letterCell;

    private List<GuessLetterCell> m_letterCells = new List<GuessLetterCell>();
    private GuessViewController m_parentView;

    public void Setup(GuessViewController parent, int wordLength)
    {
        m_parentView = parent;

        for (int i = 0; i < wordLength; i++)
        {
            GuessLetterCell cell = Instantiate(m_letterCell, this.transform);
            cell.Setup(this);
            m_letterCells.Add(cell);
        }
    }
}
