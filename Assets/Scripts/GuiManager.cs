using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _comradeNumberText;

    [SerializeField]
    private Text _timeText;

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
}