using System;
using UnityEngine;
using Zenject;

public class PlayerMove : PlayerStateBase
{
    #region Data

    readonly GamePlayInputPresenter _GamePlayPresenter;
    readonly Settings _Settings;
    readonly CharacterLeader _CharacterLeader;

    private float minXBound = -4.75f;
    private float maxXBound = 4.75f;

    #endregion Data

    public PlayerMove(GamePlayInputPresenter _GamePlayPresenter, Settings _Settings, CharacterLeader _CharacterLeader)
    {
        this._GamePlayPresenter = _GamePlayPresenter;
        this._Settings          = _Settings;
        this._CharacterLeader   = _CharacterLeader;
    }


    public override void Start()
    {
        _Player.SplineFollower.follow = true;
        _Player.SplineFollower.followSpeed = _Settings.Speed;
    }

    public override void Update()
    {

        CheckXBound();
        if (_GamePlayPresenter.IsInputDown)
        {
            var distance = _Player.SideMovement.position.x +_GamePlayPresenter.DeltaDrag.x;
            distance = Mathf.Clamp(distance, minXBound, maxXBound);
            _Player.SideMovement.position = new Vector3(distance, _Player.SideMovement.position.y, _Player.SideMovement.position.z);

           
        }
    }

    private void CheckXBound()
    {
        float minX = _Player.SideMovement.position.x;
        float maxX = _Player.SideMovement.position.x;

        for (int i = 0; i < _CharacterLeader.AllCharacter.Count; i++)
        {
            if (_CharacterLeader.AllCharacter[i].transform.position.x < minX)
            {
                minX = _CharacterLeader.AllCharacter[i].transform.position.x;
            }
            if (_CharacterLeader.AllCharacter[i].transform.position.x > maxX)
            {
                maxX = _CharacterLeader.AllCharacter[i].transform.position.x;
            }
        }

        Vector3 LeftControl = new Vector3(minX, _Player.SideMovement.position.y, _Player.SideMovement.position.z);
        Vector3 RightControl = new Vector3(maxX, _Player.SideMovement.position.y, _Player.SideMovement.position.z);


        if (Physics.Raycast(LeftControl, Vector3.left, _Settings.WallDetectDistance, _Settings.WallDetection))
        {
            minXBound = _Player.SideMovement.position.x;
        }
        else
        {
            minXBound = -_Settings.MaxDistance;
        }

        if (Physics.Raycast(RightControl, Vector3.right, _Settings.WallDetectDistance, _Settings.WallDetection))
        {
            maxXBound = _Player.SideMovement.position.x;
        }
        else
        {
            maxXBound = _Settings.MaxDistance;
        }
    }


    [Serializable]
    public class Settings
    {
        public float MaxDistance = 1.8f;
        public float Speed = 1;
        public LayerMask WallDetection;
        public float WallDetectDistance = 0.15f;
    }


    public class Factory : PlaceholderFactory<PlayerMove>
    {

    }
}
