using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WordGamePlayerPrefHandler
{
    // PLAYER PREF INFO
    public int WordNumber { get; private set; }

    public int FirstGuessAmount { get; private set; }
    public int SecondGuessAmount { get; private set; }
    public int ThirdGuessAmount { get; private set; }
    public int FourthGuessAmount { get; private set; }
    public int FifthGuessAmount { get; private set; }
    public int SixthGuessAmount { get; private set; }

    public int GamesPlayed { get; private set; }

    public int CurrentStreak { get; private set; }
    public int MaxStreak { get; private set; }

    public int TimesLooped { get; private set; }

    public WordGamePlayerPrefHandler()
    {
        LoadPlayerPrefs();
    }

    public void IncreaseWordNumber()
    {
        WordNumber++;
        SavePlayerPrefs();
    }

    public void IncreaseGuessAmount(int guessAttempts)
    {
        switch (guessAttempts)
        {
            case 1:
                FirstGuessAmount++;
                break;

            case 2:
                SecondGuessAmount++;
                break;

            case 3:
                ThirdGuessAmount++;
                break;

            case 4:
                FourthGuessAmount++;
                break;

            case 5:
                FifthGuessAmount++;
                break;

            case 6:
                SixthGuessAmount++;
                break;

            default:
                break;
        }

        SavePlayerPrefs();
    }

    public void IncreaseGamesPlayed()
    {
        GamesPlayed++;
        SavePlayerPrefs();
    }

    public void SetCurrentStreak(bool guessedAnswer)
    {
        if (guessedAnswer)
        {
            CurrentStreak++;
        }
        else
        {
            CurrentStreak = 0;
        }

        SavePlayerPrefs();
    }

    public int GetGamesWon()
    {
        return FirstGuessAmount + SecondGuessAmount + ThirdGuessAmount + FourthGuessAmount + FifthGuessAmount + SixthGuessAmount;
    }

    public void ResetWordNumber()
    {
        WordNumber = 0;
        TimesLooped++;
        SavePlayerPrefs();
        LoadPlayerPrefs();
    }

    public void LoadPlayerPrefs()
    {
        WordNumber = PlayerPrefs.GetInt("WordNumber", 0);

        FirstGuessAmount = PlayerPrefs.GetInt("FirstGuessAmount", 0);
        SecondGuessAmount = PlayerPrefs.GetInt("SecondGuessAmount", 0);
        ThirdGuessAmount = PlayerPrefs.GetInt("ThirdGuessAmount", 0);
        FourthGuessAmount = PlayerPrefs.GetInt("FourthGuessAmount", 0);
        FifthGuessAmount = PlayerPrefs.GetInt("FifthGuessAmount", 0);
        SixthGuessAmount = PlayerPrefs.GetInt("SixthGuessAmount", 0);

        GamesPlayed = PlayerPrefs.GetInt("GamesPlayed", 0);

        CurrentStreak = PlayerPrefs.GetInt("CurrentStreak", 0);
        MaxStreak = PlayerPrefs.GetInt("MaxStreak", 0);

        TimesLooped = PlayerPrefs.GetInt("TimesLooped", 0);
    }

    private void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("WordNumber", WordNumber);

        PlayerPrefs.SetInt("FirstGuessAmount", FirstGuessAmount);
        PlayerPrefs.SetInt("SecondGuessAmount", SecondGuessAmount);
        PlayerPrefs.SetInt("ThirdGuessAmount", ThirdGuessAmount);
        PlayerPrefs.SetInt("FourthGuessAmount", FourthGuessAmount);
        PlayerPrefs.SetInt("FifthGuessAmount", FifthGuessAmount);
        PlayerPrefs.SetInt("SixthGuessAmount", SixthGuessAmount);

        PlayerPrefs.SetInt("GamesPlayed", GamesPlayed);

        PlayerPrefs.SetInt("CurrentStreak", CurrentStreak);

        if (CurrentStreak > MaxStreak)
        {
            PlayerPrefs.SetInt("MaxStreak", CurrentStreak);

        }
        else
        {
            PlayerPrefs.SetInt("MaxStreak", MaxStreak);
        }

        PlayerPrefs.SetInt("TimesLooped", TimesLooped);
    }

#if UNITY_EDITOR
    [MenuItem("PlayerPrefs/Delete All PlayerPrefs")]
    private static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
#endif
}
