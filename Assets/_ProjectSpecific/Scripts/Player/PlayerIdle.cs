using UnityEngine;
using Zenject;

public class PlayerIdle : PlayerStateBase
{
    public override void Start()
    {
        _Player.SplineFollower.follow = false;
        _Player.SplineFollower.SetPercent(0);
    }

    public class Settings
    {
        public int StartTotalPlayer = 1;
    }

    public class Factory : PlaceholderFactory<PlayerIdle>
    {

    }
}
