using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class WordGameManager : MonoBehaviour
{
    private const string WORDLIST_JSON = "WordData/WordList";
    private const string GUESSLIST_JSON = "WordData/ValidGuessList";
    private const int RANDOM_SEED = 12345;

    [SerializeField] private GameViewController m_gameViewController;

    private GameWordList m_guessList;
    private GameWordList m_wordList;
    private string m_currentGameWord;
    private int m_currentGameWordLength = 5; // Eventually support more lengths
    private bool m_gameFinished = false;

    private WordGamePlayerPrefHandler m_playerPrefHandler;

    void Start()
    {
        TextAsset wordListTxt = Resources.Load<TextAsset>(WORDLIST_JSON);
        TextAsset guessListTxt = Resources.Load<TextAsset>(GUESSLIST_JSON);

        m_guessList = CreateGameWordList(GUESSLIST_JSON);
        m_wordList = CreateGameWordList(WORDLIST_JSON);

        m_playerPrefHandler = new WordGamePlayerPrefHandler();
        
        ShuffleWordList(m_wordList);

        m_currentGameWord = GetNewGameWord();

        m_gameViewController.Setup(this);
    }

    public void CorrectWordGuessed(int guessAttempt, bool correctWordGuessed)
    {
        m_gameFinished = true;

        UpdateGameStats(guessAttempt, correctWordGuessed);
        m_gameViewController.ShowStatPopup(correctWordGuessed, guessAttempt - 1);
    }

    public void NextWord()
    {
        if (!m_gameFinished)
        {
            UpdateGameStats(-1, false);
        }

        m_currentGameWord = GetNewGameWord();
        m_gameViewController.ResetViews();
        m_gameFinished = false;
    }

    public bool IsGameFinished()
    {
        return m_gameFinished;
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

    public WordGamePlayerPrefHandler GetPlayerPrefs()
    {
        return m_playerPrefHandler;
    }

    private void UpdateGameStats(int guessAttempt, bool correctWordGuessed)
    {
        m_playerPrefHandler.IncreaseGamesPlayed();
        m_playerPrefHandler.IncreaseGuessAmount(guessAttempt);
        m_playerPrefHandler.IncreaseWordNumber();
        m_playerPrefHandler.SetCurrentStreak(correctWordGuessed);
    }

    private void ShuffleWordList(GameWordList gameWordList)
    {
        System.Random randValue = new System.Random(RANDOM_SEED + m_playerPrefHandler.TimesLooped);

        for (int i = 0; i < gameWordList.words.Count; i++)
        {
            int j = randValue.Next(i, gameWordList.words.Count);
            string value = gameWordList.words[j];
            gameWordList.words[j] = gameWordList.words[i];
            gameWordList.words[i] = value;
        }
    }

    private string GetNewGameWord()
    {
        if (m_playerPrefHandler.WordNumber > m_wordList.words.Count)
        {
            m_playerPrefHandler.ResetWordNumber();
            m_wordList = CreateGameWordList(WORDLIST_JSON);
            ShuffleWordList(m_wordList);
        }

        return m_wordList.words[m_playerPrefHandler.WordNumber];
    }

    private GameWordList CreateGameWordList(string path)
    {
        TextAsset wordListTxt = Resources.Load<TextAsset>(path);
        GameWordList wordList = JsonConvert.DeserializeObject<GameWordList>(wordListTxt.text);
        return wordList;
    }
}
