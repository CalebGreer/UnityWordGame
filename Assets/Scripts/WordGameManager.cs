using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGameManager : MonoBehaviour
{
    public GuessViewController m_guessViewController;

    // Start is called before the first frame update
    void Start()
    {
        m_guessViewController.Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
