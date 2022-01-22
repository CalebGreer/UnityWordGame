using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardView : MonoBehaviour
{
    [SerializeField] private List<KeyboardKey> m_keyboardKeys = new List<KeyboardKey>();

    private GameViewController m_parentController;

    public void Setup(GameViewController parent)
    {
        m_parentController = parent;

        for (int i = 0; i < m_keyboardKeys.Count; i++)
        {
            m_keyboardKeys[i].Setup(this);
        }
    }

    public void AddLetterToGuess(string letter)
    {
        m_parentController.AddLetterToGuess(letter);
    }

    public void RemoveLastLetter()
    {
        m_parentController.RemoveLastLetter();
    }
}