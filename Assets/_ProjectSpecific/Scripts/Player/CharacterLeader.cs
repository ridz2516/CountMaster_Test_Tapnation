using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterLeader : MonoBehaviour
{
    [SerializeField] private Transform _SideControllerObject;
    [SerializeField] private List<CharacterBlue> _Characters = new List<CharacterBlue>();

    private CharacterBlue.Factory _CharacterFactory;

    #region Get/Set

    public List<CharacterBlue> AllCharacter { get => _Characters; } 

    #endregion Get/Set

    #region Constructor

    [Inject]
    public void Constructor(CharacterBlue.Factory _CharacterFactory)
    {
        this._CharacterFactory = _CharacterFactory;
    }

    #endregion Constructor

    public void AddCharacter(int _Amount)
    {
        for (int i = 0; i < _Amount; i++)
        {
            var character = _CharacterFactory.Create();
            character.transform.SetParent(_SideControllerObject);
            _Characters.Add(character);
            Vector3 pos = Random.insideUnitSphere * 0.1f;
            Vector3 newPos = transform.position + pos;

            character.transform.position = newPos;
            character.GetComponent<CharacterBlue>().SetTarget(_SideControllerObject);
        }
    }

    public void RemoveCharacter(int _Amount)
    {
        for (int i = 0; i < _Amount; i++)
        {
            if (i < _Characters.Count)
                _Characters[i].DestroyCharacter();
            else
            {
                i = _Amount;
                // Call Level Fail
            }
        }
    }
}
