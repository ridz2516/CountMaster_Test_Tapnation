
using System;
using Zenject;

public class GameManager
{
    readonly SignalBus _SignalBus;
    readonly StorageManager _StorageManager;

    public GameManager(SignalBus _SignalBus, StorageManager _StorageManager)
    {
        this._SignalBus = _SignalBus;
        this._StorageManager = _StorageManager;
    }


    public void LevelStarted()
    {
        _SignalBus.Fire<OnLevelStarted>();
        GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "Level" + _StorageManager.CurrentLevel + "_Started");
        
    }

    public void LevelComplete()
    {
        GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Complete, "Level" + _StorageManager.CurrentLevel + "_Completed");
        _StorageManager.CurrentLevel++;
        _SignalBus.Fire<OnLevelCompleted>();
    }

    public void LevelFailed()
    {
        _SignalBus.Fire<OnLevelFailed>();
        GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Fail, "Level" + _StorageManager.CurrentLevel + "_Failed");
    }

    public void LevelRestart()
    {
        _SignalBus.Fire<OnLevelRestart>();
        GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Load, "Level" + _StorageManager.CurrentLevel + "_Loaded");
    }

    public struct OnLevelStarted { }

    public struct OnLevelCompleted{}

    public struct OnLevelFailed { }

    public struct OnLevelRestart { }
}
