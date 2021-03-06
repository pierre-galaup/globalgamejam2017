﻿using GameTime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TimeManager TimeManager;

    public GuiManager GuiManager;

    public PlayerRadicalization PlayerRadicalization;

    public LeaderBoard LeaderBoard;

    public float GameDuration = 45;

    private int _numberOfRadicalized = 0;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        TimeManager.StartTimer(GameDuration);
    }

    public int GetNumberOfRadicalized()
    {
        return _numberOfRadicalized;
    }

    public void EndTimer()
    {
        GetComponent<AudioSource>().Play();
        GuiManager.DisplayEndOfGame();
    }

    public void AddRadicalized(int number = 1)
    {
        _numberOfRadicalized += number;
        GuiManager.UpdateComradeNumber(_numberOfRadicalized.ToString());
    }
}