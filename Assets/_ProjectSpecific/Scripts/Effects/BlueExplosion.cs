using UnityEngine;
using Zenject;

public class BlueExplosion : MonoBehaviour,IPoolable<IMemoryPool>
{
    #region Data

    [SerializeField] float _LifeTime = 1;
    [SerializeField] ParticleSystem _ParticleSystem;

    IMemoryPool _Pool;
    float _StartTime;

    #endregion Data

    public void OnDespawned()
    {
    }

    public void OnSpawned(IMemoryPool p1)
    {
        _Pool = p1;

        _ParticleSystem.Clear();
        _ParticleSystem.Play();
        _StartTime = Time.realtimeSinceStartup;
    }

    private void Update()
    {
        if (Time.realtimeSinceStartup - _StartTime > _LifeTime)
        {
            _Pool.Despawn(this);
        }
    }


    public class Factory : PlaceholderFactory<BlueExplosion>
    {

    }
}
