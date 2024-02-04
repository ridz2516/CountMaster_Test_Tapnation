
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
    }

    public void LevelComplete()
    {
        _StorageManager.CurrentLevel++;
        _SignalBus.Fire<OnLevelCompleted>();
    }

    public void LevelFailed()
    {
        _SignalBus.Fire<OnLevelFailed>();
    }

    public void LevelRestart()
    {
        _SignalBus.Fire<OnLevelRestart>();
    }

    public struct OnLevelStarted { }

    public struct OnLevelCompleted{}

    public struct OnLevelFailed { }

    public struct OnLevelRestart { }
}
