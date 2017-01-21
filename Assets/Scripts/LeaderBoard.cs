using System.Collections.Generic;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    private readonly List<int> _scores = new List<int>
    {
        -1,
        -1,
        -1,
        -1,
        -1
    };

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey("score" + i))
            {
                _scores[i] = PlayerPrefs.GetInt("score" + i);
            }
        }
    }

    public List<int> GetScores()
    {
        return _scores;
    }

    public void SaveNewScore(int newScore)
    {
        int i = 0;
        while (i < _scores.Count)
        {
            if (_scores[i] < newScore)
            {
                break;
            }
            i++;
        }

        _scores.Insert(i, newScore);
        if (_scores.Count > 5)
        {
            _scores.RemoveAt(5);
        }

        SaveScoreInPlayerPrefs();
    }

    private void SaveScoreInPlayerPrefs()
    {
        for (int i = 0; i < _scores.Count; i++)
        {
            PlayerPrefs.SetInt("score" + i, _scores[i]);
        }
        PlayerPrefs.Save();
    }
}