using UnityEngine;
using Zenject;

public class GameSignalInstaller : Installer<GameSignalInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<GameManager.OnLevelStarted>();
        Container.DeclareSignal<GameManager.OnLevelCompleted>();
        Container.DeclareSignal<GameManager.OnLevelRestart>();
    }
}
