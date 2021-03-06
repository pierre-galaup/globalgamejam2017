﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _comradeNumberText;

    [SerializeField]
    private Text _timeText;

    [SerializeField]
    private GameObject _endGamePanel;

    [SerializeField]
    private Text _endGameScoreText;

    [SerializeField]
    private Text _endGameLeaderboardText;

    public GameObject ActionPanel;

    public void UpdateTime(string newTime)
    {
        _timeText.text = newTime;
    }

    public void UpdateScore(string score)
    {
        _scoreText.text = score;
    }

    public void UpdateComradeNumber(string actuelNumber, string objectiveNumber)
    {
        _comradeNumberText.text = actuelNumber + " / " + objectiveNumber;
    }

    public void UpdateComradeNumber(string actuelNumber)
    {
        _comradeNumberText.text = actuelNumber;
    }

    public void DisplayEndOfGame()
    {
        _endGamePanel.SetActive(true);

        int score = GameManager.Instance.GetNumberOfRadicalized();

        List<int> scores;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            scores = GameManager.Instance.LeaderBoard.GetScores(1);
            GameManager.Instance.LeaderBoard.SaveNewScoreMap1(score);
        }
        else
        {
            scores = GameManager.Instance.LeaderBoard.GetScores(2);
            GameManager.Instance.LeaderBoard.SaveNewScoreMap2(score);
        }

        _endGameScoreText.text = "Your score : " + score;

        string leaderboard = "";
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i] == -1)
                break;
            leaderboard += "Score " + i + " : " + scores[i] + "\r\n";
        }
        _endGameLeaderboardText.text = leaderboard;
    }

    public void Quit()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Regame()
    {
        _endGamePanel.SetActive(false);

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                SceneManager.LoadScene(2);
                break;

            case 2:
                SceneManager.LoadScene(1);
                break;
        }
    }
}