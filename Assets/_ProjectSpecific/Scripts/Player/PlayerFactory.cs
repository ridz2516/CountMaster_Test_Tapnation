
using UnityEngine;

public class PlayerFactory
{
    readonly PlayerIdle.Factory _PlayerIdleFactory;
    readonly PlayerMove.Factory _PlayerMoveFactory;

    public PlayerFactory(PlayerIdle.Factory _PlayerIdleFactory, PlayerMove.Factory _PlayerMoveFactory)
    {
        this._PlayerMoveFactory = _PlayerMoveFactory;
        this._PlayerIdleFactory = _PlayerIdleFactory;
    }

    public PlayerStateBase GetState(ePlayerState ePlayerState)
    {
        switch (ePlayerState)
        {
            case ePlayerState.Idle:
                return _PlayerIdleFactory.Create();
            case ePlayerState.Move:
                return _PlayerMoveFactory.Create();
            default:
                return _PlayerIdleFactory.Create();
        }
    }
}


