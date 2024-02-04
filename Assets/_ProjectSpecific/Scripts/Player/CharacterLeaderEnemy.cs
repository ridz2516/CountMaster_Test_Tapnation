
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterLeaderEnemy : CharacterLeaderBase
{
    #region Data

    [SerializeField] private int TotalEnemy;
    [SerializeField] private List<CharacterRed> _Characters = new List<CharacterRed>();
    [SerializeField] private BubbleText _BubbleText;

    [SerializeField] private Animator _BaseAnim;

    private Player _Character;
    private CharacterRed.Factory _CharacterFactory;
    private SignalBus _SignalBus;
    EnemyBaseDelegate _EnemyBaseDelegate;

    const string _IdleAnim = "Idle";
    const string _Conquer = "Conquer";

    #endregion Data

    #region Get/Set

    public List<CharacterRed> AllCharacter { get => _Characters; }

    #endregion Get/Set

    #region Constructor

    [Inject]
    public void Constructor(CharacterRed.Factory _CharacterFactory, Player _Character, EnemyBaseDelegate _EnemyBaseDelegate)
    {
        this._CharacterFactory = _CharacterFactory;
        this._Character = _Character;
        this._EnemyBaseDelegate = _EnemyBaseDelegate;
    }

    #endregion Constructor

    private void OnEnable()
    {
        RemoveCharacter(_Characters.Count);
        AddCharacter(TotalEnemy);
        _BaseAnim.SetTrigger(_IdleAnim);
    }

    private void OnDisable()
    {
    }


    public override void AddCharacter(int _Amount)
    {
        for (int i = 0; i < _Amount; i++)
        {
            var character = _CharacterFactory.Create();
            character.transform.SetParent(transform);
            _Characters.Add(character);
            Vector3 pos = Random.insideUnitSphere;
            Vector3 newPos = transform.position + new Vector3(pos.x, 0, pos.z);
            character.SetAnimation(eCharacterAnimation.Idle);
            character.SetBase(this);
            character.transform.position = newPos;
            character.SetTarget(transform);
        }

        _BubbleText.UpdateText(_Characters.Count);
    }

    public override void RemoveCharacter(int _Amount)
    {
        for (int i = 0; i < _Amount; i++)
        {
            if (_Characters.Count > 0)
            {
                _Characters[0].DestroyCharacter();
                _Characters.RemoveAt(0);
            }
            else
            {
                i = _Amount;
            }
        }

        _BubbleText.UpdateText(_Characters.Count);
    }

    public void RemoveSpecificCharacter(CharacterRed _Target)
    {
        if (_Characters.Contains(_Target))
        {
            var index = _Characters.IndexOf(_Target);
            _Characters[index].DestroyCharacter(); ;
            _Characters.RemoveAt(index);
        }

        if (_Characters.Count == 0)
        {
            _EnemyBaseDelegate.EnemyBaseCompleted();
            _BaseAnim.SetTrigger(_Conquer);
        }

        _BubbleText.UpdateText(_Characters.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(nameof(eTags.Player)))
        {
            _EnemyBaseDelegate.EnemyBaseTriggered(this);
        }
    }

    public override void AttackCharacter(Transform _Target)
    {
        for (int i = 0; i < _Characters.Count; i++)
        {
            _Characters[i].SetAnimation(eCharacterAnimation.Run);
            _Characters[i].SetTarget(_Character.SideMovement);
        }
    }
}
