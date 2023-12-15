using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class PauseUI : CustomWidget
{
    Action OnResumeButtonClicked = null;
    Action OnOptionsButtonClicked = null;
    Action OnQuitButtonClicked = null;



    [SerializeField] Button resumeButton = null;
    [SerializeField] Button optionsButton = null;
    [SerializeField] Button quitButton = null;

    void Awake()
    {
        InitButtons();
        Debug.Log("init Buttons");
    }


    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base .Hide();
    }


    void InitButtons()
    {
        OnResumeButtonClicked += Resume;
        OnOptionsButtonClicked += ShowOptions;
        OnQuitButtonClicked += ReturnMenu;

        resumeButton.onClick.AddListener(() => { OnResumeButtonClicked?.Invoke(); });
        optionsButton.onClick.AddListener(() => { OnOptionsButtonClicked?.Invoke(); });
        quitButton.onClick.AddListener(() => { OnQuitButtonClicked?.Invoke(); });
    }


    void Resume()
    {
        Debug.Log("resume");
    }
    void ShowOptions()
    {
        Debug.Log("options");
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;

    }
    void ReturnMenu()
    {
        Debug.Log("quit");
    }
}
