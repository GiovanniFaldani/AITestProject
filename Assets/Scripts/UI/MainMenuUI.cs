using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : BaseUI
{
    private void Awake()
    {
        Time.timeScale = 0.0f;
    }

    public void GoToHud()
    {
        UIManager.Instance.ShowUI(GameUI.HUD);
    }

    public void GoToOptions()
    {
        UIManager.Instance.ShowUI(GameUI.Option);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
