
using UnityEngine;
using Zenject;

public class CharacterRed : CharacterBase, IPoolable<IMemoryPool>
{
    [SerializeField] private CharacterMovement _CharacterMovement;
    IMemoryPool _pool;

    [SerializeField] private CharacterLeaderEnemy _CharacterLeader;
    private RedExplosion.Factory _RedExplosionFactory;

    [Inject]
    public void Construct(RedExplosion.Factory _BlueExplosionFactory)
    {
        this._RedExplosionFactory = _BlueExplosionFactory;
    }

    public void OnDespawned()
    {
    }

    public void OnSpawned(IMemoryPool p1)
    {
        _pool = p1;
    }

    public void SetTarget(Transform _Target)
    {
        _CharacterMovement.SetTarget(_Target);
    }

    public void SetBase(CharacterLeaderEnemy _Base)
    {
         _CharacterLeader = _Base;
    }

    public override void DestroyCharacter()
    {
        gameObject.SetActive(false);
        _pool.Despawn(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag(nameof(eTags.CharacterBlue)))
        {
            var explosion = _RedExplosionFactory.Create();
            explosion.transform.position = transform.position;
            _CharacterLeader.RemoveSpecificCharacter(this);
        }
    }

    public class Factory : PlaceholderFactory<CharacterRed>
    {

    }
}