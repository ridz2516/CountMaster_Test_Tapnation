using System;
using UnityEngine;
using Zenject;

public class GamePlayInputPresenter : IInitializable, IDisposable, IFixedTickable,ILateTickable,ITickable
{
    #region Data

    readonly HUDController  _HudController;
    readonly InputVariables _InputVars;
    readonly SignalBus _SignalBus;

    private bool _IsInputDown;
    private Vector3 m_InputDownPosNormalized;
    private Vector3 m_LastInputPosNormalized;
    private Vector2 m_DeltaDrag;
    private Vector2 m_Drag;

    public Vector2 DeltaDrag => m_DeltaDrag;
    public Vector2 Drag => m_Drag;

    public bool IsInputDown => _IsInputDown;

    #endregion Data

    public GamePlayInputPresenter(HUDController _HudController,
        InputVariables _InputVars,
        SignalBus _SignalBus)
    {
        this._HudController = _HudController;
        this._InputVars = _InputVars;
        this._SignalBus = _SignalBus;
    }

    public void Initialize()
    {
        _HudController.OnInputDown  += OnInputDown;
        _HudController.OnInputUp    += OnInputUp;

        _SignalBus.Subscribe<GameManager.OnLevelStarted>(onLevelStarted);
    }

    private void onLevelStarted()
    {
    }

    public void Dispose()
    {
        _HudController.OnInputDown  -= OnInputDown;
        _HudController.OnInputUp    -= OnInputUp;
    }

    public void OnInputDown(Vector2 _mousePosition)
    {
        _IsInputDown = true;
        m_InputDownPosNormalized = m_LastInputPosNormalized = _mousePosition;
        
    }

    public void OnInputUp()
    {
        _IsInputDown = false;
    }

    public void FixedTick()
    {

    }

    public void SetInput(Vector3 _MousePos)
    {
        m_Drag = (_MousePos - m_InputDownPosNormalized) * _InputVars.DragSensitivity;
        m_DeltaDrag = (_MousePos - m_LastInputPosNormalized) * _InputVars.DragSensitivity;

        m_LastInputPosNormalized = _MousePos;
    }

    public void LateTick()
    {

    }

    public void Tick()
    {
        if (_IsInputDown)
        {
            SetInput(Input.mousePosition);
        }
    }
}
