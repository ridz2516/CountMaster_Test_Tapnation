using Zenject;

public class EnemyBaseDelegate
{
    readonly SignalBus _SignalBus;

    public EnemyBaseDelegate(SignalBus _SignalBus)
    {
        this._SignalBus = _SignalBus;
    }

    public void EnemyBaseTriggered(CharacterLeaderEnemy _base)
    {
        _SignalBus.Fire(new OnBaseEntered() { _EnemyBase =  _base});
    }

    public void EnemyBaseCompleted()
    {
        _SignalBus.Fire<OnBaseCompleted>();
    }


    public struct OnBaseEntered
    {
        public CharacterLeaderEnemy _EnemyBase;
    }

    public struct OnBaseCompleted { }

}
