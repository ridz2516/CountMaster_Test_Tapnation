
using UnityEngine;
using Zenject;

public class CharacterBlue : MonoBehaviour, ICharacter, IPoolable<IMemoryPool>
{
    [SerializeField] private CharacterMovement _CharacterMovement;
    IMemoryPool _pool;

    public void OnDespawned()
    {
        throw new System.NotImplementedException();
    }

    public void OnSpawned(IMemoryPool p1)
    {
    }

    public void SetTarget(Transform _Target)
    {
        _CharacterMovement.SetTarget(_Target);
    }

    public void DestroyCharacter()
    {
        _pool.Despawn(this);
    }

    public class Factory : PlaceholderFactory<CharacterBlue>
    {

    }
}
