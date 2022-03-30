using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordGameGuessStat : MonoBehaviour
{
    [SerializeField] private Color m_highlightColour;
    [SerializeField] private Text m_prefixText;
    [SerializeField] private Text m_statAmount;

    public void SetStat(int statAmount)
    {
        m_statAmount.text = statAmount.ToString();
    }

    public void HighlightText()
    {
        m_prefixText.color = m_highlightColour;
        m_statAmount.color = m_highlightColour;
    }

    public void RemoveHighlight()
    {
        m_prefixText.color = Color.white;
        m_statAmount.color = Color.white;
    }
}
