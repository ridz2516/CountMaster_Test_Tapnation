using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Installers/GameConfig")]
public class GameConfig : ScriptableObjectInstaller<GameConfig>
{
    public PlayerConfig PlayerVars          = new PlayerConfig();
    public InputVariables InputVars         = new InputVariables();
    public ObstacleConfig ObstacleConfig    = new ObstacleConfig();
    public CharacterConfig CharacterConfig  = new CharacterConfig();

    public override void InstallBindings()
    {
        Container.BindInstance(InputVars);

        Container.BindInstance(PlayerVars);
        Container.BindInstance(PlayerVars.PlayerSettings);
        Container.BindInstance(ObstacleConfig);

        Container.BindFactory<RedExplosion, RedExplosion.Factory>()
            .FromPoolableMemoryPool<RedExplosion, RedExplosionPool>(poolBinder => poolBinder
                .WithInitialSize(200)
                .FromComponentInNewPrefab(CharacterConfig.CharacterExplosionRed)
                .UnderTransformGroup("RedExplosionPool")
            );

        Container.BindFactory<BlueExplosion, BlueExplosion.Factory>()
            .FromPoolableMemoryPool<BlueExplosion, BlueExplosionPool>(poolBinder => poolBinder
                .WithInitialSize(200)
                .FromComponentInNewPrefab(CharacterConfig.CharacterExplosionBlue)
                .UnderTransformGroup("BlueExplosionPool")
            );

        Container.BindFactory<CharacterBlue, CharacterBlue.Factory>()
            .FromPoolableMemoryPool<CharacterBlue, CharacterBluePool>(poolBinder => poolBinder
                .WithInitialSize(200)
                .FromComponentInNewPrefab(PlayerVars.PlayerCharacter)
                .UnderTransformGroup("BluePlayerPool")
            );

        Container.BindFactory<CharacterRed, CharacterRed.Factory>()
            .FromPoolableMemoryPool<CharacterRed, CharacterRedPool>(poolBinder => poolBinder
                .WithInitialSize(200)
                .FromComponentInNewPrefab(CharacterConfig.EnemyCharacter)
                .UnderTransformGroup("RedPlayerPool")
            );
    }

    class CharacterBluePool : MonoPoolableMemoryPool<IMemoryPool, CharacterBlue>
    {
    }

    class CharacterRedPool : MonoPoolableMemoryPool<IMemoryPool, CharacterRed>
    {
    }

    class RedExplosionPool : MonoPoolableMemoryPool<IMemoryPool, RedExplosion>
    {
    }

    class BlueExplosionPool : MonoPoolableMemoryPool<IMemoryPool, BlueExplosion>
    {
    }

}

[Serializable]
public class InputVariables
{
    public float DragSensitivity = 0.01f;
}

[Serializable]
public class PlayerConfig
{
    public int StartPlayerAmount = 1;
    public GameObject PlayerCharacter;
    public PlayerMove.Settings PlayerSettings;
}

[Serializable] 
public class ObstacleConfig
{
    public Color PositiveColor;
    public Color NegativeColor;

    public Material PositiveTextMat;
    public Material NegativeTextMat;
}

[Serializable]
public class CharacterConfig
{
    public GameObject CharacterExplosionBlue;
    public GameObject CharacterExplosionRed;

    public GameObject EnemyCharacter;

}