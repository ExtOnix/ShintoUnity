using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : CustomWidget
{
    Action OnPlayButtonClicked = null;
    [SerializeField] SceneAsset asset = null;

    [SerializeField] Button playButton = null;
    public override void Show()
    {
        base.Show();
        Debug.Log("Show Menu");
    }

    void Awake()
    {
        InitButton();
    }

    void InitButton()
    {
        OnPlayButtonClicked += Play;
        playButton.onClick.AddListener(() => { OnPlayButtonClicked?.Invoke(); });
    }

    void Play()
    {
        //if (!GameMode.Instance) Debug.Log("instance = null");
        //GameMode.Instance.GameState = EGameState.Game;
        StartCoroutine(LoadAsyncGameScene());
    }




    IEnumerator LoadAsyncGameScene()
    {
        AsyncOperation _operation = SceneManager.LoadSceneAsync(AssetDatabase.GetAssetOrScenePath(asset));

        while (!_operation.isDone)
        {
            Debug.Log(_operation.progress);
            yield return null;
        }
    }
}
