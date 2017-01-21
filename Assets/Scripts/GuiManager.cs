using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

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

    public void DisplayEndOfGame()
    {
        _endGamePanel.SetActive(true);

        Random r = new Random();
        int score = r.Next(20, 250);

        GameManager.Instance.LeaderBoard.SaveNewScore(score);

        _endGameScoreText.text = "Your score : " + score;

        List<int> scores = GameManager.Instance.LeaderBoard.GetScores();
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
        SceneManager.LoadScene(1);
    }
}