using System.Collections.Generic;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    private readonly List<int> _scoresMap1 = new List<int>
    {
        -1,
        -1,
        -1,
        -1,
        -1
    };

    private readonly List<int> _scoresMap2 = new List<int>
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
            if (PlayerPrefs.HasKey("map1-score" + i))
            {
                _scoresMap1[i] = PlayerPrefs.GetInt("map1-score" + i);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey("map2-score" + i))
            {
                _scoresMap2[i] = PlayerPrefs.GetInt("map2-score" + i);
            }
        }
    }

    public List<int> GetScores(int mapNumber)
    {
        switch (mapNumber)
        {
            case 1:
                return _scoresMap1;

            case 2:
                return _scoresMap2;
        }
        return null;
    }

    public void SaveNewScoreMap1(int newScore)
    {
        int i = 0;
        while (i < _scoresMap1.Count)
        {
            if (_scoresMap1[i] < newScore)
            {
                break;
            }
            i++;
        }

        _scoresMap1.Insert(i, newScore);
        if (_scoresMap1.Count > 5)
        {
            _scoresMap1.RemoveAt(5);
        }

        SaveScoreInPlayerPrefs();
    }

    public void SaveNewScoreMap2(int newScore)
    {
        int i = 0;
        while (i < _scoresMap2.Count)
        {
            if (_scoresMap2[i] < newScore)
            {
                break;
            }
            i++;
        }

        _scoresMap2.Insert(i, newScore);
        if (_scoresMap2.Count > 5)
        {
            _scoresMap2.RemoveAt(5);
        }

        SaveScoreInPlayerPrefs();
    }

    private void SaveScoreInPlayerPrefs()
    {
        for (int i = 0; i < _scoresMap1.Count; i++)
        {
            PlayerPrefs.SetInt("map1-score" + i, _scoresMap1[i]);
        }

        for (int i = 0; i < _scoresMap2.Count; i++)
        {
            PlayerPrefs.SetInt("map2-score" + i, _scoresMap2[i]);
        }

        PlayerPrefs.Save();
    }
}