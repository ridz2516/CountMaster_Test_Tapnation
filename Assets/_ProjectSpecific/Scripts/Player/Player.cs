using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    #region Data

    [SerializeField] private SplineFollower _SplineFollower;
    [SerializeField] private Transform _SideMovement;
    [SerializeField] private CharacterLeader _CharacterLeader;

    private PlayerFactory       _PlayerFactory;
    private PlayerStateBase     _PlayerState;
    private SignalBus           _SignalBus;

    #endregion Data

    #region Get/Set

    public SplineFollower SplineFollower
    {
        get => _SplineFollower;
    }

    public Transform SideMovement
    {
        get => _SideMovement;
    }

    #endregion Get/Set

    #region Constructor

    [Inject]
    public void Constructor(PlayerFactory _PlayerFactory, SignalBus _SignalBus)
    {
        this._PlayerFactory = _PlayerFactory;
        this._SignalBus = _SignalBus;
    }

    #endregion Constructor

    #region Init

    private void Start()
    {
        ChangeState(ePlayerState.Idle);
        _CharacterLeader.AddCharacter(1);
    }

    private void OnEnable()
    {
        _SignalBus.Subscribe<GameManager.OnLevelStarted>(OnLevelStarted);
    }


    #endregion Init

    #region Event

    private void OnLevelStarted()
    {
        ChangeState(ePlayerState.Move);
        
    }

    #endregion Event

    private void Update()
    {
        _PlayerState.Update();
    }

    public void ChangeState(ePlayerState ePlayerState)
    {
        if(_PlayerState != null)
        {
            _PlayerState.Disposable();
            _PlayerState = null;
        }

        _PlayerState = _PlayerFactory.GetState(ePlayerState);
        _PlayerState.SetOwner(this);
        _PlayerState.Start();
    }

}
