
using UnityEngine;
using Zenject;

public class CharacterBlue : CharacterBase, IPoolable<IMemoryPool>
{
    [SerializeField] private CharacterMovement _CharacterMovement;
    IMemoryPool _pool;

    private CharacterLeader _CharacterLeader;
    private BlueExplosion.Factory _BlueExplosionFactory;

    [Inject]
    public void Construct(CharacterLeader _CharacterLeader, BlueExplosion.Factory _BlueExplosionFactory)
    {
        this._CharacterLeader = _CharacterLeader;
        this._BlueExplosionFactory = _BlueExplosionFactory;
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

    public override void DestroyCharacter()
    {
        gameObject.SetActive(false);
        _pool.Despawn(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag(nameof(eTags.Obstacle))|| other.transform.CompareTag(nameof(eTags.CharacterRed)))
        {
            var explosion = _BlueExplosionFactory.Create();
            explosion.transform.position = transform.position;
            _CharacterLeader.RemoveSpecificCharacter(this);
        }
    }

    public class Factory : PlaceholderFactory<CharacterBlue>
    {

    }
}
