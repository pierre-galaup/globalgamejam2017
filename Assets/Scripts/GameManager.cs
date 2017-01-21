﻿using GameTime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TimeManager TimeManager;

    public GuiManager GuiManager;

    public PlayerRadicalization PlayerRadicalization;

    public LeaderBoard LeaderBoard;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        TimeManager.StartTimer(60);
    }

    public void EndTimer()
    {
        GuiManager.DisplayEndOfGame();
    }
}