using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Installers/GameConfig")]
public class GameConfig : ScriptableObjectInstaller<GameConfig>
{
    public PlayerConfig PlayerVars = new PlayerConfig();
    public InputVariables InputVars = new InputVariables();

    public override void InstallBindings()
    {
        Container.BindInstance(InputVars);

        Container.BindInstance(PlayerVars);
        Container.BindInstance(PlayerVars.PlayerSettings);

        Container.BindFactory<CharacterBlue, CharacterBlue.Factory>()
            .FromPoolableMemoryPool<CharacterBlue, CharacterBluePool>(poolBinder => poolBinder
                .WithInitialSize(200)
                .FromComponentInNewPrefab(PlayerVars.PlayerCharacter)
                .UnderTransformGroup("BluePlayerPool")
            );
    }

    class CharacterBluePool : MonoPoolableMemoryPool<IMemoryPool, CharacterBlue>
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