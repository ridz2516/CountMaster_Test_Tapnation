using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;
using TMPro;

public class HUDController : MonoBehaviour, IMenuView
{
    #region Data

    [SerializeField] private CanvasGroup _CanvasGroup;

    public ExtendedButton   InputButton;

    public Action<Vector2> OnInputDown;
    public Action OnInputUp;

    private SignalBus _SignalBus;

    #endregion Data

    [Inject]
    public void Construct(SignalBus _SignalBus)
    {
        this._SignalBus = _SignalBus;
    }

    #region Init

    private void OnEnable()
    {
        InputButton.OnDownEvent += inputDown;
        InputButton.OnUpEvent   += inputUp;

        _SignalBus.Subscribe<GameManager.OnLevelStarted>(onLevelStarted);
        _SignalBus.Subscribe<GameManager.OnLevelCompleted>(onLevelFinished);
    }

    private void OnDisable()
    {
        InputButton.OnDownEvent -= inputDown;
        InputButton.OnUpEvent   -= inputUp;
    }

    #endregion Init

    #region Event

    private void onLevelStarted()
    {
        Show();
    }

    private void onLevelFinished()
    {
        Hide();
    }

    private void inputDown(PointerEventData eventData)
    {
        OnInputDown?.Invoke(Input.mousePosition);
    }

    private void inputUp(PointerEventData eventData)
    {
        OnInputUp?.Invoke();
    }

    public void Show()
    {
        _CanvasGroup.DOFade(1, 0.2f);
    }

    public void Hide()
    {
        _CanvasGroup.DOFade(0, 0.2f);
    }

    #endregion Event
}
