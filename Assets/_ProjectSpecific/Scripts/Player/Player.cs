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
    private GameManager _GameManager;

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
    public void Constructor(PlayerFactory _PlayerFactory, SignalBus _SignalBus, GameManager _GameManager)
    {
        this._PlayerFactory = _PlayerFactory;
        this._SignalBus = _SignalBus;
        this._GameManager = _GameManager;
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
        _SignalBus.Subscribe<EnemyBaseDelegate.OnBaseEntered>(x => OnBaseEntered(x._EnemyBase));
        _SignalBus.Subscribe<EnemyBaseDelegate.OnBaseCompleted>(OnBaseCompleted);
        _SignalBus.Subscribe<GameManager.OnLevelFailed>(OnLeveFailed);
        _SignalBus.Subscribe<GameManager.OnLevelCompleted>(OnLevelCompleted);
        _SignalBus.Subscribe<GameManager.OnLevelRestart>(OnLevelRestart);

        _SplineFollower.onEndReached += OnEndReached;
    }

    private void OnDisable()
    {
        _SignalBus.TryUnsubscribe<GameManager.OnLevelStarted>(OnLevelStarted);
        _SignalBus.TryUnsubscribe<EnemyBaseDelegate.OnBaseEntered>(x => OnBaseEntered(x._EnemyBase));
        _SignalBus.TryUnsubscribe<EnemyBaseDelegate.OnBaseCompleted>(OnBaseCompleted);
        _SignalBus.TryUnsubscribe<GameManager.OnLevelFailed>(OnLeveFailed);
        _SignalBus.TryUnsubscribe<GameManager.OnLevelCompleted>(OnLevelCompleted);
        _SignalBus.TryUnsubscribe<GameManager.OnLevelRestart>(OnLevelRestart);
        _SplineFollower.onEndReached -= OnEndReached;
    }


    #endregion Init

    #region Event

    private void OnLevelStarted()
    {
        ChangeState(ePlayerState.Move);
    }

    private void OnLeveFailed()
    {
        _SplineFollower.follow = false;
    }

    private void OnLevelCompleted()
    {
        _SplineFollower.follow = false;
    }

    private void OnLevelRestart()
    {
        ChangeState(ePlayerState.Idle);
        _SideMovement.localPosition = Vector3.zero;
    }

    private void OnBaseEntered(CharacterLeaderEnemy _EnemyBase)
    {
        _SplineFollower.follow = false;
        _CharacterLeader.AttackCharacter(_EnemyBase.transform);
    }

    private void OnBaseCompleted()
    {
        _SplineFollower.follow = true;
        _CharacterLeader.SetTarget(_SideMovement);
    }

    private void OnEndReached(double _Amount)
    {
        _GameManager.LevelComplete();
    }

    #endregion Event

    private void Update()
    {
        if (_SplineFollower.follow)
        {
            _PlayerState.Update();
        }
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

    public void SetSplineComputer(SplineComputer _Computer)
    {
        _SplineFollower.spline = _Computer;
        _SplineFollower.Restart(0);
    }

}
