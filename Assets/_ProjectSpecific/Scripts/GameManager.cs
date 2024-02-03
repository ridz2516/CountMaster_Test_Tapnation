
using System;
using Zenject;

public class GameManager
{
    readonly SignalBus _SignalBus;

    public GameManager(SignalBus _SignalBus)
    {
        this._SignalBus = _SignalBus;
    }

    public void LevelStarted()
    {
        _SignalBus.Fire<OnLevelStarted>();
    }

    public void LevelFinished()
    {
        _SignalBus.Fire<OnLevelCompleted>();
    }

    public void LevelRestart()
    {
        _SignalBus.Fire<OnLevelRestart>();
    }

    public struct OnLevelStarted { }

    public struct OnLevelCompleted{}

    public struct OnLevelRestart { }
}
