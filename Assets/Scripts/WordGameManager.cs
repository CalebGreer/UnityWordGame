using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class WordGameManager : MonoBehaviour
{
    private const string WORDLIST_JSON = "Assets/WordData/WordList.txt";
    private const string GUESSLIST_JSON = "Assets/WordData/ValidGuessList.txt";
    private const int RANDOM_SEED = 12345;

    [SerializeField] private GameViewController m_gameViewController;

    private GameWordList m_guessList;
    private GameWordList m_wordList;
    private string m_currentGameWord;
    private int m_currentGameWordLength = 5; // Eventually support more lengths

    private WordGamePlayerPrefHandler m_playerPrefHandler;

    void Start()
    {
        m_guessList = JsonConvert.DeserializeObject<GameWordList>(File.ReadAllText(GUESSLIST_JSON));
        m_wordList = JsonConvert.DeserializeObject<GameWordList>(File.ReadAllText(WORDLIST_JSON));
        ShuffleWordList(m_wordList);

        m_playerPrefHandler = new WordGamePlayerPrefHandler();

        m_currentGameWord = m_wordList.words[m_playerPrefHandler.WordNumber];

        m_gameViewController.Setup(this);
    }

    public bool IsGuessValid(string guess)
    {
        for (int i = 0; i < m_guessList.words.Count; i++)
        {
            if (m_guessList.words[i].ToUpper() == guess)
            {
                return true;
            }
        }

        return false;
    }

    public string GetCurrentGameWord()
    {
        return m_currentGameWord.ToUpper();
    }

    public int GetCurrentGameWordLength()
    {
        return m_currentGameWordLength;
    }

    private void ShuffleWordList(GameWordList gameWordList)
    {
        System.Random randValue = new System.Random(RANDOM_SEED);

        for (int i = 0; i < gameWordList.words.Count; i++)
        {
            int j = randValue.Next(i, gameWordList.words.Count);
            string value = gameWordList.words[j];
            gameWordList.words[j] = gameWordList.words[i];
            gameWordList.words[i] = value;
        }
    }
}
