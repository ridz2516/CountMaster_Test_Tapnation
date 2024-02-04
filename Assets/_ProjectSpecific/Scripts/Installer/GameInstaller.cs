using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        installPlayer();
        installPresenter();
        installGameManager();

        GameSignalInstaller.Install(Container);
    }

    private void installPlayer()
    {
        Container.Bind<PlayerFactory>().AsSingle();
        Container.BindFactory<PlayerIdle, PlayerIdle.Factory>().AsSingle();
        Container.BindFactory<PlayerMove, PlayerMove.Factory>().AsSingle();
    }


    private void installPresenter()
    {
        Container.BindInterfacesAndSelfTo<LevelStartPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<GamePlayInputPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<LevelFinishPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<LevelCompletePresenter>().AsSingle();
    }

    private void installGameManager()
    {
        Container.Bind<GameManager>().AsSingle();
        Container.Bind<EnemyBaseDelegate>().AsSingle();
        Container.Bind<StorageManager>().AsSingle();
    }
}