using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordGameStatScreenPopup : MonoBehaviour
{
    [SerializeField] private Text m_gamesPlayedStat;
    [SerializeField] private Text m_winRateStat;
    [SerializeField] private Text m_currentStreakStat;
    [SerializeField] private Text m_maxStreakStat;

    [SerializeField] private List<WordGameGuessStat> m_guessStats = new List<WordGameGuessStat>();

    private GameViewController m_viewController;
    private WordGamePlayerPrefHandler m_playerPrefsHandler;

    public void Setup(GameViewController viewController)
    {
        m_viewController = viewController;

        m_playerPrefsHandler = m_viewController.GetPlayerPrefs();
        ResetStatText();
    }

    public void ShowPopup(bool wordGuessed = false, int guessAttempt = -1)
    {
        RefreshStats();

        if (wordGuessed)
        {
            m_guessStats[guessAttempt].HighlightText();
        }

        this.gameObject.SetActive(true);
    }

    public void NewWordClicked()
    {
        m_viewController.NewWordClicked();

        HidePopup();
    }

    public void HidePopup()
    {
        this.gameObject.SetActive(false);
    }

    private void RefreshStats()
    {
        ResetStatText();
        m_playerPrefsHandler.LoadPlayerPrefs();

        m_gamesPlayedStat.text = m_playerPrefsHandler.GamesPlayed.ToString();

        int winRate = 0;
        if (m_playerPrefsHandler.GamesPlayed > 0)
        {
            float winRateF = (float)m_playerPrefsHandler.GetGamesWon() / m_playerPrefsHandler.GamesPlayed * 100;
            winRate = (int)winRateF;
        }

        m_winRateStat.text = winRate.ToString();
        m_currentStreakStat.text = m_playerPrefsHandler.CurrentStreak.ToString();
        m_maxStreakStat.text = m_playerPrefsHandler.MaxStreak.ToString();

        m_guessStats[0].SetStat(m_playerPrefsHandler.FirstGuessAmount);
        m_guessStats[1].SetStat(m_playerPrefsHandler.SecondGuessAmount);
        m_guessStats[2].SetStat(m_playerPrefsHandler.ThirdGuessAmount);
        m_guessStats[3].SetStat(m_playerPrefsHandler.FourthGuessAmount);
        m_guessStats[4].SetStat(m_playerPrefsHandler.FifthGuessAmount);
        m_guessStats[5].SetStat(m_playerPrefsHandler.SixthGuessAmount);
    }

    private void ResetStatText()
    {
        foreach (WordGameGuessStat stat in m_guessStats)
        {
            stat.RemoveHighlight();
        }
    }
}
