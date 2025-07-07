using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private RectTransform playerBar;
    [SerializeField] private RectTransform enemyBar;

    private CapturePoint point;
    private Player player;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        point = FindAnyObjectByType<CapturePoint>();
    }

    private void OnEnable()
    {
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            UIManager.Instance.ShowUI(GameUI.Pause);
            Time.timeScale = 0;
        }
        DisplayHP();
        DisplayTimer();
        PlayerProgress();
        EnemyProgress();
    }

    public void DisplayHP()
    {
        hpText.text = "HP: " + player.GetCurrentHp().ToString();
    }

    public void DisplayTimer()
    {
        float timeLeft = GameManager.Instance.GetTimer();
        TimeSpan time = TimeSpan.FromSeconds(timeLeft);
        timerText.text = "Time: " + time.ToString("mm' : 'ss");
    }

    public void PlayerProgress()
    {
        if (point.capturePercentRange >= 0)
        {
            playerBar.localScale = new Vector3(point.capturePercentRange*0.01f, playerBar.localScale.y, playerBar.localScale.z);
        }
    }

    public void EnemyProgress()
    {
        if (point.capturePercentRange <= 0)
        {
            enemyBar.localScale = new Vector3(-point.capturePercentRange*0.01f, enemyBar.localScale.y, enemyBar.localScale.z);
        }
    }

}
