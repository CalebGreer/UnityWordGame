using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    [SerializeField] private List<KeyboardKey> m_keyboardKeys = new List<KeyboardKey>();
    [SerializeField] private string m_wordGuess;

    public void AddLetterToGuess(string letter)
    {
        m_wordGuess += letter;
    }

    public void RemoveLastLetter()
    {
        m_wordGuess = m_wordGuess.Remove(m_wordGuess.Length - 1);
    }

    private void Awake()
    {
        for (int i = 0; i < m_keyboardKeys.Count; i++)
        {
            m_keyboardKeys[i].Setup(this);
        }
    }
}
