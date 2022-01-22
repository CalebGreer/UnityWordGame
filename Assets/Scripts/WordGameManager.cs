using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGameManager : MonoBehaviour
{
    public GameViewController m_gameViewController;

    // Start is called before the first frame update
    void Start()
    {
        m_gameViewController.Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
