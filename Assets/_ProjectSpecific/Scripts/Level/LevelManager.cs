using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level[] _AllLevels;

    private StorageManager _StorageManager;
    private Player          _Player;
    private SignalBus _SignalBus;

    [Inject]
    public void Construct(StorageManager _StorageManager, Player _Player, SignalBus _SignalBus)
    {
        this._StorageManager = _StorageManager;
        this._Player = _Player;
        this._SignalBus = _SignalBus;
    }

    private void OnEnable()
    {
        disableAll();
        setLevel();

        _SignalBus.Subscribe<GameManager.OnLevelRestart>(OnLevelReset);
    }

    private void OnDisable()
    {
        _SignalBus.TryUnsubscribe<GameManager.OnLevelRestart>(OnLevelReset);
    }

    private void OnLevelReset()
    {
        disableAll();
        setLevel();
    }

    private void setLevel()
    {
        var levelNo = _AllLevels[(_StorageManager.CurrentLevel - 1) % _AllLevels.Length];
        levelNo.gameObject.SetActive(true);
        _Player.SetSplineComputer(levelNo.SplineComputer);
    }

    private void disableAll()
    {
        foreach (var item in _AllLevels)
        {
            item.gameObject.SetActive(false);
        }
    }
}
