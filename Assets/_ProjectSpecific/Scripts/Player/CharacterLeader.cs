using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterLeader : CharacterLeaderBase
{
    [SerializeField] private Transform _SideControllerObject;
    [SerializeField] private List<CharacterBlue> _Characters = new List<CharacterBlue>();
    [SerializeField] private BubbleText _BubbleText;

    private CharacterBlue.Factory _CharacterFactory;
    private SignalBus _SignalBus;
    private GameManager _GameManager;

    #region Get/Set

    public List<CharacterBlue> AllCharacter { get => _Characters; } 

    #endregion Get/Set

    #region Constructor

    [Inject]
    public void Constructor(CharacterBlue.Factory _CharacterFactory, SignalBus _SignalBus, GameManager _GameManager)
    {
        this._CharacterFactory = _CharacterFactory;
        this._SignalBus = _SignalBus;
        this._GameManager = _GameManager;
    }

    #endregion Constructor

    #region Init

    private void OnEnable()
    {
        _SignalBus.Subscribe<GameManager.OnLevelStarted>(onLevelStarted);
        _SignalBus.Subscribe<GameManager.OnLevelFailed>(OnLevelFailed);
        _SignalBus.Subscribe<GameManager.OnLevelCompleted>(OnLevelCompleted);
        _SignalBus.Subscribe<GameManager.OnLevelRestart>(OnLevelRestart);
    }

    private void OnDisable()
    {
        _SignalBus.TryUnsubscribe<GameManager.OnLevelStarted>(onLevelStarted);
        _SignalBus.TryUnsubscribe<GameManager.OnLevelFailed>(OnLevelFailed);
        _SignalBus.TryUnsubscribe<GameManager.OnLevelCompleted>(OnLevelCompleted);
        _SignalBus.TryUnsubscribe<GameManager.OnLevelRestart>(OnLevelRestart);
    }

    #endregion Init

    #region Event

    private void onLevelStarted()
    {
        for (int i = 0; i < _Characters.Count; i++)
        {
            _Characters[i].SetAnimation(eCharacterAnimation.Run);
        }
    }

    private void OnLevelCompleted()
    {
        for (int i = 0; i < _Characters.Count; i++)
        {
            _Characters[i].SetAnimation(eCharacterAnimation.Idle);
        }
    }

    private void OnLevelFailed()
    {
        for (int i = 0; i < _Characters.Count; i++)
        {
            _Characters[i].SetAnimation(eCharacterAnimation.Idle);
        }
    }

    private void OnLevelRestart()
    {
        foreach (var item in _Characters)
        {
            item.DestroyCharacter();
        }
        _Characters.Clear();
        AddCharacter(1);
    }

    #endregion Event

    

    #region Add/Remove

    public override void AddCharacter(int _Amount)
    {
        for (int i = 0; i < _Amount; i++)
        {
            var character = _CharacterFactory.Create();
            character.transform.SetParent(_SideControllerObject);
            _Characters.Add(character);
            Vector3 pos = Random.insideUnitSphere;
            Vector3 newPos = transform.position + new Vector3(pos.x,0,pos.z);

            if(_Characters.Count != 1)
                character.SetAnimation(eCharacterAnimation.Run);
            character.transform.position = newPos;
            character.SetTarget(_SideControllerObject);
        }

        _BubbleText.UpdateText(_Characters.Count);
    }

    public override void RemoveCharacter(int _Amount)
    {
        for (int i = 0; i < _Amount; i++)
        {
            if ( _Characters.Count > 0)
            {
                _Characters[0].DestroyCharacter();
                _Characters.RemoveAt(0);
            }
            else
            {
                i = _Amount;
            }
        }

        if(_Characters.Count == 0)
        {
            _GameManager.LevelFailed();
        }

        _BubbleText.UpdateText(_Characters.Count);
    }

    public void RemoveSpecificCharacter(CharacterBlue _Target)
    {
        if (_Characters.Contains(_Target))
        {
            var index = _Characters.IndexOf(_Target);
            _Characters[index].DestroyCharacter(); ;
            _Characters.RemoveAt(index);
        }

        if (_Characters.Count == 0)
        {
            _GameManager.LevelFailed();
        }

        _BubbleText.UpdateText(_Characters.Count);
    }

    #endregion Add/Remove

    #region Attack

    public override void AttackCharacter(Transform _Target)
    {
        for (int i = 0; i < _Characters.Count; i++)
        {
            _Characters[i].SetAnimation(eCharacterAnimation.Run);
            _Characters[i].SetTarget(_Target);
        }
    }

    public void SetTarget(Transform _Target)
    {
        for (int i = 0; i < _Characters.Count; i++)
        {
            _Characters[i].SetTarget(_Target);
        }

    }

    #endregion Attack
}
