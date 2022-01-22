using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessViewController : MonoBehaviour
{
    [SerializeField] private int m_numGuesses = 6;
    [SerializeField] private GameObject m_guessesContainer;
    [SerializeField] private GuessWordRow m_guessRow;

    private List<GuessWordRow> m_guessRows = new List<GuessWordRow>();

    [ContextMenu("Setup")]
    public void Setup()
    {
        for(int i = 0; i < m_numGuesses; i++)
        {
            GuessWordRow row = Instantiate(m_guessRow, m_guessesContainer.transform);     
            row.Setup(this, 5);
            m_guessRows.Add(row);
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Reset")]
    private void ClearRows()
    {
        foreach(GuessWordRow row in m_guessRows)
        {
            DestroyImmediate(row.gameObject);
        }
        m_guessRows.Clear();
    }
#endif
}
