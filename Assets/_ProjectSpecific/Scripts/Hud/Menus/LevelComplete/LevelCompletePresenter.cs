
using System;
using UnityEngine;
using Zenject;

public class LevelCompletePresenter: IInitializable, IDisposable
{
    readonly Level_Complete _LevelFinish;
    readonly GameManager _GameManager;


    public LevelCompletePresenter(GameManager _GameManager,
        Level_Complete _LevelFinish)
    {
        this._GameManager = _GameManager;
        this._LevelFinish = _LevelFinish;
    }

    public void Dispose()
    {
        _LevelFinish.RestartButton.onClick.RemoveAllListeners();
    }

    public void Initialize()
    {
        _LevelFinish.RestartButton.onClick.AddListener(onClickRetry);
    }

    private void onClickRetry()
    {
        _GameManager.LevelRestart();
        _LevelFinish.Hide();
    }

}
